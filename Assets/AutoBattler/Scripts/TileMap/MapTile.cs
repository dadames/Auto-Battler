using UnityEngine;

namespace AutoBattler
{
    public class MapTile
    {
        private MapTileManager _mapTileManager;
        public MapTileManager MapTileManager => _mapTileManager;
        public Vector2 Position => _mapTileManager.transform.position;

        private int _cost;
        public int Cost => _cost;
        private Vector2Int _gridLocation;
        public Vector2Int GridLocation => _gridLocation;
        private UnitManager _claimingUnit;
        public UnitManager ClaimingUnit => _claimingUnit;
        private UnitManager _occupyingUnit;
        public UnitManager OccupyingUnit => _occupyingUnit;
        private int _id;
        public int Id => _id;
        private bool _isBuildable;
        private bool _isBlocker;
        public bool IsBlocker => _isBlocker;
        public bool ContainsUnit { get => _occupyingUnit != null; }
        public bool IsBlocked
        {
            get { return _isBlocker || _occupyingUnit != null; }
        }


        public MapTile(GameObject prefab, int id, Transform position, Vector2 cellWorldPos, MapTileData data, Vector2Int gridPosition)
        {
            GameObject g = GameObject.Instantiate(prefab, position);
            _mapTileManager = g.GetComponent<MapTileManager>();
            _id = id;
            _gridLocation = gridPosition;
            g.transform.position = new(cellWorldPos.x, cellWorldPos.y, 0);
            g.transform.name = $"{_gridLocation}";
            _cost = data.Cost;
            _isBlocker = data.IsBlocker;
            _isBuildable = data.IsBuildable;
            _mapTileManager.Initialize(this);
        }

        public bool MoveUnitToTile(UnitManager unitManager)
        {
            if (_isBlocker) Debug.LogError($"Unit moved to blocker tile {_id}");

            if (_occupyingUnit == null)
            {
                _occupyingUnit = unitManager;
                unitManager.Unit.SetParentTile(this);
                EventManager.TriggerEvent("UpdatePathfinding");
                return true;
            }
            else if (_occupyingUnit == unitManager)
            {
                return true;
            }

            return false;
            
        }
        public bool ClaimTile(UnitManager unit)
        {
            if (_isBlocker) Debug.LogError($"Unit moved to blocker tile {_id}");

            if (_claimingUnit == null || _claimingUnit == unit)
            {
                _claimingUnit = unit;
                return true;
            }

            return false;
        }

        public void ClearTile()
        {
            _occupyingUnit = null;
            EventManager.TriggerEvent("UpdatePathfinding");
        }

        public void HighlightTile()
        {
            _mapTileManager.ShowTile();
        }
    }
}
