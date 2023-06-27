using UnityEngine;
using UnityEngine.Tilemaps;


namespace AutoBattler
{
    public enum TileType
    {
        Normal,
        Buildable,
    }

    [CreateAssetMenu(fileName = "TileType", menuName = "Scriptable Objects/TileType", order = 3)]
    public class TileData : ScriptableObject
    {
        public TileBase[] Tiles;
        public TileType TileType;
    }

}
