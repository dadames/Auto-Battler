using System.Collections.Generic;

namespace AutoBattler
{
    public interface IWeightedGraph<L>
    {
        int Cost(MapTile moveTo);
        IEnumerable<MapTile> Neighbours(int id);
    }
}
