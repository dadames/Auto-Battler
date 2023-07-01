using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitManager : MonoBehaviour
    {
        private Unit _unit;
        public Unit Unit => _unit;
        private Rigidbody2D _rigidBody;
        [SerializeField] Sprite _normalSprite;
        [SerializeField] Sprite _debuggingSprite;
        [SerializeField] SpriteRenderer _renderer;

        private bool _initialized = false;
        private List<int> _path;
        private int _destinationTile;

        private void OnEnable()
        {
            EventManager.AddListener("UpdatePathfinding", _OnUpdatePathfinding);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener("UpdatePathfinding", _OnUpdatePathfinding);
        }

        public void Initialize(Unit unit, Vector2Int gridPosition)
        {
            _destinationTile = 17;
            _unit = unit;
            _rigidBody = GetComponent<Rigidbody2D>();
            SetPosition(gridPosition);            
            _OnUpdatePathfinding();
            foreach (int tile in _path)
                MapManager.Instance.IdToMapTile[tile].HighlightTile();
            _initialized = true;
        }

        private void FixedUpdate()
        {
            if (_initialized == false) return;

            _PathFinding();
        }

        private void _OnUpdatePathfinding()
        {
            _path = AStarSearch.Search(MapManager.Instance.PathfindingGrid, _unit.ParentTile, MapManager.Instance.IdToMapTile[_destinationTile]);
        }

        private void _PathFinding()
        {          
            if (_path == null || _path.Count <= 1)
            {
                _renderer.sprite = _debuggingSprite;
                return;
            }
            else
            {
                _renderer.sprite = _normalSprite;
            }

            _CheckIfReachedDestination();

            Vector2 newPosition = Vector2.MoveTowards(transform.position, MapManager.Instance.IdToMapTile[_path[0]].Position, Time.deltaTime * _unit.Speed* Globals.MOVEMENT_SPEED_SCALING);
            _rigidBody.MovePosition(newPosition);
            SetOccupiedTile();
        }

        public void _CheckIfReachedDestination()
        {
            if (_unit.ParentTile.Id == _path[0])
            {
                if (_path.Count <= 1) return;
                _path.RemoveAt(0);
            }
        }

        public void SetPosition(Vector2Int position)
        {
            MapTile placedOnTile = MapManager.Instance.GetTileAtPosition(position);

            transform.position = MapManager.Instance.TileToWorldSpace(placedOnTile.GridLocation);
        }

        public void SetOccupiedTile()
        {
            if (MapManager.Instance.WorldSpaceIsMapTile(this.transform.position))
            {
                MapTile placedOnTile = GetTileAtWorldPosition().MapTile;
                if (GetTileAtWorldPosition().MapTile != _unit.ParentTile)
                {
                    MapTile formerTile = _unit.ParentTile;

                    _unit.SetParentTile(placedOnTile);
                    placedOnTile.MoveUnitToTile(this);
                    formerTile.ClearTile();
                }
            }            
        }

        public void SetDestination(int destinationTile)
        {
            _destinationTile = destinationTile;
            _OnUpdatePathfinding();
        }

        public void Attack(UnitManager target)
        {
            target.Damage(1);
        }

        public void Damage(int damage)
        {
            //Debug.Log($"{Unit.OwnerId} taking {damage} damage");
        }

        public MapTileManager GetTileAtWorldPosition()
        {
            MapTileManager placedOnTile = MapManager.Instance.WorldSpaceToTile(this.transform.position).MapTileManager;
            return placedOnTile;
        }
    }
}
