using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class HeroClassControls
    {
        private HeroClass _currentClass;

        public void GeneralSettings()
        {
            _currentClass = UtilityHelper.ClassAssetList[UtilityHelper.CurrentClassTab];
            
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentClass, "Changes to class");
                EditorGUIUtility.labelWidth = 75;
                        
                _currentClass.ClassName = EditorGUILayout.TextField("Name:", _currentClass.ClassName);

                EditorGUIUtility.labelWidth = 0;
                
            }
            if (!EditorGUI.EndChangeCheck()) return;

            UtilityHelper.ClassNameList[UtilityHelper.CurrentClassTab] = _currentClass.ClassName;
            EditorUtility.SetDirty(_currentClass);
        }
    }
}
