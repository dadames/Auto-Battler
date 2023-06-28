using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            MapManager.Instance.GenerateMap();
        }
        private void Start()
        {
            SpawnUnit(0, new Vector2Int(0,0));
        }

        public void SpawnUnit(int unitId, Vector2Int unitPos)
        {
            UnitData unitData = Globals.UNIT_DATA.Where((UnitData x) => x.UnitId == unitId).First();
            Unit die = new(unitData, unitPos);
        }
    }
}
