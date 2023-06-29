using UnityEngine;
using TMPro;

namespace AutoBattler
{
    [RequireComponent(typeof(Renderer))]
    public class MapTileManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _idText;
        [SerializeField] private TextMeshProUGUI _gridLocationText;
        private MapTile _mapTile;
        public MapTile MapTile => _mapTile;        
        


        private void OnEnable()
        {
            EventManager.AddListener("DebugMapCoords", _OnDebugMapCoords); 
            EventManager.AddListener("DebugMapIds", _OnDebugMapIds);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener("DebugMapCoords", _OnDebugMapCoords);
            EventManager.RemoveListener("DebugMapIds", _OnDebugMapIds);
        }

        public void Initialize(MapTile mapTile)
        {
            _mapTile = mapTile;
            HideTile();
            _idText.text = MapTile.Id.ToString();
            _gridLocationText.text = MapTile.GridLocation.ToString();
            _idText.enabled = true;
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

        private void _OnDebugMapIds()
        {
            _idText.enabled = !_gridLocationText.enabled;
        }

        private void _OnDebugMapCoords()
        {
            _gridLocationText.enabled = !_gridLocationText.enabled;
        }

        private void OnDestroy()
        {
            Logging.LogNotification($"Tile {MapTile.GridLocation} despawned.", LogType.GAME_SETUP);
        }
    }
}
