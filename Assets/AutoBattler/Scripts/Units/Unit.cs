using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class Unit
    {
        private Transform _transform;
        private UnitManager _unitManager;
        private int _uid;
        public int Uid => _uid;
        private int _ownerId;
        public int OwnerId => _ownerId;

        private MapTile _parentTile;
        public MapTile ParentTile => _parentTile;
        private int _defaultDestination = 0;
        public int DefaultDestination => _defaultDestination;
        private int _speed;
        public int Speed => _speed;
        private int _attackRange;
        public int AttackRange => _attackRange;
        private List<int> _enemyIds;
        public List<int> EnemyIds => _enemyIds;


        public Unit(UnitData data, int uid, int ownerId, List<int> enemyIds, int mapTileId)
        {
            GameObject g = GameObject.Instantiate(data.Prefab);
            _transform = g.transform;
            _transform.name = $"{data.UnitTypeId}";
            _unitManager = g.transform.GetComponent<UnitManager>();
            

            _uid = uid;
            _ownerId = ownerId;
            _enemyIds = enemyIds;

            _speed = data.Speed;
            _attackRange = data.AttackRange;

            _unitManager.Initialize(this, mapTileId);
        }

        public void SetParentTile(MapTile tile)
        {
            MapTile oldTile = tile;
            _parentTile = tile;
            if (_parentTile != oldTile)
            {
                oldTile.ClearTile();
            }
           
            if (!UnitCoordinatorManager.Instance.UnitsOnMap.Contains(this))
            {
                UnitCoordinatorManager.Instance.UnitsOnMap.Add(this);
            }
            
                
            if (_parentTile == null) Debug.LogError($"{_transform.name} has no parent tile.");
        }
    }
}
