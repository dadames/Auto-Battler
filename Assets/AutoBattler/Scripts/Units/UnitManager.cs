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
        public List<int> path;

        public void Initialize(Unit unit, Vector2Int position)
        {
            _unit = unit;
            _rigidBody = GetComponent<Rigidbody2D>();
            SetPosition(position);
            _initialized = true;
            path = AStarSearch.Search(MapManager.Instance.AdjacencyMap, _unit.ParentTile, MapManager.Instance.IntToTile[20]);
            foreach (int tile in path)
            {
                MapManager.Instance.IntToTile[tile].HighlightTile();
            }
        }

        private void FixedUpdate()
        {
            if (_initialized == false) return;            

            if (_unit.ParentTile.Id == path[0])
            {
                if (path.Count <= 1) return;
                Debug.Log($"Reached {path[0]} moving to {path[1]}");
                path.RemoveAt(0);
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
            MapTile placedOnTile = GetTileAtPosition().MapTile;

            if (placedOnTile != _unit.ParentTile)
            {
                MapTile formerTile = _unit.ParentTile;
                _unit.SetParentTile(placedOnTile);
                placedOnTile.MoveUnitToTile(this);
                formerTile.ClearTile();
            }
        }

        public MapTileManager GetTileAtPosition()
        {
            MapTileManager placedOnTile = Physics2D.OverlapCircle(transform.position, 0.1f).GetComponent<MapTileManager>();
            return placedOnTile;
        }
    }
}
