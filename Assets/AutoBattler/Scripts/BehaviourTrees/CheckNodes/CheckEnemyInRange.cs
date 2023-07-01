using System.Collections.Generic;
using System.Linq;
using AutoBattler.AI;

namespace AutoBattler
{
    public class CheckEnemyInRange : Node
    {
        Unit _unit;

        public CheckEnemyInRange(UnitManager manager) : base()
        {
            _unit = manager.Unit;
        }
        
        public override NodeState Evaluate()
        {
            List<UnitManager> enemiesInRange = TilemapUtilities.FindUnitsInRangeByOwnerId(_unit.ParentTile, _unit.AttackRange, _unit.EnemyIds);

            if (enemiesInRange.Any())
            {
                Root.SetData("targetUnit", TilemapUtilities.FindClosestUnitByOwnerId(_unit.ParentTile, _unit.AttackRange, _unit.EnemyIds));
                _state = NodeState.SUCCESS;
                return _state;
            }


            _state = NodeState.FAILURE;
            return _state;
        }
    }
}
