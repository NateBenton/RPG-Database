using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class SkillsControls
    {
        private Skill _currentSkill;

        public void GeneralSettings()
        {
            _currentSkill = UtilityHelper.SkillAssetList[UtilityHelper.CurrentSkillTab];
            
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentSkill, "Changes to skill");
                EditorGUIUtility.labelWidth = 75;
                        
                _currentSkill.SkillName = EditorGUILayout.TextField("Name:", _currentSkill.SkillName);
                _currentSkill.Description = EditorGUILayout.TextField("Description:", _currentSkill.Description);
                _currentSkill.MpCost = EditorGUILayout.IntField("MP Cost:", _currentSkill.MpCost);

                EditorGUIUtility.labelWidth = 0;
                
            }
            if (!EditorGUI.EndChangeCheck()) return;

            UtilityHelper.SkillNameList[UtilityHelper.CurrentSkillTab] = _currentSkill.SkillName;
            EditorUtility.SetDirty(_currentSkill);
        }
    }
}
