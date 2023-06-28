using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AutoBattler
{
    [RequireComponent(typeof(Renderer))]
    public class OverlayTile : MonoBehaviour
    {
        private Vector2Int _gridLocation;
        public Vector2Int GridLocation => _gridLocation;
        [SerializeField] private TextMeshProUGUI _gridLocationText;
        private TileData _data;
        public TileData Data => _data;
        private UnitManager _occupyingUnit;
        public UnitManager OccupyingUnit => _occupyingUnit;
        public bool IsBlocked
        {
            get { return _data.TileType == TileType.Blocking || _occupyingUnit != null; }
        }


        private void OnEnable()
        {
            EventManager.AddListener("DebugMapCoords", _OnDebugMapCoords);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener("DebugMapCoords", _OnDebugMapCoords);
        }

        private void Awake()
        {
            HideTile();
        }

        private void Start()
        {
            _gridLocationText.text = _gridLocation.ToString();
            _gridLocationText.enabled = false;
        }

        public void ShowTile()
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }

        public void HideTile()
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }

        public void SetData(TileData data)
        {
            _data = data;
        }

        public void SetGridLocation(Vector2Int gridLocation)
        {
            _gridLocation = gridLocation;
        }

        private void _OnDebugMapCoords()
        {
            _gridLocationText.enabled = !_gridLocationText.enabled;
        }

        public void MoveUnitToTile(UnitManager unit)
        {
            _occupyingUnit = unit;
        }

        public void ClearTile()
        {
            _occupyingUnit = null;
        }

        private void OnDestroy()
        {
            Logging.LogNotification($"Tile {_gridLocation} despawned.", LogType.GAME_SETUP);
        }
    }
}
