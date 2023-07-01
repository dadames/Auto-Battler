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
            Root.ClearData("destinationMapTileId");
           

            List<UnitManager> enemiesOnMap = TilemapUtilities.FindUnitsOnMapByOwnerId(_unit.EnemyIds);
            

            if (enemiesOnMap.Any())
            {
                UnitManager destinationEnemy = TilemapUtilities.FindClosestUnitOnMapByOwnerId(_unit.ParentTile,_unit.EnemyIds);
                Root.SetData("destinationMapTileId", destinationEnemy.Unit.ParentTile.Id);
                _state = NodeState.SUCCESS;
                return _state;
            }


            _state = NodeState.FAILURE;
            return _state;
        }
    }
}
