using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    [RequireComponent(typeof(Renderer))]
    public class OverlayTile : MonoBehaviour
    {
        private Vector2Int _gridLocation;
        public Vector2Int GridLocation { get => _gridLocation; }
        private TileData _data;
        public TileData Data { get => _data; }

        private void Awake()
        {
            HideTile();
        }

        public void ShowTile()
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }

        public void HideTile()
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
