using UnityEngine;

namespace AutoBattler
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData", order = 2)]
    public class UnitData : ScriptableObject
    {
        public int UnitId;
        public GameObject Prefab;
        [Range(1, 20)] public int Speed;
    }
}
