using UnityEngine;

namespace AutoBattler
{
    public static class Globals
    {
        #region GAME ---------------------------------

        public static UnitData[] UNIT_DATA;

        #endregion


        #region DEBUGGING ---------------------------------

        public static bool DEBUG_GAME_SETUP;
        public static bool DEBUG_UNIT_SPAWN;
        public static bool DEBUG_INVENTORY;
        public static bool DEBUG_ITEM_GENERATION;
        public static bool DEBUG_SERIALIZATION;
        public static bool DEBUG_PHASES;
        public static bool DEBUG_AI;

        #endregion

        #region FOLDERS ---------------------------------

        public static string UNIT_DATA_FOLDER = "ScriptableObjects/UnitData";


#if UNITY_EDITOR
        public static string DATA_DIRECTORY = "Data_Dev";
#else
    public static string DATA_DIRECTORY = "Data";
#endif


        public static string SAVE_GAMES_LOCATION = System.IO.Path.Combine(
                Application.persistentDataPath,
                Globals.DATA_DIRECTORY,
                "SaveGames"
                );


#if UNITY_WEBGL
    public static string GetLogFolderPath()
        => System.IO.Path.Combine(
            "idbfs",
            "AutoBattler",
            Globals.DATA_DIRECTORY,
            "Logs");
#else
        public static string GetLogFolderPath()
            => System.IO.Path.Combine(
                Application.persistentDataPath,
                Globals.DATA_DIRECTORY,
                "Logs");

#endif
        #endregion
    }
}