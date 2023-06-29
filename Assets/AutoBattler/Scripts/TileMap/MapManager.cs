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
        private readonly SquareGrid _adjacencyMap = new();
        public SquareGrid AdjacencyMap => _adjacencyMap;

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
            _CreateReverseLookupDictionaries();
            _CreateAdjacencyMapDictionary();
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
                            idIterator++;                            
                        }
                    }
                }
            }
        }

        private void _CreateReverseLookupDictionaries()
        {
            foreach (KeyValuePair<int, MapTile> pair in IdToMapTile)
            {
                _worldPositionToMapTile.Add(pair.Value.Position, pair.Value);
                _gridLocationToMapTile.Add(pair.Value.GridLocation, pair.Value);
            }
        }

        private void _CreateAdjacencyMapDictionary()
        {
            foreach (KeyValuePair<int, MapTile> from in IdToMapTile)
                _adjacencyMap.Add(GetTileAtPosition(from.Value.GridLocation).Id, _GetAdjacencies(from.Value.GridLocation));
        }

        private Dictionary<int, int> _GetAdjacencies(Vector2Int from)
        {
            Dictionary<int, int> adjacencies = new();
            
            if (_gridLocationToMapTile.ContainsKey(from + new Vector2Int(0, -1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(0, -1)).Id, GetTileAtPosition(from + new Vector2Int(0, -1)).Cost);
            if (_gridLocationToMapTile.ContainsKey(from + new Vector2Int(-1, 0))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(-1, 0)).Id, GetTileAtPosition(from + new Vector2Int(-1, 0)).Cost);
            if (_gridLocationToMapTile.ContainsKey(from + new Vector2Int(0, 1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(0, 1)).Id, GetTileAtPosition(from + new Vector2Int(0, 1)).Cost);
            if (_gridLocationToMapTile.ContainsKey(from + new Vector2Int(1, 0))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(1, 0)).Id, GetTileAtPosition(from + new Vector2Int(1, 0)).Cost);
            if (_gridLocationToMapTile.ContainsKey(from + new Vector2Int(-1, -1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(-1, -1)).Id, GetTileAtPosition(from + new Vector2Int(-1, -1)).Cost + 1);
            if (_gridLocationToMapTile.ContainsKey(from + new Vector2Int(1, 1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(1, 1)).Id, GetTileAtPosition(from + new Vector2Int(1, 1)).Cost + 1);
            if (_gridLocationToMapTile.ContainsKey(from + new Vector2Int(-1, 1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(-1, 1)).Id, GetTileAtPosition(from + new Vector2Int(-1, 1)).Cost + 1);
            if (_gridLocationToMapTile.ContainsKey(from + new Vector2Int(1, -1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(1, -1)).Id, GetTileAtPosition(from + new Vector2Int(1, -1)).Cost + 1);

            return adjacencies;
        }

        public Vector3 TileToWorldSpace(Vector2Int pos)
        {
            return TileToWorldSpace((Vector3Int)pos);
        }

        public Vector3 TileToWorldSpace(Vector3Int position)
        {
            Vector3 vec = _tileMap.GetCellCenterWorld(position);
            vec += ((position.x + position.y)) * Vector3.forward;

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
