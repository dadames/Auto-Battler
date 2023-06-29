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
        private UnitManager _occupyingUnit;
        public UnitManager OccupyingUnit => _occupyingUnit;
        private int _id;
        public int Id => _id;
        private bool _isBuildable;
        private bool _isBlocker;
        public bool IsBlocked
        {
            get { return !_isBlocker || _occupyingUnit != null; }
        }


        public MapTile(GameObject prefab, int id, Transform position, Vector2 cellWorldPos, MapTileData data, Vector2Int gridPosition)
        {
            GameObject g = GameObject.Instantiate(prefab, position);
            _mapTileManager = g.GetComponent<MapTileManager>();
            _id = id;
            _gridLocation = gridPosition;
            g.transform.position = new(cellWorldPos.x, cellWorldPos.y, 0);
            g.transform.name = $"{_gridLocation}";
            _isBlocker = data.IsBlocker;
            _isBuildable = data.IsBuildable;
            _mapTileManager.Initialize(this);
        }

        public void MoveUnitToTile(UnitManager unit)
        {
            _occupyingUnit = unit;
        }

        public void ClearTile()
        {
            _occupyingUnit = null;
        }

        public void HighlightTile()
        {
            _mapTileManager.ShowTile();
        }
    }
}
