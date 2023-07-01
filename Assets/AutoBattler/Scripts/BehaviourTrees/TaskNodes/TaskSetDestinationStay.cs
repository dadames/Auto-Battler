using AutoBattler.AI;
using UnityEngine;

namespace AutoBattler
{
    public class TaskSetDestinationStay : Node
    {
        UnitManager _unitManager;

        public TaskSetDestinationStay(UnitManager unitManager)
        {
            _unitManager = unitManager;
        }

        public override NodeState Evaluate()
        {
            //Debug.Log($"{_unitManager.Unit.OwnerId} TaskSetDestinationStay");
            int destinationTileId = _unitManager.Unit.ParentTile.Id;
            Debug.Log(destinationTileId);
            _unitManager.SetDestination(destinationTileId);

            _state = NodeState.SUCCESS;
            return _state;
        }
    }
}
