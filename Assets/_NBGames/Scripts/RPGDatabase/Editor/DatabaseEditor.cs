using System;
using System.Collections.Generic;
using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
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
                UtilityHelper.AssetList[i] = AssetDatabase.GUIDToAssetPath(UtilityHelper.AssetList[i]);

                switch (dataType)
                {
                    case 0:
                        UtilityHelper.HeroAssetList.
                            Add((Hero)AssetDatabase.
                                LoadAssetAtPath(UtilityHelper.AssetList[i], typeof(Hero)));
                        break;
                    case 1:
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
            UtilityHelper.ScrollHeroTab = EditorGUILayout.BeginScrollView(UtilityHelper.ScrollHeroTab,
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
    }
}
