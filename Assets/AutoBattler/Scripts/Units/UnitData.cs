using UnityEngine;

namespace AutoBattler
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData", order = 2)]
    public class UnitData : ScriptableObject
    {
        public int UnitId;
        public GameObject Prefab;
        public int Speed;
    }
}
