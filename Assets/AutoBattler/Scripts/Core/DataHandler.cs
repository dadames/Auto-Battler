using UnityEngine;

namespace AutoBattler
{
    public class DataHandler : MonoBehaviour
    {
        private void Awake()
        {
            LoadGameData();
        }

        public void LoadGameData()
        {
            Globals.UNIT_DATA = Resources.LoadAll<UnitData>(Globals.UNIT_DATA_FOLDER);
            Globals.LEVEL_DATA = Resources.LoadAll<LevelData>(Globals.LEVEL_DATA_FOLDER);

            //TileMap Directions
            Globals.TILEMAP_STRAIGHT_MOVES.Add(new Vector2Int(0, -1));
            Globals.TILEMAP_STRAIGHT_MOVES.Add(new Vector2Int(-1, 0));
            Globals.TILEMAP_STRAIGHT_MOVES.Add(new Vector2Int(0, 1));
            Globals.TILEMAP_STRAIGHT_MOVES.Add(new Vector2Int(1, 0));

            Globals.TILEMAP_DIAGONAL_MOVES.Add(new Vector2Int(-1, -1));
            Globals.TILEMAP_DIAGONAL_MOVES.Add(new Vector2Int(1, 1));
            Globals.TILEMAP_DIAGONAL_MOVES.Add(new Vector2Int(-1, 1));
            Globals.TILEMAP_DIAGONAL_MOVES.Add(new Vector2Int(1, -1));
        }
    }
}
