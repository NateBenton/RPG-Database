using System.Runtime.Remoting.Messaging;
using _NBGames.Scripts.RPGDatabase.Misc;
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
                EditorGUIUtility.labelWidth = 85;
                        
                _currentClass.Name = EditorGUILayout.TextField("Name:", _currentClass.Name);
                
                _currentClass.SkillsToLearn = Mathf.Clamp(EditorGUILayout.IntField("Skills to Learn:", 
                    _currentClass.SkillsToLearn), 0, 99);

                EditorGUIUtility.labelWidth = 0;
                
            }
            if (!EditorGUI.EndChangeCheck()) return;

            UtilityHelper.ClassNameListRaw[UtilityHelper.CurrentClassTab] = _currentClass.Name;

            ReadjustSkillLists();
            
            EditorUtility.SetDirty(_currentClass);
            RefreshClasses();
        }

        private void ReadjustSkillLists()
        {
            while (_currentClass.SkillIds.Count < _currentClass.SkillsToLearn)
            {
                _currentClass.SkillIds.Add(0);
                _currentClass.LevelsLearned.Add(0);
                _currentClass.SkillFields.Add(new SkillField());
            }

            while (_currentClass.SkillIds.Count > _currentClass.SkillsToLearn)
            {
                _currentClass.SkillIds.RemoveAt(_currentClass.SkillIds.Count -1);
                _currentClass.LevelsLearned.RemoveAt(_currentClass.LevelsLearned.Count - 1);
                _currentClass.SkillFields.RemoveAt(_currentClass.SkillFields.Count - 1);
            }
        }

        public void SkillSettings()
        {
            _currentClass = UtilityHelper.ClassAssetList[UtilityHelper.CurrentClassTab];
            if (_currentClass.SkillsToLearn <= 0) return;
            
            EditorGUI.BeginChangeCheck();
            {
                for (var i = 0; i < _currentClass.SkillsToLearn; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUIUtility.labelWidth = 45;
                        _currentClass.SkillIds[i] = EditorGUILayout.Popup("Skill:", _currentClass.SkillIds[i], 
                            UtilityHelper.SkillNameList.ToArray());

                        EditorGUIUtility.labelWidth = 90;
                        
                        _currentClass.LevelsLearned[i] =
                            EditorGUILayout.IntField("Level Learned:", _currentClass.LevelsLearned[i]);
                        

                        _currentClass.SkillFields[i].SkillLearned
                            = _currentClass.SkillIds[i] != 0 ?
                                UtilityHelper.SkillAssetList[_currentClass.SkillIds[i] - 1] : null;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUIUtility.labelWidth = 0;
            }
            if (!EditorGUI.EndChangeCheck()) return;
            
            EditorUtility.SetDirty(_currentClass);
        }

        public void LevelCurveSettings()
        {
            _currentClass = UtilityHelper.ClassAssetList[UtilityHelper.CurrentClassTab];
            
            EditorGUI.BeginChangeCheck();
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUIUtility.labelWidth = 75;
                    
                    _currentClass.CurveIndex = EditorGUILayout.Popup("Level Curve:", _currentClass.CurveIndex,
                        UtilityHelper.CurveNameList.ToArray());
                    
                    EditorGUIUtility.labelWidth = 0;
                }
                EditorGUILayout.EndVertical();
            }
            if (!EditorGUI.EndChangeCheck()) return;

            _currentClass.LevelCurve = _currentClass.CurveIndex == 0 
                ? null : UtilityHelper.CurveAssetList[_currentClass.CurveIndex - 1];
            
            EditorUtility.SetDirty(_currentClass);
        }

        private static void RefreshClasses()
        {
            UtilityHelper.ClassNameList.Clear();
            UtilityHelper.ClassNameListRaw.Clear();
            UtilityHelper.ClassGuidList.Clear();
            UtilityHelper.ClassAssetList.Clear();
            UtilityHelper.GetAssetData(1);
        }
    }
}
