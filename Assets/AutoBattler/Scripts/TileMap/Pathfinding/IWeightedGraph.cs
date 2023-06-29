using System.Collections.Generic;

namespace AutoBattler
{
    public interface IWeightedGraph<L>
    {
        IEnumerable<MapTile> Neighbours(int id);
    }
}
