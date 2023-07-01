using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoBattler.AI;

namespace AutoBattler
{
    public class TaskSetDefaultDestination : Node
    {
        Unit _unit;

        public TaskSetDefaultDestination(UnitManager unitManager)
        {
            _unit = unitManager.Unit;
        }

        public override NodeState Evaluate()
        {
            Debug.Log($"{_unit.OwnerId} TaskSetDefaultDestination");
            Root.SetData("destinationMapTileId", _unit.DefaultDestination);

            _state = NodeState.SUCCESS;
            return _state;
        }
    }
}
