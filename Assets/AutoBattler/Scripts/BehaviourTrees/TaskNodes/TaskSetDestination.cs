using AutoBattler.AI;
using UnityEngine;

namespace AutoBattler
{
    public class TaskSetDestination : Node
    {
        UnitManager _unitManager;

        public TaskSetDestination(UnitManager unitManager)
        {
            _unitManager = unitManager;
        }

        public override NodeState Evaluate()
        {
            int destinationTileId = (int)GetData("destinationMapTileId");
            Debug.Log(destinationTileId);
            _unitManager.SetDestination(destinationTileId);

            _state = NodeState.SUCCESS;
            return _state;
        }
    }
}
