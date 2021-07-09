using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class HeroContainer
    {
        private bool _showGeneralSettings;
        private readonly HeroControls _heroControls = new HeroControls();
        
        public void GeneralSettings()
        {
            _showGeneralSettings = UtilityHelper.ShowHeroGeneralSettings;

            _showGeneralSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneralSettings, "General Settings");
            {
                if (_showGeneralSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _heroControls.GeneralSettings();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            UtilityHelper.ShowHeroGeneralSettings = _showGeneralSettings;
        }

        public void Stats()
        {
            UtilityHelper.ShowHeroStats = EditorGUILayout.BeginFoldoutHeaderGroup(UtilityHelper.ShowHeroStats, "Stats");
            {
                if (UtilityHelper.ShowHeroStats)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _heroControls.Stats();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public static void HeroCreateButton()
        {
            if (UtilityHelper.CreatingHero) return;
            if (!GUILayout.Button("Create", GUILayout.Width(120))) return;
            GUI.FocusControl(null);
            UtilityHelper.CreatingHero = true;
        }
        
        public static void DeleteHeroButton()
        {
            if (UtilityHelper.CreatingHero) return;
            if (!GUILayout.Button("Delete", GUILayout.Width(120))) return;
            var delete = EditorUtility.DisplayDialog("Delete hero?", "Are you sure you wish to delete this hero?", "Yes",
                "Cancel");

            if (!delete) return;
            GUI.FocusControl(null);

            AssetDatabase.DeleteAsset(UtilityHelper.HeroGuidList[UtilityHelper.CurrentHeroTab]);
            UtilityHelper.RefreshWindow();

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
        
        public static void NewHeroFields()
        {
            if (!UtilityHelper.CreatingHero) return;
            
            EditorGUIUtility.labelWidth = 40;
            GUI.SetNextControlName("newHeroName");
            UtilityHelper.NewHeroName = EditorGUILayout.TextField("Name:", UtilityHelper.NewHeroName);
            GUI.FocusControl("newHeroName");
            EditorGUIUtility.labelWidth = 0;

            if (GUILayout.Button("Create"))
            {
                CreateNewHero(UtilityHelper.NewHeroName);
                UtilityHelper.NewHeroName = "New Hero";
                UtilityHelper.RefreshWindow();
                UtilityHelper.CreatingHero = false;
                GUI.FocusControl(null);
                UtilityHelper.CurrentHeroTab = (UtilityHelper.HeroAssetList.Count - 1);
            }

            if (!GUILayout.Button("Cancel")) return;
            GUI.FocusControl(null);
            UtilityHelper.NewHeroName = "New Hero";
            UtilityHelper.CreatingHero = false;
            GUI.FocusControl(null);
        }
        
        private static void CreateNewHero(string heroName)
        {
            var newHero = ScriptableObject.CreateInstance<Hero>();
            newHero.HeroName = heroName;
            var fileName = UtilityHelper.GenerateFileName(0);
            AssetDatabase.CreateAsset(newHero, $"Assets/_NBGames/Data/Heroes/{fileName}.asset");
        }
    }
}
