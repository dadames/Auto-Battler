using System.Linq;
using System.Collections.Generic;
using AutoBattler.AI;

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
            Root.ClearData("destinationEnemy");
           

            List<UnitManager> enemiesOnMap = TilemapUtilities.FindUnitsOnMapByOwnerId(_unit.EnemyIds);
            //Find Closest Enemy Unit
            //Pathfinding check to find enemies on map
            //Set priority enemy

            if (enemiesOnMap.Any())
            {
                UnitManager destinationEnemy = TilemapUtilities.FindClosestUnitOnMapByOwnerId(_unit.ParentTile,_unit.EnemyIds);
                Root.SetData("destinationTile", destinationEnemy.Unit.ParentTile);
                _state = NodeState.RUNNING;
                return _state;
            }


            _state = NodeState.FAILURE;
            return _state;
        }
    }
}
