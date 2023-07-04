using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    public class UnitCoordinatorManager : MonoBehaviour
    {
        private static UnitCoordinatorManager _instance;
        public static UnitCoordinatorManager Instance => _instance;
        private List<Unit> _allUnits = new();
        public List<Unit> AllUnits => _unitsOnMap;
        private List<Unit> _unitsOnMap = new();
        public List<Unit> UnitsOnMap => _unitsOnMap;
        private int _unitUidIterator;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        public void SpawnUnit(int UnitTypeId, int ownerId, List<int> enemyIds, int mapTileId)
        {
            UnitData unitData = Globals.UNIT_DATA.Where((UnitData x) => x.UnitTypeId == UnitTypeId).First();
            Unit unit = new(unitData, _unitUidIterator, ownerId, enemyIds, mapTileId);
            _unitUidIterator++;
        }
    }
}
