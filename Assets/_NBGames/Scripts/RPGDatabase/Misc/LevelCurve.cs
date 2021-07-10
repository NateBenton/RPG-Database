using System;
using System.Collections.Generic;
using UnityEngine;

namespace _NBGames.Scripts.RPGDatabase.Misc
{
    [CreateAssetMenu(fileName = "New Level Curve", menuName = "NBGames/Level Curve")]
    public class LevelCurve : ScriptableObject
    {
        public string CurveName;
        public int AmountOfLevels;
        public float AttackMultiplier = 1.07f, DefenseMultiplier = 1.07f;
        public float MaxHpMultiplier = 1.14f, MaxMpMultiplier = 1.105f, ExpMultiplier = 1.2f;

        public int BaseAttack = 10, BaseDefense = 8, BaseHp = 25, BaseMp = 10, BaseExp = 100;

        public int MaxAttack = 255, MaxDefense = 255, MaxHp = 9999, MaxMp = 999;
        
        public List<int> Hp = new List<int>(), Mp = new List<int>(), 
            Attack = new List<int>(), Defense = new List<int>(), ExpToNextLevel = new List<int>();
    }
}
