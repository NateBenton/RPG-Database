using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class ArmorControls
    {
        private Armor _currentArmor;

        public void GeneralSettings()
        {
            _currentArmor = UtilityHelper.ArmorAssetList[UtilityHelper.CurrentArmorTab];
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentArmor, "Changes to armor");
                EditorGUIUtility.labelWidth = 105;
                        
                _currentArmor.Name = EditorGUILayout.TextField("Name:", _currentArmor.Name);
                
                _currentArmor.AttackModifier =
                    EditorGUILayout.IntField("Attack Modifier:", _currentArmor.AttackModifier);
                
                _currentArmor.DefenseModifier =
                    EditorGUILayout.IntField("Defense Modifier:", _currentArmor.DefenseModifier);
                
                _currentArmor.HpModifier =
                    EditorGUILayout.IntField("HP Modifier:", _currentArmor.HpModifier);
                
                _currentArmor.MpModifier =
                    EditorGUILayout.IntField("MP Modifier:", _currentArmor.MpModifier);

                EditorGUIUtility.labelWidth = 0;
                
            }
            if (!EditorGUI.EndChangeCheck()) return;

            UtilityHelper.ArmorNameListRaw[UtilityHelper.CurrentArmorTab] = _currentArmor.Name;
            EditorUtility.SetDirty(_currentArmor);
            RefreshArmor();
        }

        private static void RefreshArmor()
        {
            UtilityHelper.ArmorNameList.Clear();
            UtilityHelper.ArmorNameListRaw.Clear();
            UtilityHelper.ArmorGuidList.Clear();
            UtilityHelper.ArmorAssetList.Clear();
            
            UtilityHelper.GetAssetData(3);
        }
    }
}
