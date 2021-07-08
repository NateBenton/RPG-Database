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
            var classNameList = UtilityHelper.ClassNameList.ToArray();
            
            
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentHero, "Changes to hero");
                _currentHero.HeroName = EditorGUILayout.TextField("Name:", _currentHero.HeroName);
                _currentHero.ClassIndex = EditorGUILayout.Popup("Class:", _currentHero.ClassIndex, classNameList);
            }
            
            if (!EditorGUI.EndChangeCheck()) return;

            _currentHero = UtilityHelper.HeroAssetList[UtilityHelper.CurrentHeroTab];
            _currentHero.Class = UtilityHelper.ClassAssetList[_currentHero.ClassIndex];

            UtilityHelper.HeroNameList[UtilityHelper.CurrentHeroTab] = _currentHero.HeroName;
            EditorUtility.SetDirty(_currentHero);
        }
    }
}
