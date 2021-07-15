using System.Collections.Generic;
using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class HeroControls
    {
        private Hero _currentHero;

        public void GeneralSettings()
        {
            _currentHero = UtilityHelper.HeroAssetList[UtilityHelper.CurrentHeroTab];
            
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentHero, "Changes to hero");
                EditorGUILayout.BeginHorizontal();
                {
                    _currentHero.Sprite = (Sprite) EditorGUILayout.ObjectField(_currentHero.Sprite, 
                        typeof(Sprite), false, GUILayout.Width(50), GUILayout.Height(80));
                    
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUIUtility.labelWidth = 75;
                        
                        _currentHero.Name = EditorGUILayout.TextField("Name:", _currentHero.Name);
                        _currentHero.Description = EditorGUILayout.TextField("Description:", _currentHero.Description);
                        _currentHero.ClassIndex = EditorGUILayout.Popup("Class:", _currentHero.ClassIndex, 
                            UtilityHelper.ClassNameList.ToArray());
                
                        _currentHero.WeaponIndex = EditorGUILayout.Popup("Weapon:", _currentHero.WeaponIndex, 
                            UtilityHelper.WeaponNameList.ToArray());

                        _currentHero.ArmorIndex = EditorGUILayout.Popup("Armor:", _currentHero.ArmorIndex,
                            UtilityHelper.ArmorNameList.ToArray());

                        if (UtilityHelper.WeaponAssetList.Count == 0)
                        {
                            _currentHero.WeaponIndex = 0;
                        }

                        if (UtilityHelper.ArmorAssetList.Count == 0)
                        {
                            _currentHero.ArmorIndex = 0;
                        }

                        if (UtilityHelper.ClassAssetList.Count == 0)
                        {
                            _currentHero.ClassIndex = 0;
                        }

                        EditorGUIUtility.labelWidth = 0;
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
            
            if (!EditorGUI.EndChangeCheck()) return;

            _currentHero = UtilityHelper.HeroAssetList[UtilityHelper.CurrentHeroTab];

            _currentHero.Class = _currentHero.ClassIndex != 0 
                ? UtilityHelper.ClassAssetList[_currentHero.ClassIndex - 1] : null;
            
            _currentHero.Weapon = _currentHero.WeaponIndex != 0
                ? UtilityHelper.WeaponAssetList[_currentHero.WeaponIndex - 1]
                : null;

            _currentHero.Armor = _currentHero.ArmorIndex != 0
                ? UtilityHelper.ArmorAssetList[_currentHero.ArmorIndex - 1]
                : null;

            UtilityHelper.HeroNameList[UtilityHelper.CurrentHeroTab] = _currentHero.Name;
            EditorUtility.SetDirty(_currentHero);
        }

        public void Stats()
        {
            _currentHero = UtilityHelper.HeroAssetList[UtilityHelper.CurrentHeroTab];
            if (_currentHero.Class == null) return;
            
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentHero, "Changes to hero");

                if (_currentHero.Class.LevelCurve != null)
                {
                    _currentHero.Stats.CurrentLevel = EditorGUILayout.IntSlider("Starting Level",
                        _currentHero.Stats.CurrentLevel, 1, _currentHero.Class.LevelCurve.AmountOfLevels);
                }
                
                _currentHero.Stats.Experience = EditorGUILayout.IntSlider("Experience Points",
                    _currentHero.Stats.Experience, 1, 9999);
            }
            
            if (!EditorGUI.EndChangeCheck()) return;

            _currentHero = UtilityHelper.HeroAssetList[UtilityHelper.CurrentHeroTab];

            _currentHero.Class = _currentHero.ClassIndex != 0 
                ? UtilityHelper.ClassAssetList[_currentHero.ClassIndex - 1] : null;
            
            _currentHero.Weapon = _currentHero.WeaponIndex != 0
                ? UtilityHelper.WeaponAssetList[_currentHero.WeaponIndex - 1]
                : null;

            _currentHero.Armor = _currentHero.ArmorIndex != 0
                ? UtilityHelper.ArmorAssetList[_currentHero.ArmorIndex - 1]
                : null;

            UtilityHelper.HeroNameList[UtilityHelper.CurrentHeroTab] = _currentHero.Name;
            EditorUtility.SetDirty(_currentHero);
        }
    }
}
