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
            UtilityHelper.GetAssetData(0);
            UtilityHelper.GetAssetData(1);
            UtilityHelper.GetAssetData(2);
            UtilityHelper.GetAssetData(3);

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
                        HeroContainer.HeroCreateButton();
                        HeroContainer.DeleteHeroButton();
                        HeroContainer.NewHeroFields();
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
                    EditorGUILayout.Space();
                    _heroContainer.Stats();
                }
                else
                {
                    EditorGUILayout.HelpBox("No Hero data could be found. Please create a Hero.", MessageType.Warning);
                }
            }
            EditorGUILayout.EndScrollView();
        }

        
        
        private void RefreshWindow()
        {
            UtilityHelper.ClearAssetData();
            UtilityHelper.GetAssetData(0);
            UtilityHelper.GetAssetData(1);
            UtilityHelper.GetAssetData(2);
            UtilityHelper.GetAssetData(3);
            Repaint();
        }
    }
}
