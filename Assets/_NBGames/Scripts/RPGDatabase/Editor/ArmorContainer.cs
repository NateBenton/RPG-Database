using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class ArmorContainer
    {
        private bool _showGeneralSettings = true;
        private ArmorControls _armorControls = new ArmorControls();

        public void GeneralSettings()
        {
            _showGeneralSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneralSettings, "General Settings");
            {
                if (_showGeneralSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _armorControls.GeneralSettings();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
        public static void CreateNewArmor(string armorName)
        {
            var newArmor = ScriptableObject.CreateInstance<Armor>();
            newArmor.Name = armorName;
            var fileName = UtilityHelper.GenerateFileName(3);
            AssetDatabase.CreateAsset(newArmor, $"Assets/_NBGames/Data/Armor/{fileName}.asset");
        }
    }
}
