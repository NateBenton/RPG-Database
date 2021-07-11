using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class WeaponContainer
    {
        private bool _showGeneralSettings = true;
        private WeaponControls _weaponControls = new WeaponControls();

        public void GeneralSettings()
        {
            _showGeneralSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneralSettings, "General Settings");
            {
                if (_showGeneralSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _weaponControls.GeneralSettings();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
        public static void CreateNewWeapon(string weaponName)
        {
            var newWeapon = ScriptableObject.CreateInstance<Weapon>();
            newWeapon.Name = weaponName;
            var fileName = UtilityHelper.GenerateFileName(2);
            AssetDatabase.CreateAsset(newWeapon, $"Assets/_NBGames/Data/Weapons/{fileName}.asset");
        }
    }
}
