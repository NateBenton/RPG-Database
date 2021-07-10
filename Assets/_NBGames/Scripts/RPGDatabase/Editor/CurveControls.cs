using _NBGames.Scripts.RPGDatabase.Misc;
using _NBGames.Scripts.RPGDatabase.Utilities;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class CurveControls
    {
        private LevelCurve _currentCurve;

        public void GeneralSettings()
        {
            _currentCurve = UtilityHelper.CurveAssetList[UtilityHelper.CurrentCurveTab];
            
            EditorGUI.BeginChangeCheck();
            {
                Undo.RecordObject(_currentCurve, "Changes to level curve");
                EditorGUIUtility.labelWidth = 115;
                        
                _currentCurve.CurveName = EditorGUILayout.TextField("Name:", _currentCurve.CurveName);
                
                _currentCurve.AttackMultiplier =
                    EditorGUILayout.FloatField("Attack Multiplier:", _currentCurve.AttackMultiplier);
                
                _currentCurve.DefenseMultiplier =
                    EditorGUILayout.FloatField("Defense Multiplier:", _currentCurve.DefenseMultiplier);
                
                _currentCurve.MaxHpMultiplier =
                    EditorGUILayout.FloatField("HP Multiplier:", _currentCurve.MaxHpMultiplier);
                
                _currentCurve.MaxMpMultiplier =
                    EditorGUILayout.FloatField("MP Multiplier:", _currentCurve.MaxMpMultiplier);
                
                _currentCurve.ExpMultiplier =
                    EditorGUILayout.FloatField("Exp Multiplier:", _currentCurve.ExpMultiplier);
                
                
                
                _currentCurve.AmountOfLevels = EditorGUILayout.IntField("Total Levels:", _currentCurve.AmountOfLevels);
                
                

                EditorGUIUtility.labelWidth = 0;
                
            }
            if (!EditorGUI.EndChangeCheck()) return;

            UtilityHelper.CurveNameListRaw[UtilityHelper.CurrentSkillTab] = _currentCurve.CurveName;
            ReadjustCurveLists();
            EditorUtility.SetDirty(_currentCurve);
            RefreshCurves();
        }

        public void BaseStats()
        {
            _currentCurve = UtilityHelper.CurveAssetList[UtilityHelper.CurrentCurveTab];

            EditorGUI.BeginChangeCheck();
            {
                EditorGUIUtility.labelWidth = 70;
                EditorGUILayout.BeginVertical();
                {
                    _currentCurve.BaseAttack = EditorGUILayout.IntField("Attack:", _currentCurve.BaseAttack);
                    _currentCurve.BaseDefense = EditorGUILayout.IntField("Defense:", _currentCurve.BaseDefense);
                    _currentCurve.BaseHp = EditorGUILayout.IntField("HP:", _currentCurve.BaseHp);
                    _currentCurve.BaseMp = EditorGUILayout.IntField("MP:", _currentCurve.BaseMp);
                    _currentCurve.BaseExp = EditorGUILayout.IntField("Exp:", _currentCurve.BaseExp);
                    EditorGUIUtility.labelWidth = 90;
                    GUILayout.Space(20);
                }
                EditorGUILayout.EndVertical();
                EditorGUIUtility.labelWidth = 0;
            }
            if (!EditorGUI.EndChangeCheck()) return;
            EditorUtility.SetDirty(_currentCurve);
            RefreshCurves();
        }

        public void MaxStats()
        {
            _currentCurve = UtilityHelper.CurveAssetList[UtilityHelper.CurrentCurveTab];

            EditorGUI.BeginChangeCheck();
            {
                EditorGUIUtility.labelWidth = 115;
                EditorGUILayout.BeginVertical();
                {
                    _currentCurve.MaxAttack = EditorGUILayout.IntField("Max Attack:", _currentCurve.MaxAttack);
                    _currentCurve.MaxDefense = EditorGUILayout.IntField("Max Defense:", _currentCurve.MaxDefense);
                    _currentCurve.MaxHp = EditorGUILayout.IntField("Max HP:", _currentCurve.MaxHp);
                    _currentCurve.MaxMp = EditorGUILayout.IntField("Max MP:", _currentCurve.MaxMp);
                    EditorGUIUtility.labelWidth = 90;
                    GUILayout.Space(20);
                }
                EditorGUILayout.EndVertical();
                EditorGUIUtility.labelWidth = 0;
            }
            if (!EditorGUI.EndChangeCheck()) return;
            EditorUtility.SetDirty(_currentCurve);
        }

        private void ReadjustCurveLists()
        {
            if (_currentCurve.Attack.Count < _currentCurve.AmountOfLevels)
            {
                for (var i = _currentCurve.Attack.Count; i < _currentCurve.AmountOfLevels; i++)
                {
                    if (i == 0)
                    {
                        _currentCurve.Attack.Add(_currentCurve.BaseAttack);
                        _currentCurve.Defense.Add(_currentCurve.BaseDefense);
                        _currentCurve.Hp.Add(_currentCurve.BaseHp);
                        _currentCurve.Mp.Add(_currentCurve.BaseMp);
                        _currentCurve.ExpToNextLevel.Add(_currentCurve.BaseExp);
                    }
                    else
                    {
                        _currentCurve.Attack.Add(Mathf.
                            RoundToInt(_currentCurve.Attack[i - 1] * _currentCurve.AttackMultiplier));
                        
                        _currentCurve.Defense.Add(Mathf.
                            RoundToInt(_currentCurve.Defense[i - 1] * _currentCurve.DefenseMultiplier));
                        
                        _currentCurve.Hp.Add(Mathf.
                            RoundToInt(_currentCurve.Hp[i - 1] * _currentCurve.MaxHpMultiplier));
                        
                        _currentCurve.Mp.Add(Mathf.
                            RoundToInt(_currentCurve.Mp[i - 1] * _currentCurve.MaxMpMultiplier));
                        
                        _currentCurve.ExpToNextLevel.Add(Mathf.
                            RoundToInt(_currentCurve.ExpToNextLevel[i -1] * _currentCurve.ExpMultiplier));
                    }
                }
            }

            while (_currentCurve.Attack.Count > _currentCurve.AmountOfLevels)
            {
                _currentCurve.Attack.RemoveAt(_currentCurve.Attack.Count - 1);
                _currentCurve.Defense.RemoveAt(_currentCurve.Defense.Count -1);
                _currentCurve.Hp.RemoveAt(_currentCurve.Hp.Count -1);
                _currentCurve.Mp.RemoveAt(_currentCurve.Mp.Count -1);
                _currentCurve.ExpToNextLevel.RemoveAt(_currentCurve.ExpToNextLevel.Count -1);
            }
        }

        public void CurveSettings()
        {
            _currentCurve = UtilityHelper.CurveAssetList[UtilityHelper.CurrentCurveTab];
            if (_currentCurve.AmountOfLevels <= 0) return;
            
            EditorGUI.BeginChangeCheck();
            {
                for (var i = 0; i < _currentCurve.AmountOfLevels; i++)
                {
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUIUtility.labelWidth = 95;
                        EditorGUILayout.LabelField($"Level {i + 1}:");
                        _currentCurve.Attack[i] = EditorGUILayout.IntField("Attack:", 
                            Mathf.Clamp(_currentCurve.Attack[i], 1, _currentCurve.MaxAttack));
                        
                        _currentCurve.Defense[i] = EditorGUILayout.IntField("Defense:", 
                            Mathf.Clamp(_currentCurve.Defense[i], 1, _currentCurve.MaxDefense));
                        
                        _currentCurve.Hp[i] = EditorGUILayout.IntField("Max HP:",
                            Mathf.Clamp(_currentCurve.Hp[i], 1, _currentCurve.MaxHp));
                        
                        _currentCurve.Mp[i] = EditorGUILayout.IntField("Max MP:",
                            Mathf.Clamp(_currentCurve.Mp[i], 1, _currentCurve.MaxMp));
                        
                        _currentCurve.ExpToNextLevel[i] = EditorGUILayout.IntField("Exp to next lvl:",
                            _currentCurve.ExpToNextLevel[i]);
                        
                        EditorGUIUtility.labelWidth = 90;
                        GUILayout.Space(20);
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUIUtility.labelWidth = 0;
            }
            if (!EditorGUI.EndChangeCheck()) return;
            
            EditorUtility.SetDirty(_currentCurve);
        }

        private static void RefreshCurves()
        {
            UtilityHelper.CurveNameList.Clear();
            UtilityHelper.CurveNameListRaw.Clear();
            UtilityHelper.CurveGuidList.Clear();
            UtilityHelper.CurveAssetList.Clear();
            
            UtilityHelper.GetAssetData(5);
        }
    }
}
