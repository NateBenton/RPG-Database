using System;
using System.Collections.Generic;
using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class DatabaseEditor : EditorWindow
    {
        private const int AssetTabWidth = 240;
        private const int GeneralWindowWidth = 845;
        private readonly HeroContainer _heroContainer = new HeroContainer();

        [MenuItem("NBGames/RPG Editor")]

        public static void ShowWindow()
        {
            GetWindow(typeof(DatabaseEditor), false, "RPG Database");
        }

        private void OnEnable()
        {
            GetAssetData(0);
            GetAssetData(1);
        }

        private void OnDisable()
        {
            ClearAssetData();
        }

        private static void ClearAssetData()
        {
            UtilityHelper.HeroAssetList.Clear();
            UtilityHelper.ClassAssetList.Clear();
            UtilityHelper.HeroNameList.Clear();
            UtilityHelper.ClassNameList.Clear();
            UtilityHelper.HeroGUIDList.Clear();
        }

        private void OnGUI()
        {
            DisplayDatabaseTabs();
            DisplayWindowContent();
        }

        private static void GetAssetData(int dataType)
        {
            UtilityHelper.AssetList = dataType switch
            {
                0 => AssetDatabase.FindAssets("t:Hero"),
                1 => AssetDatabase.FindAssets("t:HeroClass"),
                _ => UtilityHelper.AssetList
            };

            for (var i = 0; i < UtilityHelper.AssetList.Length; i++)
            {
                switch (dataType)
                {
                    case 0:
                        UtilityHelper.AssetList[i] = AssetDatabase.GUIDToAssetPath(UtilityHelper.AssetList[i]);
                        UtilityHelper.HeroGUIDList.Add(UtilityHelper.AssetList[i]);
                        UtilityHelper.HeroAssetList.
                            Add((Hero)AssetDatabase.
                                LoadAssetAtPath(UtilityHelper.AssetList[i], typeof(Hero)));
                        break;
                    case 1:
                        UtilityHelper.AssetList[i] = AssetDatabase.GUIDToAssetPath(UtilityHelper.AssetList[i]);
                        UtilityHelper.ClassAssetList.
                            Add((HeroClass)AssetDatabase.
                                LoadAssetAtPath(UtilityHelper.AssetList[i], typeof(HeroClass)));
                        break;
                }
            }

            switch (dataType)
            {
                case 0:
                    foreach (var hero in UtilityHelper.HeroAssetList)
                    {
                        UtilityHelper.HeroNameList.Add(hero.HeroName);
                    }

                    break;
                case 1:
                    foreach (var heroClass in UtilityHelper.ClassAssetList)
                    {
                        UtilityHelper.ClassNameList.Add(heroClass.ClassName);
                    }

                    break;
            }
        }

        private static void DisplayDatabaseTabs()
        {
            UtilityHelper.CurrentDatabaseTab = GUILayout.Toolbar(UtilityHelper.CurrentDatabaseTab, 
                UtilityHelper.DatabaseTabNames);
        }

        private void DisplayWindowContent()
        {
            switch (UtilityHelper.CurrentDatabaseTab)
            {
                case 0:
                    DrawHeroTab();
                    break;
            }
        }

        private void DrawHeroTab()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();
                {
                    UtilityHelper.ScrollHeroTab = EditorGUILayout.BeginScrollView(UtilityHelper.ScrollHeroTab, 
                        GUILayout.Width(AssetTabWidth));
                    {
                        DisplayHeroAssetTabs();
                    }
                    EditorGUILayout.EndScrollView();

                    EditorGUILayout.BeginHorizontal();
                    {
                        HeroCreateButton();
                        DeleteHeroButton();
                        NewHeroFields();
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();

                HeroEditorControls();
            }
            EditorGUILayout.EndHorizontal();
        }

        private static void DisplayHeroAssetTabs()
        {
            if (UtilityHelper.HeroAssetList.Count <= 0) return;
            var heroNames = UtilityHelper.HeroNameList.ToArray();

            UtilityHelper.CurrentHeroTab = GUILayout.SelectionGrid(UtilityHelper.CurrentHeroTab, heroNames, 1);
        }
        
        private void HeroEditorControls()
        {
            UtilityHelper.ScrollHeroContainer = EditorGUILayout.BeginScrollView(UtilityHelper.ScrollHeroContainer,
                GUILayout.Width(GeneralWindowWidth));
            {
                if (UtilityHelper.HeroAssetList.Count > 0)
                {
                    _heroContainer.GeneralSettings();
                    //graphics settings container
                    //equipment settings container
                    //traits container
                    //apply button
                }
                else
                {
                    EditorGUILayout.HelpBox("No Hero data could be found. Please create a Hero.", MessageType.Warning);
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private static void HeroCreateButton()
        {
            if (UtilityHelper.CreatingHero) return;
            if (GUILayout.Button("Create", GUILayout.Width(120)))
            {
                UtilityHelper.CreatingHero = true;
            }
        }

        private void NewHeroFields()
        {
            if (!UtilityHelper.CreatingHero) return;
            EditorGUIUtility.labelWidth = 40;
            GUI.SetNextControlName("heroName");
            UtilityHelper.NewHeroName = EditorGUILayout.TextField("Name:", UtilityHelper.NewHeroName);
            EditorGUIUtility.labelWidth = 0;
            EditorGUI.FocusTextInControl("heroName");

            if (GUILayout.Button("Create"))
            {
                CreateNewHero(UtilityHelper.NewHeroName);
                UtilityHelper.NewHeroName = "New Hero";
                RefreshWindow();
                UtilityHelper.CreatingHero = false;
            }

            if (!GUILayout.Button("Cancel")) return;
            UtilityHelper.NewHeroName = "New Hero";
            UtilityHelper.CreatingHero = false;
        }

        private void DeleteHeroButton()
        {
            if (UtilityHelper.CreatingHero) return;
            if (!GUILayout.Button("Delete", GUILayout.Width(120))) return;
            var delete = EditorUtility.DisplayDialog("Delete hero?", "Are you sure you wish to delete this hero?", "Yes",
                "Cancel");

            if (!delete) return;

            AssetDatabase.DeleteAsset(UtilityHelper.HeroGUIDList[UtilityHelper.CurrentHeroTab]);
            RefreshWindow();

            if (UtilityHelper.HeroNameList.Count > 1 && UtilityHelper.CurrentHeroTab != 0)
            {
                if (UtilityHelper.CurrentHeroTab - 1 != 0)
                {
                    UtilityHelper.CurrentHeroTab -= 1;
                }
            }
            else
            {
                UtilityHelper.CurrentHeroTab = 0;
            }
        }

        private static string GenerateFileName(int dataType)
        {
            var randomString = "";
            for (var i = 0; i < 30; i++)
            {
                randomString += UtilityHelper.Glyph[UnityEngine.Random.Range(0, UtilityHelper.Glyph.Length)];
            }

            return dataType switch
            {
                1 => $"Class_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}",
                _ => $"Hero_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{randomString}"
            };
        }

        private static void CreateNewHero(string heroName)
        {
            var newHero = CreateInstance<Hero>();
            newHero.HeroName = heroName;
            var fileName = GenerateFileName(0);
            AssetDatabase.CreateAsset(newHero, $"Assets/_NBGames/Data/Heroes/{fileName}.asset");
        }

        private void RefreshWindow()
        {
            ClearAssetData();
            GetAssetData(0);
            GetAssetData(1);
            Repaint();
        }
    }
}
