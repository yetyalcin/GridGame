using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using System.Linq;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    [ReadOnly] public Transform HoldingTransform;
    public LayerMask Mask;

    public List<GridController> ClickedNeighboors;

    #region UnityBuildinFunctions
    private void Start()
    {
        SceneManager.Instance.PlayerController = this;
    }
    private void Update()
    {
        UnitSelector();
        UnitMover();
    }
    #endregion

    #region CustomMethods
    private void UnitSelector()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mask))
            {
                if (HoldingTransform == null)
                {
                    if (hit.transform.gameObject.layer.Equals(GlobalVariables.UnitSoldierLayerIndex))
                    {
                        HoldingTransform = hit.transform;
                        GuiManager.Instance.InfoPanel.SetUnitValues(HoldingTransform.GetComponent<UnitBase>());
                    }
                    if (hit.transform.gameObject.layer.Equals(GlobalVariables.GridLayerIndex))
                    {
                        GridController clickedGridCotnroller = hit.transform.GetComponent<GridController>();

                        if (!clickedGridCotnroller.FillType.Equals(GlobalVariables.UnitType.Empty))
                        {
                            GuiManager.Instance.InfoPanel.SetUnitValues(clickedGridCotnroller.FillObject.GetComponent<UnitBase>());
                        }
                        else
                        {
                            GuiManager.Instance.InfoPanel.ResetUnitValues();
                            HoldingTransform = null;
                        }
                    }
                }
                else
                {
                    if (hit.transform.gameObject.layer.Equals(GlobalVariables.UnitSoldierLayerIndex))
                    {
                        HoldingTransform = hit.transform;
                    }
                    if (hit.transform.gameObject.layer.Equals(GlobalVariables.GridLayerIndex))
                    {
                        if (HoldingTransform.gameObject.layer.Equals(GlobalVariables.UnitBuildLayerIndex))
                        {
                            GridController clickedGridCotnroller = hit.transform.GetComponent<GridController>();
                            UnitBase unitBase = HoldingTransform.GetComponent<UnitBase>();

                            UnitBarracks unitBarracks = HoldingTransform.GetComponent<UnitBarracks>();

                            int rowRange = unitBase.Rows;
                            int colRange = unitBase.Cols;

                            NeighboorChecker(clickedGridCotnroller.ColIndex, clickedGridCotnroller.RowIndex, colRange, rowRange);
                            FillChecker(colRange * rowRange, unitBase);

                            if (unitBarracks != null)
                            {
                                foreach (var extendedNeighbours in ClickedNeighboors)
                                {
                                    List<GridController> temp = GridManager.Instance.GetNeighbours(extendedNeighbours);
                                    foreach (var barrackNeighbour in temp)
                                    {
                                        if (!unitBarracks.Neighbours.Contains(barrackNeighbour) && !barrackNeighbour.IsFilled)
                                        {
                                            unitBarracks.Neighbours.Add(barrackNeighbour);
                                            //barrackNeighbour.Visual.color = Color.cyan;
                                        }
                                    }
                                }
                                unitBarracks.CanSpawn = true;
                            }

                            ClickedNeighboors.Clear();
                        }
                        else if (HoldingTransform.gameObject.layer.Equals(GlobalVariables.UnitSoldierLayerIndex))
                        {
                            GuiManager.Instance.InfoPanel.ResetUnitValues();
                            HoldingTransform = null;
                        }
                        
                    }
                }
            }
        }
    }
    private void UnitMover()
    {
        if(HoldingTransform == null)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mask))
            {
                if (hit.transform.gameObject.layer.Equals(GlobalVariables.GridLayerIndex))
                {
                    if (HoldingTransform.gameObject.layer.Equals(GlobalVariables.UnitSoldierLayerIndex))
                    {
                        UnitSoldier soldier = HoldingTransform.GetComponent<UnitSoldier>();

                        soldier.FindMyPath(hit.transform.position);
                        soldier.CanMove = true;
                    }
                }
            }
        }
    }
    private void NeighboorChecker(int clickedGridIndexX, int clickedGridIndexY, int colRange, int rowRange)
    {
        int crossMultipler = 0;
        int xMultipler = 1;
        int yMultipler = 1;

        int range = colRange * rowRange;
        int j = 0;
        int k = 0;

        foreach (var item in GridManager.Instance.SpawnedGrids)
        {
            if (ClickedNeighboors.Count.Equals(range))
                return;

            if (item.RowIndex == clickedGridIndexY + crossMultipler && item.ColIndex == clickedGridIndexX + crossMultipler)
            {
                if (!ClickedNeighboors.Contains(item))
                    ClickedNeighboors.Add(item);

                crossMultipler++;
            }

            if (item.RowIndex == clickedGridIndexY + yMultipler && item.ColIndex == clickedGridIndexX + j)
            {
                if (!ClickedNeighboors.Contains(item))
                    ClickedNeighboors.Add(item);

                yMultipler++;

                if (yMultipler == rowRange)
                {
                    yMultipler = 0;
                    j++;
                }
            }

            if (item.ColIndex == clickedGridIndexX + xMultipler && item.RowIndex == clickedGridIndexY + k)
            {
                if (!ClickedNeighboors.Contains(item))
                    ClickedNeighboors.Add(item);

                xMultipler++;
                if (xMultipler == colRange)
                {
                    xMultipler = 0;
                    k++;
                }
            }
        }

        ClickedNeighboors = ClickedNeighboors.Distinct().ToList();
    }
    private void FillChecker(int range, UnitBase unit)
    {
        for (int i = 0; i < range; i++)
        {
            if (ClickedNeighboors.Count < range)
                return;

            GridController clickedNeighbor = ClickedNeighboors[i];

            if (clickedNeighbor.IsFilled)
                SetFilled(unit, i, clickedNeighbor);
        }

        int neighboorCount = ClickedNeighboors.FindAll(item => item.IsFilled).Count;

        if (neighboorCount == 0)
        {
            unit.DropBuilding(ClickedNeighboors[0].transform.position);

            for (int i = 0; i < range; i++)
            {
                GridManager.Instance.FillGrid(ClickedNeighboors[i].RowIndex, ClickedNeighboors[i].ColIndex, unit.Type, unit.gameObject);
            }

            HoldingTransform = null;
        }
    }
    private static void SetFilled(UnitBase unit, int i, GridController clickedNeighbor)
    {
        unit.Grids[i].ChangeFillColor();
        clickedNeighbor.FilledVisual();
    }
    
    #endregion
}
