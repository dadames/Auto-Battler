using System.Collections.Generic;
using System.Linq;
using AutoBattler.AI;

namespace AutoBattler
{
    public class CheckEnemyInRange : Node
    {
        UnitManager _unitManager;

        public CheckEnemyInRange(UnitManager manager) : base()
        {
            _unitManager = manager;
        }


        public override NodeState Evaluate()
        {
            List<UnitManager> enemiesInRange = new();
            //Pathfinding check each tile within range until enemy is found
            //Set closest enemy

            if (enemiesInRange.Any())
            {
                _state = NodeState.RUNNING;
                return _state;
            }


            _state = NodeState.FAILURE;
            return _state;
        }
    }
}
