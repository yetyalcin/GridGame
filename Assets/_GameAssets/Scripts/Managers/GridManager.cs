using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;


public class GridManager : MonoBehaviour
{
    public GridController[,] GridArray;
    public List<GridController> SpawnedGrids;
    private int _vecticalScreenSize;
    private int _horizontalScreenSize;
    private int _cols;
    private int _rows;

    public List<GridController> path;

    [SerializeField] private GameObject _spawnTileObj;
    [SerializeField] private Transform _spawnedTileObjParent;

    public event Action<int, int, GlobalVariables.UnitType, GameObject> IsFilled;

    public static GridManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        CreateGridMap();
    }

    #region UnityBuildinFunctions
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var item in GridArray)
            {
                item.Visual.color = Color.white;
            }

            //PathFinding.Instance.FindPath(PathFinding.Instance.StartPosition.position, PathFinding.Instance.TargetPosition.position);

            foreach (var item in path)
            {
                item.Visual.gameObject.SetActive(true);
                item.Visual.color = Color.red;
            }
        }
    }
    #endregion

    #region CustomMethods
    public void FillGrid(int x, int y, GlobalVariables.UnitType type, GameObject unitObj)
    {
        if (IsFilled == null)
            return;

        IsFilled(x, y, type, unitObj);
    }
    public void CreateGridMap()
    {
        _vecticalScreenSize = (int)Camera.main.orthographicSize;
        _horizontalScreenSize = _vecticalScreenSize * (Screen.width / Screen.height);

        _cols = _horizontalScreenSize * 2;
        _rows = _vecticalScreenSize * 2;

        GridArray = new GridController[_cols, _rows];

        for (int x = 0; x < _cols; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                GridController spawnedGridController = SpawnTile(x, y);
                GridArray[x, y] = spawnedGridController;
            }
        }
    }

    public GridController SpawnTile(int x, int y)
    {
        GameObject obj = Instantiate(_spawnTileObj, null);
        obj.name = "[" + x + "]" + "[" + y + "]";

        GridController gridControler = obj.GetComponent<GridController>();

        Vector3 newPosition = new Vector3(x - (_horizontalScreenSize - 0.5f), y - (_vecticalScreenSize - 0.5f));
        gridControler.SetValues(newPosition, _spawnedTileObjParent, GlobalVariables.GridLayerIndex);

        gridControler.ColIndex = x;
        gridControler.RowIndex = y;

        SpawnedGrids.Add(gridControler);

        return gridControler;
    }
    #endregion

    public GridController GridControllerFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + _horizontalScreenSize / 2) / _horizontalScreenSize;
        float percentY = (worldPosition.y + _vecticalScreenSize / 2) / _vecticalScreenSize;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((_cols - 1) * percentX);
        int y = Mathf.RoundToInt((_rows - 1) * percentY);

        return GridArray[x, y];
    }

    public List<GridController> GetNeighbours(GridController gridController)
    {
        List<GridController> tempList = new List<GridController>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                int checkX = gridController.ColIndex + i;
                int checkY = gridController.RowIndex + j;

                if (checkX >= 0 && checkX < _cols && checkY >= 0 && checkY < _rows)
                {
                    tempList.Add(GridArray[checkX, checkY]);
                }
            }
        }

        return tempList;
    }

}