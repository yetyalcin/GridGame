using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class GuiManager : MonoBehaviour
{
    [HideInInspector] public InformationPanelManager InfoPanel;

    public static GuiManager Instance;
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
