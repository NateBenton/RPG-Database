using System;
using _NBGames.Scripts.ScriptableObjects;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Misc
{
    [Serializable]
    public class SkillField
    {
        public Skill SkillLearned;
        public int LevelLearned;
    }
}
