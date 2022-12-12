using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using DG.Tweening;

public class UnitGridController : MonoBehaviour
{
    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer Visual;


    #region UnityBuildinFunctions
    private void Start()
    {
        StartCoroutine(CorVisualEnable());
    }
    private void Update()
    {
	
    }
    #endregion

    #region CustomMethods
	public void ChangeFillColor()
    {
        Visual.color = new Color(Visual.color.r, Visual.color.g, Visual.color.b, 0);
        Visual.DOFade(1, 1).OnComplete(() =>
        {
            
        });
        
    }
    IEnumerator CorVisualEnable()
    {
        yield return new WaitForSeconds(0.05f);
        Visual.enabled = true;
    }
    #endregion
}
