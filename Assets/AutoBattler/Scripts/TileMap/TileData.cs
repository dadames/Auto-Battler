using UnityEngine;
using UnityEngine.Tilemaps;


namespace AutoBattler
{
    [CreateAssetMenu(fileName = "MapTileData", menuName = "Scriptable Objects/MapTileData", order = 3)]
    public class MapTileData : ScriptableObject
    {
        public TileBase[] Tiles;
        public int Cost;
        public bool IsBuildable;
        public bool IsBlocker;
    }
}
