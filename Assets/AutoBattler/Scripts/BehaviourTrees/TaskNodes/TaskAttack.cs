using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoBattler.AI;

namespace AutoBattler
{
    public class TaskAttack : Node
    {
        UnitManager _unitManager;

        public TaskAttack(UnitManager unitManager)
        {
            _unitManager = unitManager;
        }

        public override NodeState Evaluate()
        {
            UnitManager targetUnit = (UnitManager)GetData("targetUnit");

            _unitManager.Attack(targetUnit);

            _state = NodeState.SUCCESS;
            return _state;
        }
    }
}
