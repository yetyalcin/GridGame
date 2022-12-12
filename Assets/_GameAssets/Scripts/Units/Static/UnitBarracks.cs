using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class UnitBarracks : UnitBase
{
    public Transform ProdictionSoldier;
    public float ProductionCoolDown;
    public int SpawnCount;
    public List<GridController> Neighbours = new List<GridController>();

    [HideInInspector] public bool CanSpawn;

    private void Start()
    {
        StartValues();
        CreateGrid();
    }
    private void Update()
    {
        DragWithMouse();

        if (CanSpawn)
        {
            CanSpawn = false;
           StartCoroutine( CorProdictionTime());
        }
    }
    public override void StartValues()
    {
        Controlable = false;
        Type = GlobalVariables.UnitType.Barracks;
    }

    public void CreateUnitSoldier()
    {
        if (Neighbours.Count == 0)
            return;

        Transform spawnedSoldier = Instantiate(ProdictionSoldier, null);

        int rndmNmbr = Random.Range(0, Neighbours.Count);

        spawnedSoldier.transform.position = Neighbours[rndmNmbr].transform.position;
        SpawnCount++;

        if (SpawnCount == 3)
            CanSpawn = false;
        else
            CanSpawn = true;
    }

    IEnumerator CorProdictionTime()
    {
        yield return new WaitForSeconds(ProductionCoolDown);
        CreateUnitSoldier();
        
    }
}
