using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class UnitPowerPlant : UnitBarracks
{
    private void Start()
    {
        StartValues();
        CreateGrid();
    }

    private void Update()
    {
        DragWithMouse();
    }
    public override void StartValues()
    {
        Type = GlobalVariables.UnitType.PowerPlant;
    }
}
