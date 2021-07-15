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
                        
                _currentSkill.Name = EditorGUILayout.TextField("Name:", _currentSkill.Name);
                _currentSkill.Description = EditorGUILayout.TextField("Description:", _currentSkill.Description);
                _currentSkill.MpCost = EditorGUILayout.IntField("MP Cost:", _currentSkill.MpCost);

                EditorGUIUtility.labelWidth = 0;
                
            }
            if (!EditorGUI.EndChangeCheck()) return;

            UtilityHelper.SkillNameListRaw[UtilityHelper.CurrentSkillTab] = _currentSkill.Name;
            EditorUtility.SetDirty(_currentSkill);
            RefreshSkills();
        }

        private static void RefreshSkills()
        {
            UtilityHelper.SkillNameList.Clear();
            UtilityHelper.SkillNameListRaw.Clear();
            UtilityHelper.SkillGuidList.Clear();
            UtilityHelper.SkillAssetList.Clear();
            
            UtilityHelper.GetAssetData(4);
        }
    }
}
