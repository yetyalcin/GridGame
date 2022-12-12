using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class PathNode 
{
    private GridController _gridController;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode CameFromNode;

    
    #region UnityBuildinFunctions
    private void Start()
    {
	
    }
    private void Update()
    {
	
    }
    #endregion

    #region CustomMethods
	//CustomMethods
    #endregion
}
