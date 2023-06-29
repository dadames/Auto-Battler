using System.Collections.Generic;


namespace AutoBattler
{
    public class SquareGrid : IWeightedGraph<MapTile>
    {
        private readonly Dictionary<int, MapTile[]> grid = new();


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
    }
}
