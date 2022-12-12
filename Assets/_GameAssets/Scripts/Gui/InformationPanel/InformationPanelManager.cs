using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections;
using System;

public class InformationPanelManager : MonoBehaviour
{
    private GuiManager _guiManager;

    [SerializeField] private Image _unitImage;
    [SerializeField] private TextMeshProUGUI _unitName;
    [SerializeField] private Image _productionImage;
    [SerializeField] private TextMeshProUGUI _productionText;

    public GameObject ProductionPanel;

    #region UnityBuildinFunctions
    private void Start()
    {
        _guiManager = GetComponentInParent<GuiManager>();
        _guiManager.InfoPanel = this;

        ResetUnitValues();
    }
    private void Update()
    {
	
    }
    #endregion

    #region CustomMethods
	public void SetUnitValues(UnitBase unit)
    {
        _unitImage.color = unit.UnitImage.color;
        _unitName.SetText("" + unit.UnitName);

        _unitImage.gameObject.SetActive(true);
        _unitName.gameObject.SetActive(true);

        if (unit.Type.Equals(GlobalVariables.UnitType.Barracks))
        {
            ProductionPanel.SetActive(true);
            _productionImage.color = unit.GetComponent<UnitBarracks>().ProdictionSoldier.GetComponent<UnitSoldier>().UnitImage.color;
            _productionText.SetText("" + unit.GetComponent<UnitBarracks>().ProdictionSoldier.GetComponent<UnitSoldier>().name);
        }
        else
        {
            ProductionPanel.SetActive(false);
        }
    }
    public void ResetUnitValues()
    {
        _unitImage.gameObject.SetActive(false);
        _unitName.gameObject.SetActive(false);
        ProductionPanel.SetActive(false);
    }
    #endregion
}
