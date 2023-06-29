using System.Collections.Generic;
using UnityEngine;


namespace AutoBattler
{
    public class SquareGrid : IWeightedGraph<MapTile>
    {
        //Start ID[Neighbour ID, Cost]
        private readonly Dictionary<int, Dictionary<int, int>> grid = new();


        public void Add(int id, Dictionary<int, int> adjacencies) 
        {
            grid.Add(id, adjacencies);
        }

        public IEnumerable<MapTile> Neighbours(int id)
        {
            foreach (KeyValuePair<int, int> neighbourId in grid[id])
            {
                yield return MapManager.Instance.IdToMapTile[neighbourId.Key];
            }
        }

        public int Cost(int start, int end)
        {
            if (grid.ContainsKey(start))
            {
                return grid[start][end];
            }

            Debug.Log("Referencing missing MapTile ID");
            return 0;
        }
    }
}
