using _NBGames.Scripts.RPGDatabase.Utilities;
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
    }
}
