using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class SkillsContainer
    {
        private bool _showGeneralSettings = true;
        private SkillsControls _skillsControls = new SkillsControls();

        public void GeneralSettings()
        {
            _showGeneralSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneralSettings, "General Settings");
            {
                if (_showGeneralSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _skillsControls.GeneralSettings();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
        public static void CreateNewSkill(string skillName)
        {
            var newSkill = ScriptableObject.CreateInstance<Skill>();
            newSkill.SkillName = skillName;
            var fileName = UtilityHelper.GenerateFileName(2);
            AssetDatabase.CreateAsset(newSkill, $"Assets/_NBGames/Data/Skills/{fileName}.asset");
        }
    }
}
