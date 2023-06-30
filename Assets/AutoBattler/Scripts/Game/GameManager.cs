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
            SpawnUnit(0, 10);
            SpawnUnit(0, 110);
        }

        public void SpawnUnit(int unitId, int mapTileId)
        {
            UnitData unitData = Globals.UNIT_DATA.Where((UnitData x) => x.UnitId == unitId).First();
            Unit die = new(unitData, mapTileId);
        }
    }
}
