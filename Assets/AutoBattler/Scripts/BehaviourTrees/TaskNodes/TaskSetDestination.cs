using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoBattler.AI;

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
            MapTile destinationTile = (MapTile)GetData("destinationMapTile");

            _unitManager.SetDestination(destinationTile.Id);

            _state = NodeState.SUCCESS;
            return _state;
        }
    }
}
