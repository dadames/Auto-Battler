using UnityEngine;

namespace AutoBattler
{
    public class DataHandler : MonoBehaviour
    {
        private void Awake()
        {
            LoadGameData();
        }

        public void LoadGameData()
        {
            Globals.UNIT_DATA = Resources.LoadAll<UnitData>(Globals.UNIT_DATA_FOLDER) as UnitData[];
        }
    }
}
