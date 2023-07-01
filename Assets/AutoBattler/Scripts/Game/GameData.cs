using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData", order = 1)]

    public class LevelData : ScriptableObject
    {
        public int LevelId;
        public List<PlayerDataWithTeam> Players;

    }

    [System.Serializable]
    public class PlayerDataWithTeam
    {
        public PlayerData PlayerData;
        public int TeamId;
    }
}
