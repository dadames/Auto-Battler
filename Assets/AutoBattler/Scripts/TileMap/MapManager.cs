using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AutoBattler
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance => _instance;

        
        [SerializeField] private GameObject _mapTilePrefab;
        [SerializeField] private GameObject _tileParent;
        [SerializeField] private Tilemap _tileMap;


        private readonly Dictionary<TileBase, MapTileData> _tileBaseToData = new(); //Keeps track of base tiles used to draw the map and their respective data
        private readonly Dictionary<int, MapTile> _idToMapTile = new();
        public Dictionary<int, MapTile> IdToMapTile => _idToMapTile;
        private readonly Dictionary<Vector2, MapTile> _worldPositionToMapTile = new();
        private readonly Dictionary<Vector2Int, MapTile> _gridLocationToMapTile = new();
        private readonly SquareGrid _pathfindingGrid = new();
        public SquareGrid PathfindingGrid => _pathfindingGrid;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            foreach (MapTileData data in Resources.LoadAll<MapTileData>(Globals.MAPTILE_DATA_FOLDER))
                foreach (TileBase tile in data.Tiles)
                    _tileBaseToData.Add(tile, data);
        }

        public void GenerateMap()
        {
            _CreateMapTiles();
            _CreatePathfindingGrid();
        }

        private void _CreateMapTiles()
        {
            BoundsInt bounds = _tileMap.cellBounds;
            int idIterator = 0;

            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        Vector3Int gridLocation = new(x, y, z);

                        if (_tileMap.HasTile(gridLocation))
                        {
                            MapTileData data = GetMapTileDataFromPlacedTile(gridLocation);
                            Vector2 cellWorldPos = TileToWorldSpace(gridLocation);

                            MapTile mapTile = new(_mapTilePrefab, idIterator, _tileParent.transform, cellWorldPos, data, (Vector2Int)gridLocation);

                            _idToMapTile.Add(mapTile.Id, mapTile);
                            _worldPositionToMapTile.Add(mapTile.Position, mapTile);
                            _gridLocationToMapTile.Add(mapTile.GridLocation, mapTile);
                            idIterator++;                            
                        }
                    }
                }
            }
        }

        private void _CreatePathfindingGrid()
        {
            foreach (KeyValuePair<int, MapTile> pair in IdToMapTile)
            {
                _pathfindingGrid.Add(pair.Key, _GetAdjacencies(pair.Value.GridLocation));
            }
        }

        private Dictionary<int, int> _GetAdjacencies(Vector2Int from)
        {
            Dictionary<int, int> adjacencies = new();

            foreach (Vector2Int direction in Globals.TILEMAP_STRAIGHT_MOVES)
            {
                if (_gridLocationToMapTile.ContainsKey(from + direction))
                    adjacencies.Add(GetTileAtPosition(from + direction).Id, GetTileAtPosition(from + direction).Cost);
            }
            foreach (Vector2Int direction in Globals.TILEMAP_DIAGONAL_MOVES)
            {
                if (_gridLocationToMapTile.ContainsKey(from + direction))
                    adjacencies.Add(GetTileAtPosition(from + direction).Id, GetTileAtPosition(from + direction).Cost + 1);
            }

            return adjacencies;
        }

        public Vector2 TileToWorldSpace(Vector2Int pos)
        {
            return TileToWorldSpace((Vector3Int)pos);
        }

        public Vector2 TileToWorldSpace(Vector3Int position)
        {
            Vector2 vec = _tileMap.GetCellCenterWorld(position);

            return vec;
        }

        public bool WorldSpaceIsMapTile(Vector2 position)
        {
            return _worldPositionToMapTile.ContainsKey(position);
        }

        public MapTile WorldSpaceToTile(Vector2 position)
        {
            return _worldPositionToMapTile[position];
        }

        public MapTileData GetMapTileDataFromPlacedTile(Vector3Int position)
        {
            TileBase tileBase = _tileMap.GetTile(position);

            if (!_tileBaseToData.ContainsKey(tileBase))
            {
                Debug.LogError("Referencing null key.");
                return null;
            }
            else
            {
                return _tileBaseToData[tileBase];
            }
        }

        public MapTile GetTileAtPosition(Vector2Int position)
        {
            MapTile tileToReturn;

            tileToReturn = _gridLocationToMapTile[position];
            if (tileToReturn == null) { Debug.LogError("Referencing point off map."); }

            return tileToReturn;
        }
    }
}
