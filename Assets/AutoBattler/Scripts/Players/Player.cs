using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class Player
    {
        private int _playerId;
        public int PlayerId => _playerId;
        private int _teamId;
        public int TeamId => _teamId;
        private List<int> _enemyIds;
        public List<int> EnemyIds => _enemyIds;
        private List<int> _allyIds;
        public List<int> AllyIds => _allyIds;


        public Player(PlayerData data, int teamId)
        {
            _playerId = data.PlayerId;
            _teamId = teamId;
            GameManager.Instance.Players.Add(this);
        }

        public void SetEnemiesAndAllies(List<int> enemyIds, List<int> allyIds)
        {
            _enemyIds = enemyIds;
            _allyIds = allyIds;
        }
    }
}
