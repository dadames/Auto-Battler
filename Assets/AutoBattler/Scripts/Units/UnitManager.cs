using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoBattler
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitManager : MonoBehaviour
    {
        private OverlayTile _parentTile;
        public OverlayTile ParentTile => _parentTile;


        public void SetParentTile(OverlayTile parentTile)
        {
            _parentTile = parentTile;
        }
    }
}
