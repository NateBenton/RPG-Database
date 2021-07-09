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
        private readonly SkillsContainer _skillsContainer = new SkillsContainer();
        private readonly HeroClassContainer _classContainer = new HeroClassContainer();

        [MenuItem("NBGames/RPG Editor")]

        public static void ShowWindow()
        {
            GetWindow(typeof(DatabaseEditor), false, "RPG Database");
        }

        private void OnEnable()
        {
            UtilityHelper.GetAssetData(0);
            UtilityHelper.GetAssetData(1);
            UtilityHelper.GetAssetData(2);
            UtilityHelper.GetAssetData(3);
            UtilityHelper.GetAssetData(4);

            UtilityHelper.onRefreshWindow += RefreshWindow;
        }

        private void OnDisable()
        {
            UtilityHelper.ClearAssetData();
            UtilityHelper.onRefreshWindow -= RefreshWindow;
        }
        
        private void OnGUI()
        {
            DisplayDatabaseTabs();
            EditorGUILayout.Space();
            DisplayWindowContent();
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
                    TabLayout
                    (
                        UtilityHelper.HeroAssetList.Count, 
                        ref UtilityHelper.HeroNameList, 
                        ref UtilityHelper.CurrentHeroTab, 
                        ref UtilityHelper.ScrollHeroTab, 
                        CreateHeroButton,
                        DeleteHeroButton,
                        NewHeroFields,
                        HeroEditorControls
                    );
                    break;
                case 1:
                    TabLayout
                    (
                        UtilityHelper.ClassAssetList.Count, 
                        ref UtilityHelper.ClassNameListRaw,
                        ref UtilityHelper.CurrentClassTab, 
                        ref UtilityHelper.ScrollClassTab, 
                        CreateClassButton,
                        DeleteClassButton,
                        NewClassFields,
                        ClassEditorControls
                    );
                    break;
                case 2:
                    TabLayout
                    (
                        UtilityHelper.SkillAssetList.Count, 
                        ref UtilityHelper.SkillNameListRaw, 
                        ref UtilityHelper.CurrentSkillTab, 
                        ref UtilityHelper.ScrollSkillTab, 
                        CreateSkillButton,
                        DeleteSkillButton,
                        NewSkillsFields,
                        SkillEditorControls
                    );
                    break;
            }
        }

        private void TabLayout
        (
            int listCount,
            ref List<string> nameList,
            ref int currentTab,
            ref Vector2 scroll, 
            Action createButton, 
            Action deleteButton, 
            Action newFields, 
            Action editorControls
            )
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();
                {
                    scroll = EditorGUILayout.BeginScrollView(scroll, 
                        GUILayout.Width(AssetTabWidth));
                    {
                        DisplayAssetTabs(ref listCount, ref nameList, ref currentTab);
                    }
                    EditorGUILayout.EndScrollView();

                    EditorGUILayout.BeginHorizontal();
                    {
                        createButton();
                        deleteButton();
                        newFields();
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();

                editorControls();
            }
            EditorGUILayout.EndHorizontal();
        }

        
        private static void DisplayAssetTabs(ref int listCount, ref List<string> nameList, ref int currentTab)
        {
            if (listCount == 0) return;
            var names = nameList.ToArray();
            currentTab = GUILayout.SelectionGrid(currentTab, names, 1);
        }
        
        private void HeroEditorControls()
        {
            EditorControls(ref UtilityHelper.ScrollHeroContainer, 
                UtilityHelper.HeroAssetList.Count, _heroContainer.GeneralSettings, "Hero");
        }

        private void ClassEditorControls()
        {
            EditorControls(ref UtilityHelper.ScrollClassContainer, 
                UtilityHelper.ClassAssetList.Count, _classContainer.GeneralSettings, "Class");
        }

        private void SkillEditorControls()
        {
            EditorControls(ref UtilityHelper.ScrollSkillContainer, 
                UtilityHelper.SkillAssetList.Count, _skillsContainer.GeneralSettings, "Skill");
        }

        private void EditorControls(ref Vector2 scrollContainer, int count, Action generalSettings, string dataType)
        {
            scrollContainer = EditorGUILayout.BeginScrollView(scrollContainer,
                GUILayout.Width(GeneralWindowWidth));
            {
                if (count > 0)
                {
                    generalSettings();
                    if (dataType == "Hero")
                    {
                        EditorGUILayout.Space();
                        _heroContainer.Stats();
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox(
                        $"No {dataType} data could be found. Please create a {dataType}", MessageType.Warning);
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private static void CreateHeroButton()
        {
            CreateButton(ref UtilityHelper.CreatingHero);
        }

        private static void CreateClassButton()
        {
            CreateButton(ref UtilityHelper.CreatingClass);
        }

        private static void CreateSkillButton()
        {
            CreateButton(ref UtilityHelper.CreatingSkill);
        }

        private static void CreateButton(ref bool isCreating)
        {
            if (isCreating) return;
            if (!GUILayout.Button("Create", GUILayout.Width(120))) return;
            GUI.FocusControl(null);
            isCreating = true;
        }
        
        private static void DeleteHeroButton()
        {
            if (UtilityHelper.HeroAssetList.Count == 0) return;
            DeleteButton(
                UtilityHelper.CreatingHero, 
                "hero", 
                UtilityHelper.HeroGuidList[UtilityHelper.CurrentHeroTab], 
                UtilityHelper.HeroNameList.Count, 
                ref UtilityHelper.CurrentHeroTab
                );
        }

        private static void DeleteClassButton()
        {
            if (UtilityHelper.ClassAssetList.Count == 0) return;
            DeleteButton(
                UtilityHelper.CreatingClass, 
                "class", 
                UtilityHelper.ClassGuidList[UtilityHelper.CurrentClassTab], 
                UtilityHelper.ClassNameList.Count, 
                ref UtilityHelper.CurrentClassTab
            );
        }

        private static void DeleteSkillButton()
        {
            if (UtilityHelper.SkillAssetList.Count == 0) return;
            DeleteButton(
                UtilityHelper.CreatingSkill, 
                "skill", 
                UtilityHelper.SkillGuidList[UtilityHelper.CurrentHeroTab], 
                UtilityHelper.SkillNameList.Count, 
                ref UtilityHelper.CurrentSkillTab
            );
        }

        private static void DeleteButton(bool isCreating, string dataType, string assetPath, int nameCount, ref int currentTab)
        {
            if (isCreating) return;
            if (!GUILayout.Button("Delete", GUILayout.Width(120))) return;
            
            var delete = EditorUtility.DisplayDialog(
                $"Delete {dataType}?", 
                $"Are you sure you wish to delete this {dataType}?", 
                "Yes", 
                "Cancel");

            if (!delete) return;
            GUI.FocusControl(null);

            AssetDatabase.DeleteAsset(assetPath);
            UtilityHelper.RefreshWindow();
            

            if (nameCount > 1 && currentTab != 0)
            {
                if (currentTab - 1 > 0)
                {
                    currentTab -= 1;
                }
                else
                {
                    currentTab = 0;
                }
            }
            else
            {
                currentTab = 0;
            }
        }

        private static void NewHeroFields()
        {
            NewFields(ref UtilityHelper.CreatingHero, 
                ref UtilityHelper.NewHeroName, 
                HeroContainer.CreateNewHero, 
                ref UtilityHelper.CurrentHeroTab, 
                UtilityHelper.HeroAssetList.Count, 
                "New Hero");
        }

        private static void NewClassFields()
        {
            NewFields(ref UtilityHelper.CreatingClass, 
                ref UtilityHelper.NewClassName, 
                HeroClassContainer.CreateNewClass, 
                ref UtilityHelper.CurrentClassTab, 
                UtilityHelper.ClassAssetList.Count, 
                "New Class");
        }

        private static void NewSkillsFields()
        {
            NewFields(ref UtilityHelper.CreatingSkill, 
                ref UtilityHelper.NewSkillName,
                SkillsContainer.CreateNewSkill,
                ref UtilityHelper.CurrentSkillTab,
                UtilityHelper.SkillAssetList.Count,
                "New Skill");
        }

        private static void NewFields(ref bool isCreating, ref string newName, 
            Action<string> createNewObject, ref int currentAssetTab, int assetCount, string defaultNewName)
        {
            if (!isCreating) return;
            
            EditorGUIUtility.labelWidth = 40;
            GUI.SetNextControlName("newName");
            newName = EditorGUILayout.TextField("Name:", newName);
            GUI.FocusControl("newName");
            EditorGUIUtility.labelWidth = 0;

            if (GUILayout.Button("Create"))
            {
                createNewObject(newName);
                newName = defaultNewName;
                UtilityHelper.RefreshWindow();
                isCreating = false;
                GUI.FocusControl(null);

                currentAssetTab = assetCount;
            }

            if (!GUILayout.Button("Cancel")) return;
            GUI.FocusControl(null);
            newName = defaultNewName;
            isCreating = false;
            GUI.FocusControl(null);
        }
        
        private void RefreshWindow()
        {
            UtilityHelper.ClearAssetData();
            UtilityHelper.GetAssetData(0);
            UtilityHelper.GetAssetData(1);
            UtilityHelper.GetAssetData(2);
            UtilityHelper.GetAssetData(3);
            UtilityHelper.GetAssetData(4);
            Repaint();
        }
    }
}
