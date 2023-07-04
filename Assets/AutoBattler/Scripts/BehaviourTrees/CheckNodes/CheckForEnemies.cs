using System.Linq;
using System.Collections.Generic;
using AutoBattler.AI;
using UnityEngine;

namespace AutoBattler
{
    public class CheckForEnemies : Node
    {
        Unit _unit;

        public CheckForEnemies(UnitManager manager) : base()
        {
            _unit = manager.Unit;
        }


        public override NodeState Evaluate()
        {
            Debug.Log($"{_unit.OwnerId} CheckForEnemies");
            Root.ClearData("destinationMapTileId");
            List<Unit> enemiesOnMap = TilemapUtilities.FindUnitsOnMapByOwnerId(_unit.EnemyIds);            

            if (enemiesOnMap.Any())
            {

                Unit destinationEnemy = TilemapUtilities.FindClosestUnitOnMapByOwnerId(_unit.ParentTile,_unit.EnemyIds);
                Root.SetData("destinationMapTileId", destinationEnemy.ParentTile.Id);

                _state = NodeState.SUCCESS;
                return _state;
            }

            _state = NodeState.FAILURE;
            return _state;
        }
    }
}
