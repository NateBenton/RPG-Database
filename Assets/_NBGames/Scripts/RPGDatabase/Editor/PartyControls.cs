using _NBGames.Scripts.RPGDatabase.Utilities;
using _NBGames.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Editor
{
    public class PartyControls
    {
        private StartingParty _startingParty;

        public void GeneralSettings()
        {
            _startingParty = UtilityHelper.StartingParty;
            EditorGUI.BeginChangeCheck();
            {
                UtilityHelper.RemoveDeletedHeroesFromParty();
                
                Undo.RecordObject(_startingParty, "Changes to party");
                EditorGUIUtility.labelWidth = 75;
                        
                _startingParty.PartyIndexes[0] = EditorGUILayout.Popup("Member 1:", _startingParty.PartyIndexes[0],
                    UtilityHelper.HeroNameListMod.ToArray());
                
                _startingParty.PartyIndexes[1] = EditorGUILayout.Popup("Member 2:", _startingParty.PartyIndexes[1],
                    UtilityHelper.HeroNameListMod.ToArray());
                
                _startingParty.PartyIndexes[2] = EditorGUILayout.Popup("Member 3:", _startingParty.PartyIndexes[2],
                    UtilityHelper.HeroNameListMod.ToArray());
                
                EditorGUIUtility.labelWidth = 0;
                
            }
            if (!EditorGUI.EndChangeCheck()) return;

            _startingParty.Party[0] = _startingParty.PartyIndexes[0] != 0
                ? _startingParty.Party[0] = UtilityHelper.HeroAssetList[_startingParty.PartyIndexes[0] - 1]
                : null;
            
            _startingParty.Party[1] = _startingParty.PartyIndexes[1] != 0
                ? _startingParty.Party[1] = UtilityHelper.HeroAssetList[_startingParty.PartyIndexes[1] - 1]
                : null;
            
            _startingParty.Party[2] = _startingParty.PartyIndexes[2] != 0
                ? _startingParty.Party[2] = UtilityHelper.HeroAssetList[_startingParty.PartyIndexes[2] - 1]
                : null;
            
            EditorUtility.SetDirty(_startingParty);
        }
    }
}
