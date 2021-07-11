using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class WeaponControls
    {
        private Weapon _currentWeapon;

        public void GeneralSettings()
        {
            _currentWeapon = UtilityHelper.WeaponAssetList[UtilityHelper.CurrentWeaponTab];
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentWeapon, "Changes to weapon");
                EditorGUIUtility.labelWidth = 105;
                        
                _currentWeapon.Name = EditorGUILayout.TextField("Name:", _currentWeapon.Name);
                
                _currentWeapon.AttackModifier =
                    EditorGUILayout.IntField("Attack Modifier:", _currentWeapon.AttackModifier);
                
                _currentWeapon.DefenseModifier =
                    EditorGUILayout.IntField("Defense Modifier:", _currentWeapon.DefenseModifier);
                
                _currentWeapon.HpModifier =
                    EditorGUILayout.IntField("HP Modifier:", _currentWeapon.HpModifier);
                
                _currentWeapon.MpModifier =
                    EditorGUILayout.IntField("MP Modifier:", _currentWeapon.MpModifier);

                EditorGUIUtility.labelWidth = 0;
                
            }
            if (!EditorGUI.EndChangeCheck()) return;

            UtilityHelper.WeaponNameListRaw[UtilityHelper.CurrentSkillTab] = _currentWeapon.Name;
            EditorUtility.SetDirty(_currentWeapon);
            RefreshWeapons();
        }

        private static void RefreshWeapons()
        {
            UtilityHelper.WeaponNameList.Clear();
            UtilityHelper.WeaponNameListRaw.Clear();
            UtilityHelper.WeaponGuidList.Clear();
            UtilityHelper.WeaponAssetList.Clear();
            
            UtilityHelper.GetAssetData(2);
        }
    }
}
