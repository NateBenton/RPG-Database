using UnityEngine;

namespace _NBGames.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Party", menuName = "NBGames/Party")]
    public class StartingParty : ScriptableObject
    {
        public Hero[] Party = new Hero[3];
        public int[] PartyIndexes = new int[3];
    }
}
