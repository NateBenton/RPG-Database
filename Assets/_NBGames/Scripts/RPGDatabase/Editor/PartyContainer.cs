using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class PartyContainer
    {
        private bool _showGeneralSettings = true;
        private PartyControls _partyControls = new PartyControls();

        public void GeneralSettings()
        {
            _showGeneralSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showGeneralSettings, "General Settings");
            {
                if (_showGeneralSettings)
                {
                    EditorGUILayout.BeginVertical("Box");
                    {
                        _partyControls.GeneralSettings();
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
