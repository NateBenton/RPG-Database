using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Hero", menuName = "NBGames/Hero")]
    public class Hero : ScriptableObject
    {
        public string HeroName;
        public HeroClass Class;
        public int ClassIndex;
    }
}
