using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoBattler
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        private List<Player> _players = new();
        public List<Player> Players => _players;


        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            MapManager.Instance.GenerateMap();
            _LoadLevelData(0);
            _SetPlayerTeams();
        }

        private void Start()
        {
            foreach (Player player in _players)
            {
                if (player.PlayerId == 0)
                {
                    SpawnUnit(0, player.PlayerId, player.EnemyIds, 10);
                }
                if (player.PlayerId == 1)
                {
                    SpawnUnit(0, player.PlayerId, player.EnemyIds, 110);
                }
            }
        }

        public void SpawnUnit(int unitId, int ownerId, List<int> enemyIds, int mapTileId)
        {
            UnitData unitData = Globals.UNIT_DATA.Where((UnitData x) => x.UnitId == unitId).First();
            Unit unit = new(unitData, ownerId, enemyIds, mapTileId);
        }

        private void _LoadLevelData(int levelId)
        {
            LevelData levelData = Globals.LEVEL_DATA.Where((LevelData x) => x.LevelId == levelId).First();
            foreach (PlayerDataWithTeam playerDataWithTeam in levelData.Players)
            {
                Player player = new(playerDataWithTeam.PlayerData, playerDataWithTeam.TeamId);
            }
        }

        private void _SetPlayerTeams()
        {
            foreach (Player player in _players)
            {
                List<int> enemyIds= new();
                List<int> allyIds = new();

                foreach (Player otherPlayer in _players)
                {
                    if (otherPlayer != player && otherPlayer.TeamId != player.TeamId)
                    {
                        enemyIds.Add(otherPlayer.PlayerId);
                    }
                    else if (otherPlayer != player && otherPlayer.TeamId == player.TeamId)
                    {
                        allyIds.Add(otherPlayer.PlayerId);
                    }
                }

                player.SetEnemiesAndAllies(enemyIds, allyIds);
            }
        }
    }
}
