using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class SquareGrid : IWeightedGraph<MapTile>
    {
        private int _id;
        MapTile[] _adjacencies;

        public SquareGrid()
        {

        }

        public void Add(int id, MapTile[] adjacencies)
        {
            _id = id;
            _adjacencies = adjacencies;
        }

        public IEnumerable<MapTile> Neighbours()
        {
            foreach (MapTile maptile in _adjacencies)
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
