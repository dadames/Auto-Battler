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
        public Tilemap TileMap => _tileMap;

        private readonly Dictionary<TileBase, MapTileData> _tileToData = new();
        private readonly Dictionary<Vector2Int, MapTile> _map = new();
        public Dictionary<Vector2Int, MapTile> Map => _map;
        private readonly Dictionary<int, MapTile> _intToTile = new();
        public Dictionary<int, MapTile> IntToTile => _intToTile;
        private readonly SquareGrid _adjacencyMap = new();
        public SquareGrid AdjacencyMap => _adjacencyMap;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            foreach (MapTileData data in Resources.LoadAll<MapTileData>(Globals.MAPTILE_DATA_FOLDER))
                foreach (TileBase tile in data.Tiles)
                    _tileToData.Add(tile, data);
        }

        public void GenerateMap()
        {
            _CreateMapTiles();
            _CreateAdjacencyMap();
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
                        Vector2Int tileKey = new(x, y);

                        if (_tileMap.HasTile(gridLocation) && !_map.ContainsKey(tileKey))
                        {
                            MapTileData data = GetMapTileDataFromPlacedTile(gridLocation);
                            Vector2 cellWorldPos = TileToWorldSpace(gridLocation);

                            MapTile mapTile = new(_mapTilePrefab, idIterator, _tileParent.transform, cellWorldPos, data, (Vector2Int)gridLocation);

                            _map.Add(tileKey, mapTile);
                            _intToTile.Add(idIterator, mapTile);
                            idIterator++;                            
                        }
                    }
                }
            }
        }

        private void _CreateAdjacencyMap()
        {
            foreach (KeyValuePair<Vector2Int, MapTile> from in _map)
            {
                //if (from.Value.Id == 110)
                //{
                //    Debug.Log(from.Key);
                //    foreach (MapTile tile in _GetAdjacencies(from.Key))
                //    {
                //        Debug.Log(tile.Id);
                //    }                   
                //}
                _adjacencyMap.Add(GetTileAtPosition(from.Key).Id, _GetAdjacencies(from.Key));
            }
        }

        private MapTile[] _GetAdjacencies(Vector2Int from)
        {
            List<MapTile> adjacencies = new();
            //Debug.Log(from);
            //Debug.Log(from + new Vector2Int(-1, -1));
            if (_map.ContainsKey(from + new Vector2Int(-1, -1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(-1, -1)));
            if (_map.ContainsKey(from + new Vector2Int(0, -1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(0, -1)));
            if (_map.ContainsKey(from + new Vector2Int(-1, 0))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(-1, 0)));
            if (_map.ContainsKey(from + new Vector2Int(0, 1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(0, 1)));
            if (_map.ContainsKey(from + new Vector2Int(1, 0))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(1, 0)));
            if (_map.ContainsKey(from + new Vector2Int(1, 1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(1, 1)));
            if (_map.ContainsKey(from + new Vector2Int(-1, 1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(-1, 1)));
            if (_map.ContainsKey(from + new Vector2Int(1, -1))) adjacencies.Add(GetTileAtPosition(from + new Vector2Int(1, -1)));

            return adjacencies.ToArray();
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


        public MapTileData GetMapTileDataFromPlacedTile(Vector3Int position)
        {
            TileBase tileBase = _tileMap.GetTile(position);

            if (!_tileToData.ContainsKey(tileBase))
            {
                Debug.LogError("Referencing null key.");
                return null;
            }
            else
            {
                return _tileToData[tileBase];
            }
        }

        public MapTile GetTileAtPosition(Vector2Int position)
        {
            //Debug.Log(position);
            MapTile tileToReturn;

            tileToReturn = _map[position];
            if (tileToReturn == null) { Debug.LogError("Referencing point off map."); }

            return tileToReturn;
        }
    }
}
