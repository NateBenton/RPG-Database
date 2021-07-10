using _NBGames.Scripts.RPGDatabase.Misc;
using _NBGames.Scripts.RPGDatabase.Utilities;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class CurveContainer
    {
        private bool _showGeneralSettings = true;
        private bool _showBaseSettings = true;
        private bool _showCurveSettings = true;
        private bool _showMaxSettings = true;
        private CurveControls _curveControls = new CurveControls();

        public void GeneralSettings()
        {
            _showGeneralSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneralSettings, "General Settings");
            {
                if (_showGeneralSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _curveControls.GeneralSettings();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public void BaseStats()
        {
            _showBaseSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showBaseSettings, "Base Settings");
            {
                if (_showBaseSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _curveControls.BaseStats();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public void MaxStats()
        {
            _showMaxSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showMaxSettings, "Max Settings");
            {
                if (_showMaxSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _curveControls.MaxStats();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        public void CurveSettings()
        {
            _showCurveSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showCurveSettings, "Curve Settings");
            {
                if (!_showCurveSettings) return;
                EditorGUILayout.BeginVertical("Box");
                {
                    _curveControls.CurveSettings();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
        public static void CreateNewCurve(string curveName)
        {
            var newCurve = ScriptableObject.CreateInstance<LevelCurve>();
            newCurve.CurveName = curveName;
            var fileName = UtilityHelper.GenerateFileName(5);
            AssetDatabase.CreateAsset(newCurve, $"Assets/_NBGames/Data/LevelCurves/{fileName}.asset");
        }
    }
}
