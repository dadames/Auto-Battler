using UnityEngine;

namespace AutoBattler
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData", order = 10)]
    public class UnitData : ScriptableObject
    {
        public int UnitId;
        public GameObject Prefab;
        [Range(1, 20)] public int Speed;
        [Range(0, 60)] public int AttackRange;
    }
}
