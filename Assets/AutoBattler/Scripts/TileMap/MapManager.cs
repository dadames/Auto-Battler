using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AutoBattler
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance => _instance;


        [SerializeField] private OverlayTile _overlayTilePrefab;
        [SerializeField] private GameObject _tileParent;
        [SerializeField] private Tilemap _tileMap;
        public Tilemap TileMap => _tileMap;

        private readonly Dictionary<Vector2Int, OverlayTile> _map = new();
        private readonly Dictionary<TileBase, TileData> _tileDataDict = new();

        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            foreach (var data in Resources.LoadAll<TileData>("ScriptableObjects/TileData/"))
                foreach (var tile in data.Tiles)
                    _tileDataDict.Add(tile, data);
        }

        public void GenerateMap()
        {
            BoundsInt bounds = _tileMap.cellBounds;

            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        Vector3Int tileLocation = new(x, y, z);
                        Vector2Int tileKey = new(x, y);

                        if (_tileMap.HasTile(tileLocation) && !_map.ContainsKey(tileKey))
                        {
                            var overlayTile = Instantiate(_overlayTilePrefab, _tileParent.transform);
                            var cellWorldPos = TileToWorldSpace(tileLocation);

                            overlayTile.transform.position = new Vector3(cellWorldPos.x, cellWorldPos.y, 0);
                            overlayTile.SetGridLocation((Vector2Int)tileLocation);
                            overlayTile.GetComponent<OverlayTile>().HideTile();
                            overlayTile.name = tileKey.ToString();

                            overlayTile.SetData(GetTileData(tileLocation));

                            _map.Add(tileKey, overlayTile);
                        }
                    }
                }
            }
        }


        public Vector3 TileToWorldSpace(Vector2Int pos)
        {
            return TileToWorldSpace((Vector3Int)pos);
        }

        public Vector3 TileToWorldSpace(Vector3Int pos)
        {
            Vector3 vec = this._tileMap.GetCellCenterWorld(pos);
            vec += ((pos.x + pos.y)) * Vector3.forward;
            return vec;
        }

        public TileData GetTileData(Vector3Int pos)
        {
            TileBase tileBase = _tileMap.GetTile(pos);

            if (!_tileDataDict.ContainsKey(tileBase))
            {
                return null;
            }
            else
            {
                return _tileDataDict[tileBase];
            }
        }

        public OverlayTile GetTileAtPos(Vector2Int pos)
        {
            return _map[pos];
        }
    }
}
