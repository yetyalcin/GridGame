using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class SceneManager : MonoBehaviour
{
    public GameObject GameCam;
    public PlayerController PlayerController;

    public List<Transform> BuildingsOnScene;

    public static SceneManager Instance;
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
	
    #endregion
}
