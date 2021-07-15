using System.Collections.Generic;
using _NBGames.Scripts.RPGDatabase.Misc;
using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Hero Class", menuName = "NBGames/Hero Class")]
    public class HeroClass : ScriptableObject
    {
        public string Name;
        public int SkillsToLearn;
        public List<SkillField> SkillFields = new List<SkillField>();
        public LevelCurve LevelCurve;
        public int CurveIndex;
        public List<int> SkillIds = new List<int>();
        public List<int> LevelsLearned = new List<int>();
    }
}
