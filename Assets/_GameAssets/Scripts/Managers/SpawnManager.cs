using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> UnitBuildingPrefabs;

    public static SpawnManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
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
