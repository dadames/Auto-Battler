using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitManager : MonoBehaviour
    {
        private Unit _unit;
        private Rigidbody2D _rigidBody;

        private bool _initialized = false;
        public Dictionary<int, int> path;

        public void Initialize(Unit unit, Vector2Int position)
        {
            _unit = unit;
            _rigidBody = GetComponent<Rigidbody2D>();
            SetPosition(position);
            _initialized = true;
            path = AStarSearch.Search(MapManager.Instance.AdjacencyMap, _unit.ParentTile, MapManager.Instance.IntToTile[20]);
            foreach (KeyValuePair<int, int> pair in path)
            {
                Debug.Log($"{pair.Key}, {pair.Value}");
            }
        }

        private void FixedUpdate()
        {
            if (_initialized == false) return;

            if (_unit.ParentTile.Id == path[0])
            {
                Debug.Log($"Reached {path[0]} moving to {path[1]}");
                path.Remove(0);                
            }

            Vector2 newPosition = Vector2.MoveTowards(transform.position, MapManager.Instance.IntToTile[path[0]].Position, Time.deltaTime * _unit.Speed);
            _rigidBody.MovePosition(newPosition);
            SetOccupiedTile();
        }

        public void SetPosition(Vector2Int position)
        {
            MapTile placedOnTile = MapManager.Instance.GetTileAtPosition(position);

            transform.position = MapManager.Instance.TileToWorldSpace(placedOnTile.GridLocation);
        }

        public void SetOccupiedTile()
        {
            MapTile placedOnTile = GetTileAtPosition();

            if (placedOnTile != _unit.ParentTile)
            {
                MapTile formerTile = _unit.ParentTile;
                _unit.SetParentTile(placedOnTile);
                placedOnTile.MoveUnitToTile(this);
                formerTile.ClearTile();
            }
        }

        public MapTile GetTileAtPosition()
        {
            MapTile placedOnTile = Physics2D.OverlapCircle(transform.position, 0.1f).GetComponent<MapTile>();
            return placedOnTile;
        }
    }
}
