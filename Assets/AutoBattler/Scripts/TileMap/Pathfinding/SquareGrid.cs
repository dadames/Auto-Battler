using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class SquareGrid : IWeightedGraph<MapTile>
    {
        Dictionary<int, MapTile[]> grid = new();


        public void Add(int id, MapTile[] adjacencies)
        {
            grid.Add(id, adjacencies);
        }

        public IEnumerable<MapTile> Neighbours(int id)
        {
            foreach (MapTile maptile in grid[id])
            {
                yield return maptile;
            }
        }

        public int Cost(MapTile moveTo)
        {
            return moveTo.Cost;
        }
    }
}
