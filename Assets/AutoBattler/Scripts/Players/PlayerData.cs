using UnityEngine;

namespace AutoBattler
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData", order = 2)]
    public class PlayerData : ScriptableObject
    {
        public int PlayerId;
    }
}
