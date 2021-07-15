using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "NBGames/Skill")]
    public class Skill : ScriptableObject
    {
        public string Name, Description;
        public int MpCost;
    }
}
