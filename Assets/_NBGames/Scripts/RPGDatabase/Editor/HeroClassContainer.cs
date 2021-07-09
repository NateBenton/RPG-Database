using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class HeroClassContainer
    {
        private bool _showGeneralSettings = true;
        private bool _showSkillSettings = true;
        private HeroClassControls _classControls = new HeroClassControls();

        public void GeneralSettings()
        {
            _showGeneralSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneralSettings, "General Settings");
            {
                if (_showGeneralSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _classControls.GeneralSettings();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public void SkillSettings()
        {
            _showSkillSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showSkillSettings, "Skill Settings");
            {
                if (!_showSkillSettings) return;
                EditorGUILayout.BeginVertical("Box");
                {
                    _classControls.SkillSettings();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
        public static void CreateNewClass(string className)
        {
            var newClass = ScriptableObject.CreateInstance<HeroClass>();
            newClass.ClassName = className;
            var fileName = UtilityHelper.GenerateFileName(1);
            AssetDatabase.CreateAsset(newClass, $"Assets/_NBGames/Data/Hero Class/{fileName}.asset");
        }
    }
}
