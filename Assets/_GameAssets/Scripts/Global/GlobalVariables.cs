using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public static class GlobalVariables
{
    #region Layers
    public static int UnitBuildLayerIndex = 6;
    public static int UnitSoldierLayerIndex = 7;
    public static int GridLayerIndex = 8;
    #endregion

    #region Tags
    public static string BarracksTag = "Barracks";
    public static string PowerPlantTag = "PowerPlant";
    public static string SoldierTag = "Soldier";
    #endregion

    public enum UnitType : byte
    {
        Barracks,
        PowerPlant,
        Soldier,
        Empty,
    }
}
