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

        public void Initialize(Unit unit, int mapTileId)
        {
            _unit = unit;
            _rigidBody = GetComponent<Rigidbody2D>();
            PlaceUnit(mapTileId);
            this.GetComponent<UnitBehaviourTree>().enabled = true;
            _OnUpdatePathfinding();
            _initialized = true;
        }

        private void FixedUpdate()
        {
            if (!_initialized) return;

            _PathFinding();
        }

        private void _OnUpdatePathfinding() //Runs whenever changes are made to pathfinding map
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

            MoveToTile(this.transform.position);
            _CheckIfReachedDestination();

            Vector2 newPosition = Vector2.MoveTowards(transform.position, MapManager.Instance.IdToMapTile[_path[0]].Position, Time.deltaTime * _unit.Speed* Globals.MOVEMENT_SPEED_SCALING);
            _rigidBody.MovePosition(newPosition);
            
        }

        public void _CheckIfReachedDestination() //Checks if where the tile is = destTile 
        {
            if (_path.Count <= 1) return;
            if (_unit.ParentTile.Id == _path[0])
            {
                _path.RemoveAt(0);
                if (!ClaimTile(_path[0])) 
                {
                    _OnUpdatePathfinding();
                }
               // UnitCoordinatorManager.Instance.CheckTileDibs(_unit.Uid, _path[0]);
            }
        }

        public void PlaceUnit(int mapTileId) //Moves the UnitManager GameObject to a maptile immediately
        {
            if (MoveToTile(mapTileId))
            {
                this.transform.position = MapManager.Instance.TileToWorldSpace(MapManager.Instance.IdToMapTile[mapTileId].GridLocation);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }        

        public bool MoveToTile(Vector2 position) //If this is on a world tile, try to move it
        {
            if (MapManager.Instance.WorldSpaceIsMapTile(position))
            {
                MapTile placedOnTile = GetTileAtWorldPosition().MapTile;

                return placedOnTile.MoveUnitToTile(this);
            }

            return false;
        }

        public bool MoveToTile(int mapTileId) //Try to move this to specified mapTileId
        {
            MapTile placedOnTile = MapManager.Instance.IdToMapTile[mapTileId];

            return placedOnTile.MoveUnitToTile(this);
        }

        public bool ClaimTile(int tileId) //Checks if a tile can be claimed
        {
            return MapManager.Instance.IdToMapTile[tileId].ClaimTile(this);
        }

        public MapTileManager GetTileAtWorldPosition() //Returns the tile this is on
        {
            MapTileManager placedOnTile = MapManager.Instance.WorldSpaceToTile(this.transform.position).MapTileManager;

            return placedOnTile;
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
    }
}
