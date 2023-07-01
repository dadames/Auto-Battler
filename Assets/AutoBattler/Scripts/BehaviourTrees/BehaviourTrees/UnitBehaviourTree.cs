using System.Collections.Generic;
using AutoBattler.AI;

namespace AutoBattler
{
    public class UnitBehaviourTree : BehaviourTree
    {
        private UnitManager _unitManager;

        private void Awake()
        {
            _unitManager = GetComponent<UnitManager>();
        }

        protected override Node SetupTree()
        {
            Node _root;

            //Prepare Subtrees
            Sequence AttackNearbyEnemy = new(new List<Node> {
                new CheckEnemyInRange(_unitManager),
                new TaskSetDestinationStay(_unitManager),
                new TaskAttack(_unitManager),
            });

            Sequence SetDestination = new(new List<Node> {
                new Selector(new List<Node>
                {
                    new CheckForEnemies(_unitManager),
                    new TaskSetDefaultDestination(_unitManager),
                }),
                new TaskSetDestination(_unitManager),
            });


            //Build full tree
            _root = new Selector(new List<Node>
            {
                AttackNearbyEnemy,
                SetDestination,
                //new TaskMoveToDestination(),
            });

            return _root;
        }
    }
}
