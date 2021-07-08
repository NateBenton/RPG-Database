using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Hero Class", menuName = "NBGames/Hero Class")]
    public class HeroClass : ScriptableObject
    {
        public string ClassName;
    }
}
