using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using DG.Tweening;

public class GridController : MonoBehaviour
{
    //indexes
    public int RowIndex;
    public int ColIndex;

    //costs
    public int gCost;//cost to next grid
    public int hCost;//distance to target
    public int fCost => gCost + hCost;


    public GlobalVariables.UnitType FillType;
    public GameObject FillObject;
    public bool IsFilled;
    public SpriteRenderer Visual;
    public GridController CameFrom;

    private void Start()
    {
        GridManager.Instance.IsFilled += FillSub;
        FillType = GlobalVariables.UnitType.Empty;
    }

    public void SetValues(Vector3 position, Transform parent, int Layer)
    {
        transform.position = position;
        transform.SetParent(parent);
        gameObject.layer = Layer;
    }

    public void FillSub(int x, int y, GlobalVariables.UnitType type, GameObject unitObj)
    {
        if (x == RowIndex && y == ColIndex)
        {
            Debug.Log("Filled" + this.transform.name);
            IsFilled = true;
            FillType = type;
            FillObject = unitObj;
        }
    }

    public void FillUnSub(int x, int y)
    {
        if (x == RowIndex && y == ColIndex)
        {
            Debug.Log("UnFilled" + this.transform.name);
            IsFilled = false;
        }
    }
    public void FilledVisual()
    {
        if (!Visual.gameObject.activeSelf)
        {
            Visual.gameObject.SetActive(true);
            Visual.color = Color.red;
            Visual.DOFade(0, 1).OnComplete(() =>
            {
                Visual.gameObject.SetActive(false);
            });
        }
    }

}