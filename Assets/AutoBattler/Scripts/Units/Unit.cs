using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    public class Unit
    {
        private int _speed;
        private Transform _transform;
        private UnitManager _unitManager;
        

        public Unit(UnitData data, Vector2Int position)
        {
            GameObject g = GameObject.Instantiate(data.Prefab);
            _transform = g.transform;
            _unitManager = g.transform.GetComponent<UnitManager>();
            _speed = data.Speed;
            SetPosition(position);
        }

        public void SetPosition(Vector2Int position)
        {
            var placedOnTile = MapManager.Instance.GetTileAtPos(position);

            if (placedOnTile == null)
            {
                Debug.LogError("Dice spawning off map.");
                return;
            }

            OverlayTile overlayTile= placedOnTile.gameObject.GetComponent<OverlayTile>();
            overlayTile.MoveUnitToTile(_unitManager);
            _transform.position = MapManager.Instance.TileToWorldSpace(placedOnTile.GridLocation);
        }
    }
}
