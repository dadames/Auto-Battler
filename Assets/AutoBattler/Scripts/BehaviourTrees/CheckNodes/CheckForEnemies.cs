using System.Collections.Generic;
using System.Linq;
using AutoBattler.AI;

namespace AutoBattler
{
    public class CheckForEnemies : Node
    {
        UnitManager _unitManager;

        public CheckForEnemies(UnitManager manager) : base()
        {
            _unitManager = manager;
        }


        public override NodeState Evaluate()
        {
            Root.ClearData("destinationEnemy");
            UnitManager destinationEnemy = null;

            List<UnitManager> enemiesOnMap = new();
            //Find Closest Enemy Unit
            //Pathfinding check to find enemies on map
            //Set priority enemy

            if (destinationEnemy != null)
            {
                Root.SetData("destinationTile", destinationEnemy.Unit.ParentTile);
                _state = NodeState.RUNNING;
                return _state;
            }


            _state = NodeState.FAILURE;
            return _state;
        }
    }
}
