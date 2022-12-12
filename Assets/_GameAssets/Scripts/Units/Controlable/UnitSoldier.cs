using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class UnitSoldier : UnitBarracks
{
    [Header("Soldier")]
    public float MoveSpeed;
    public Transform TargetPos;
    public List<GridController> Path;

    public bool CanMove;

    #region UnityBuildinFunctions
    private void Start()
    {

    }
    private void Update()
    {
        Movement();
    }
    #endregion

    #region CustomMethods
    public void FindMyPath(Vector3 target)
    {
        Path.Clear();
        PathFinding.Instance.FindPath(this.transform.position, target, Path);
    }
    public void Movement()
    {
        if (!CanMove)
            return;

        if (Path.Count > 0)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, Path[0].transform.position, Time.deltaTime * MoveSpeed);

            float distance = Vector3.Distance(this.transform.position, Path[0].transform.position);

            if (distance < 0.1f)
                Path.Remove(Path[0]);
        }
    }
    #endregion
}
