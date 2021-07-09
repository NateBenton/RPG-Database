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
                        typeof(Sprite), false, GUILayout.Width(70), GUILayout.Height(70));
                    
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUIUtility.labelWidth = 75;
                        
                        _currentHero.HeroName = EditorGUILayout.TextField("Name:", _currentHero.HeroName);
                        _currentHero.Description = EditorGUILayout.TextField("Description:", _currentHero.Description);
                        _currentHero.ClassIndex = EditorGUILayout.Popup("Class:", _currentHero.ClassIndex, 
                            UtilityHelper.ClassNameList.ToArray());
                
                        _currentHero.WeaponIndex = EditorGUILayout.Popup("Weapon:", _currentHero.WeaponIndex, 
                            UtilityHelper.WeaponNameList.ToArray());

                        _currentHero.ArmorIndex = EditorGUILayout.Popup("Armor:", _currentHero.ArmorIndex,
                            UtilityHelper.ArmorNameList.ToArray());

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

            UtilityHelper.HeroNameList[UtilityHelper.CurrentHeroTab] = _currentHero.HeroName;
            EditorUtility.SetDirty(_currentHero);
        }

        public void Stats()
        {
            _currentHero = UtilityHelper.HeroAssetList[UtilityHelper.CurrentHeroTab];
            
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentHero, "Changes to hero");
                _currentHero.Stats.MaxLevel = EditorGUILayout.IntSlider("Max Level", 
                    _currentHero.Stats.MaxLevel, 1, 99);

                _currentHero.Stats.CurrentLevel = EditorGUILayout.IntSlider("Starting Level",
                    _currentHero.Stats.CurrentLevel, 1, _currentHero.Stats.MaxLevel);
                
                _currentHero.Stats.Experience = EditorGUILayout.IntSlider("Experience Points",
                    _currentHero.Stats.Experience, 1, 9999);
                
                _currentHero.Stats.Attack = EditorGUILayout.IntSlider("Attack",
                    _currentHero.Stats.Attack, 1, 255);

                _currentHero.Stats.Defense = EditorGUILayout.IntSlider("Defense",
                    _currentHero.Stats.Defense, 1, 255);

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

            UtilityHelper.HeroNameList[UtilityHelper.CurrentHeroTab] = _currentHero.HeroName;
            EditorUtility.SetDirty(_currentHero);
        }
    }
}
