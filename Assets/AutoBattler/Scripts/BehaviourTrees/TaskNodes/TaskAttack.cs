using AutoBattler.AI;
using UnityEngine;

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
            //Debug.Log($"{_unitManager.Unit.OwnerId} TaskAttack");
            UnitManager targetUnit = (UnitManager)GetData("targetUnit");

            _unitManager.Attack(targetUnit);

            _state = NodeState.SUCCESS;
            return _state;
        }
    }
}
