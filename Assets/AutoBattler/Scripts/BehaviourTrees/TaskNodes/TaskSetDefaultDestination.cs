using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoBattler.AI;

namespace AutoBattler
{
    public class TaskSetDefaultDestination : Node
    {
        UnitManager _unitManager;

        public TaskSetDefaultDestination(UnitManager unitManager)
        {
            _unitManager = unitManager;
        }

        public override NodeState Evaluate()
        {
            Root.SetData("destinationMapTileId", _unitManager.Unit.DefaultDestination);

            _state = NodeState.SUCCESS;
            return _state;
        }
    }
}
