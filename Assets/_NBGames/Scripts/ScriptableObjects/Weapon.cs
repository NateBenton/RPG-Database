using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "NBGames/Weapon")]
    public class Weapon : ScriptableObject
    {
        public string WeaponName;
    }
}
