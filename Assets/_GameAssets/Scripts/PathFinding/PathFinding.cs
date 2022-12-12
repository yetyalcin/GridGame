using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class PathFinding : MonoBehaviour
{
    public static PathFinding Instance;

    private GridManager _grid;
    public Transform StartPosition;
    public Transform TargetPosition;

    #region UnityBuildinFunctions
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _grid = GetComponent<GridManager>();
    }
    private void Update()
    {
        //FindPath(StartPosition.position, TargetPosition.position);
    }
    #endregion

    #region CustomMethods
    public void FindPath(Vector3 startPos, Vector3 endPos,List<GridController> movePath)
    {
        GridController startNode = _grid.GridControllerFromWorldPoint(startPos);
        GridController targetNode = _grid.GridControllerFromWorldPoint(endPos);

        List<GridController> openSet = new List<GridController>();
        HashSet<GridController> closedSet = new HashSet<GridController>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            GridController currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode, movePath);
                return;
            }

            foreach (var item in _grid.GetNeighbours(currentNode))
            {
                if (item.IsFilled || closedSet.Contains(item))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, item);
                if (newMovementCostToNeighbour < item.gCost || !openSet.Contains(item))
                {
                    item.gCost = newMovementCostToNeighbour;
                    item.hCost = GetDistance(item, targetNode);
                    item.CameFrom = currentNode;

                    if (!openSet.Contains(item))
                        openSet.Add(item);

                }
            }
        }

    }

    private void RetracePath(GridController startNode, GridController endNode, List<GridController> movePath)
    {
        List<GridController> path = new List<GridController>();
        GridController currentNode = endNode;

        while (currentNode != startNode)
        {
            movePath.Add(currentNode);
            currentNode = currentNode.CameFrom;
        }
        movePath.Reverse();

       // movePath = path;

        //List<GridController> path = new List<GridController>();
        //GridController currentNode = endNode;

        //while (currentNode != startNode)
        //{
        //    path.Add(currentNode);
        //    currentNode = currentNode.CameFrom;
        //}
        //path.Reverse();

        //movePath = path;
    }

    private int GetDistance(GridController nodeA, GridController nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.ColIndex - nodeB.RowIndex);
        int distanceY = Mathf.Abs(nodeA.RowIndex - nodeB.ColIndex);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
    #endregion

}