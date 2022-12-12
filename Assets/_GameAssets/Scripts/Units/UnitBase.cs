using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.UI;

public abstract class UnitBase : MonoBehaviour
{
    public GlobalVariables.UnitType Type;
    public string UnitName;
    public SpriteRenderer UnitImage;
    [Header("Buildings")]
    public int[,] UnitGridArray;
    public GameObject SpawnGridObj;

    public int Cols;
    public int Rows;

    [HideInInspector] public List<UnitGridController> Grids;
    [HideInInspector] public bool OnMouseDrag;
    [HideInInspector] public bool Controlable;

    #region UnityBuildinFunctions
   
    private void Start()
    {

    }
    private void Update()
    {
         
    }
    #endregion

    #region CustomMethods
    public abstract void StartValues();
    public void CreateGrid()
    {
        if (Rows == 0 || Cols == 0)
            return;

        UnitGridArray = new int[Rows,Cols];

        for (int x = 0; x < Cols; x++)
        {
            for (int y = 0; y < Rows; y++)
            {
                SpawnGrid(x,y);
            }
        }
        if(!Controlable)
            PLayerDragHold();
    }
    private void SpawnGrid(int x, int y)
    {
        GameObject obj = Instantiate(SpawnGridObj, null);

        obj.transform.localPosition = new Vector3(x, y);
        obj.transform.SetParent(this.transform);

        Grids.Add(obj.GetComponent<UnitGridController>());

        UnitGridController gridController = obj.GetComponent<UnitGridController>();
        gridController.RowIndex = y;
        gridController.ColIndex = x;
        gridController.Visual.color = UnitImage.color;
    }
    protected void DragWithMouse()
    {
        if (!OnMouseDrag)
            return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2f;
        Vector3 objPos = Camera.main.ScreenToWorldPoint(mousePos);

        this.transform.position = objPos;
    }
    public void DropBuilding(Vector3 pos)
    {
        OnMouseDrag = false;
        this.transform.position = pos;
    }
    private void PLayerDragHold()
    {
        OnMouseDrag = true;
        SceneManager.Instance.BuildingsOnScene.Add(this.transform);
        SceneManager.Instance.PlayerController.HoldingTransform = this.transform;
    }
    
    #endregion
}
