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


        public Unit(UnitData data, Vector2Int position)
        {
            GameObject g = GameObject.Instantiate(data.Prefab);
            _transform = g.transform;
            _transform.name = $"{data.UnitId}";
            _unitManager = g.transform.GetComponent<UnitManager>();
            _speed = data.Speed;
            _parentTile = MapManager.Instance.GetTileAtPosition(position);

            _unitManager.Initialize(this, position);
        }

        public void SetParentTile(MapTile tile)
        {
            _parentTile = tile;
            if (_parentTile == null) Debug.LogError($"{_transform.name} has no parent tile.");
        }
    }
}
