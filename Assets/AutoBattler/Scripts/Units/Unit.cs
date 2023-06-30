using UnityEngine;

namespace AutoBattler
{
    public class Unit
    {
        private Transform _transform;
        private UnitManager _unitManager;

        private MapTile _parentTile;
        public MapTile ParentTile => _parentTile;
        private int _speed;
        public int Speed => _speed;


        public Unit(UnitData data, int mapTileId)
        {
            GameObject g = GameObject.Instantiate(data.Prefab);
            _transform = g.transform;
            _transform.name = $"{data.UnitId}";
            _unitManager = g.transform.GetComponent<UnitManager>();
            _speed = data.Speed;
            _parentTile = MapManager.Instance.IdToMapTile[mapTileId];

            _unitManager.Initialize(this, _parentTile.GridLocation);
        }

        public void SetParentTile(MapTile tile)
        {
            _parentTile = tile;
            if (_parentTile == null) Debug.LogError($"{_transform.name} has no parent tile.");
        }
    }
}
