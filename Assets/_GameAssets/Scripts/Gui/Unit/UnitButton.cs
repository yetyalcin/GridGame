using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class UnitButton : MonoBehaviour
{
    public GlobalVariables.UnitType Type;
    private Transform _spawnPrefab;

    #region UnityBuildinFunctions
    private void Start()
    {
        GetUnitType();
    }
    #endregion

    #region CustomMethods
	public void OnClick()
    {
        SpawnUnit();
    }
    private void GetUnitType()
    {
        switch (Type)
        {
            case GlobalVariables.UnitType.Barracks:
                _spawnPrefab = SpawnManager.Instance.UnitBuildingPrefabs[0];
                break;
            case GlobalVariables.UnitType.PowerPlant:
                _spawnPrefab = SpawnManager.Instance.UnitBuildingPrefabs[1];
                break;
            case GlobalVariables.UnitType.Empty:
                Debug.Log("Unit spawned");
                _spawnPrefab = null;
                break;
            default:
                break;
        }
    }
    private void SpawnUnit()
    {
        if (_spawnPrefab == null)
            return;
        if (SceneManager.Instance.PlayerController.HoldingTransform != null)
            return;

        Transform spawnedTransform = Instantiate(_spawnPrefab, null);
    }
    #endregion
}
