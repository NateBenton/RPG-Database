using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    public class Equipment : ScriptableObject
    {
        public string Name;
        public int AttackModifier, DefenseModifier, HpModifier, MpModifier;
    }
}
