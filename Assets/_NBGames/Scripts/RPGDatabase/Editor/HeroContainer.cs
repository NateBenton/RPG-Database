using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class HeroContainer
    {
        private bool _showGeneralSettings = true;
        private readonly HeroControls _heroControls = new HeroControls();
        
        public void GeneralSettings()
        {
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

        public static void CreateNewHero(string heroName)
        {
            var newHero = ScriptableObject.CreateInstance<Hero>();
            newHero.Name = heroName;
            var fileName = UtilityHelper.GenerateFileName(0);
            AssetDatabase.CreateAsset(newHero, $"Assets/_NBGames/Data/Heroes/{fileName}.asset");
        }
    }
}
