using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

            });

            Sequence FindDestination = new(new List<Node> {
                new Sequence(new List<Node>
                {
                    new CheckForEnemies(_unitManager),
                    new TaskSetDestination(_unitManager),
                }),
                new TaskSetDestination(_unitManager),
            });


            //Build full tree
            _root = new Selector(new List<Node>
            {
                AttackNearbyEnemy,
                FindDestination,
                //new TaskMoveToDestination(),
            });

            return _root;
        }
    }
}
