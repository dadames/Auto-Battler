using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitManager : MonoBehaviour
    {
        private Unit _unit;
        private Rigidbody2D _rigidBody;

        private bool _initialized = false;
        private OverlayTile _parentTile;
        public OverlayTile ParentTile => _parentTile;


        public void Initialize(Unit unit, Vector2Int position)
        {
            _unit = unit;
            _rigidBody = GetComponent<Rigidbody2D>();
            SetPosition(position);
            _initialized = true;
        }

        private void Update()
        {
            if (_initialized == false) return;
            SetOccupiedTile();
        }

        private void FixedUpdate()
        {
            if (_initialized == false) return;
            _rigidBody.MovePosition(_rigidBody.position + new Vector2(0.01f, 0));
        }

        public void SetPosition(Vector2Int position)
        {
            OverlayTile placedOnTile = MapManager.Instance.GetTileAtPosition(position);

            transform.position = MapManager.Instance.TileToWorldSpace(placedOnTile.GridLocation);
        }

        public void SetOccupiedTile()
        {
            OverlayTile placedOnTile = GetTileAtPosition();

            if (placedOnTile != _unit.ParentTile)
            {
                OverlayTile formerTile = _unit.ParentTile;
                _unit.SetParentTile(placedOnTile);
                formerTile.ClearTile();
            }
        }

        public OverlayTile GetTileAtPosition()
        {
            OverlayTile placedOnTile = Physics2D.OverlapCircle(transform.position, 0.1f).GetComponent<OverlayTile>();
            Debug.Log(placedOnTile.name);
            return placedOnTile;
        }
    }
}
