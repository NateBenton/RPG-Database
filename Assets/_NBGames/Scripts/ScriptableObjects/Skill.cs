using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "NBGames/Skill")]
    public class Skill : ScriptableObject
    {
        public string SkillName, Description;
        public int MpCost;
    }
}
