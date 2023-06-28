using UnityEngine;
using UnityEngine.Tilemaps;


namespace AutoBattler
{
    public enum TileType
    {
        Normal,
        Buildable,
        Blocking,
    }

    [CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData", order = 3)]
    public class TileData : ScriptableObject
    {
        public TileBase[] Tiles;
        public TileType TileType;
    }

}
