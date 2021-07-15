using _NBGames.Scripts.RPGDatabase.Misc;
using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "NBGames/Hero")]
    public class Hero : ScriptableObject
    {
        public string Name, Description;
        public HeroClass Class;
        public Weapon Weapon;
        public Armor Armor;
        public Stats Stats;
        public Sprite Sprite;
        
        public int ClassIndex;
        public int WeaponIndex;
        public int ArmorIndex;
    }
}
