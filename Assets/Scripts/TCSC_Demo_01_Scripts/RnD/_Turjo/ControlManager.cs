using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField]
    GameObject dropDownPanel;
    private bool isOn  =true;
    private bool isOn2 = true;
    private bool isOn3 = true;


    [SerializeField]
    GameObject equipmentMenu;
    [SerializeField]
    GameObject AntennaMenu;

    [SerializeField]
    GameObject AccessoriesPanel;
    [SerializeField]
    GameObject DocumentsPanel;

    [SerializeField]
    GameObject equipmentHolderRadio;
    [SerializeField]
    GameObject equipmentHolderRR;
    [SerializeField]
    GameObject equipmentHolderMicro;
    [SerializeField]
    GameObject AntennaHolderBase;
    [SerializeField]
    GameObject AntennaHolderVehicle;
    [SerializeField]
    GameObject AntennaHolderManpack;

    //[SerializeField]
    //GameObject EquipText;
    //[SerializeField]
    //GameObject AntennaText;
    //[SerializeField]
    //GameObject accessText;
    //[SerializeField]
    //GameObject docuText;


    [SerializeField]
    GameObject EWpanel;
    [SerializeField]
    GameObject narrativesPanel;


    [SerializeField]
    GameObject systemTypePanel;

    [SerializeField]
    GameObject selectedButton;
    [SerializeField]
    GameObject analyzerButton;
    [SerializeField]
    GameObject MDFbutton;
    [SerializeField]
    GameObject jammerButton;

    int selected;

    /// <summary>
    /// guide sequence
    /// </summary>

    public GameObject pickRadio;
    public GameObject pickAntenna;
    public GameObject pickAccessories;

    public GameObject selectAntenna;
    public GameObject selectAccessories;
    public GameObject selectVehicleAntenna;

    public GameObject SelectAnteenaText;
    public GameObject SelectVehicleAntennaText;
    public GameObject DragAntennaText;

    public GameObject selectAccessoriesText;
    public GameObject DragCableHeadText;
    public GameObject DragHeadSetText;


    private void Start()
    {
        dropDownPanel.SetActive(false);
        EWpanel.SetActive(false);
        narrativesPanel.SetActive(false);
    }

    public void Update()
    {
        selected = PlayerPrefs.GetInt("selectedType");

        if(selected == 0)
        {
            selectedButton.SetActive(true);
            analyzerButton.SetActive(false);
            MDFbutton.SetActive(false);
            jammerButton.SetActive(false);
        }
        else if (selected == 1)
        {
            selectedButton.SetActive(false);
            analyzerButton.SetActive(true);
            MDFbutton.SetActive(false);
            jammerButton.SetActive(false);
        }
        else if (selected == 2)
        {
            selectedButton.SetActive(false);
            analyzerButton.SetActive(false);
            MDFbutton.SetActive(true);
            jammerButton.SetActive(false);
        }
        else if (selected == 3)
        {
            selectedButton.SetActive(false);
            analyzerButton.SetActive(false);
            MDFbutton.SetActive(false);
            jammerButton.SetActive(true);
        }
    }


    public void DropDown()
    {
        if(isOn)
        {
            dropDownPanel.SetActive(true);
            isOn = false;
        }
        else
        {
            dropDownPanel.SetActive(false);
            isOn = true;
        }
    }

    public void DropDownEW()
    {
        if (isOn2)
        {
            EWpanel.SetActive(true);
            isOn2 = false;
            PlayerPrefs.SetInt("selectedType", 0);
        }
        else
        {
            EWpanel.SetActive(false);
            isOn2 = true;
            PlayerPrefs.SetInt("selectedType", 0);
        }
    }
    public void DropDownNarrative()
    {
        if (isOn3)
        {
            narrativesPanel.SetActive(true);
            isOn3 = false;
        }
        else
        {
            narrativesPanel.SetActive(false);
            isOn3 = true;
        }
    }

    public void equipmentShow()
    {
        equipmentMenu.SetActive(true);
        equipmentHolderRadio.SetActive(true);
        equipmentHolderRR.SetActive(false);
        equipmentHolderMicro.SetActive(false);


        AntennaMenu.SetActive(false);
        AntennaHolderBase.SetActive(false);
        AntennaHolderVehicle.SetActive(false);
        AntennaHolderManpack.SetActive(false);

        AccessoriesPanel.SetActive(false);

        DocumentsPanel.SetActive(false);

        //EquipText.SetActive(true);
        //AntennaText.SetActive(false);
        //accessText.SetActive(false);
        //docuText.SetActive(false);
    }
    public void antennaShow()
    {
        equipmentMenu.SetActive(false);
        equipmentHolderRadio.SetActive(false);
        equipmentHolderRadio.SetActive(false);
        equipmentHolderRR.SetActive(false);
        equipmentHolderMicro.SetActive(false);

        AntennaMenu.SetActive(true);
        AntennaHolderBase.SetActive(true);
        AntennaHolderVehicle.SetActive(false);
        AntennaHolderManpack.SetActive(false);

        AccessoriesPanel.SetActive(false);

        DocumentsPanel.SetActive(false);


        selectAntenna.SetActive(false);
        SelectAnteenaText.SetActive(false);
        selectVehicleAntenna.SetActive(true);
        SelectVehicleAntennaText.SetActive(true);
        
        //EquipText.SetActive(false);

        //EquipText.SetActive(false);
        //AntennaText.SetActive(true);
        //accessText.SetActive(false);
        //docuText.SetActive(false);
    }
    public void accessoriesShoiw()
    {
        equipmentMenu.SetActive(false);
        equipmentHolderRadio.SetActive(false);
        equipmentHolderRR.SetActive(false);
        equipmentHolderMicro.SetActive(false);

        AntennaMenu.SetActive(false);
        AntennaHolderBase.SetActive(false);
        AntennaHolderVehicle.SetActive(false);
        AntennaHolderManpack.SetActive(false);

        AccessoriesPanel.SetActive(true);

        DocumentsPanel.SetActive(false);


        selectAccessories.SetActive(false);
        pickAccessories.SetActive(true);

        selectAccessoriesText.SetActive(false);
        DragCableHeadText.SetActive(true);


    }

    public void documentShow()
    {
        equipmentMenu.SetActive(false);
        equipmentHolderRadio.SetActive(false);
        equipmentHolderRR.SetActive(false);
        equipmentHolderMicro.SetActive(false);

        AntennaMenu.SetActive(false);
        AntennaHolderBase.SetActive(false);
        AntennaHolderVehicle.SetActive(false);
        AntennaHolderManpack.SetActive(false);

        AccessoriesPanel.SetActive(false);

        DocumentsPanel.SetActive(true);

        //EquipText.SetActive(false);
        //AntennaText.SetActive(false);
        //accessText.SetActive(false);
        //docuText.SetActive(true);
    }

    public void RadioHolderShow()
    {
        equipmentHolderRadio.SetActive(true);
        equipmentHolderRR.SetActive(false);
        equipmentHolderMicro.SetActive(false);
    }
    public void RRHolderShow()
    {
        equipmentHolderRadio.SetActive(false);
        equipmentHolderRR.SetActive(true);
        equipmentHolderMicro.SetActive(false);
    }
    public void MicroHolderShow()
    {
        equipmentHolderRadio.SetActive(false);
        equipmentHolderRR.SetActive(false);
        equipmentHolderMicro.SetActive(true);
    }

    public void BaseHolderShow()
    {
        AntennaHolderBase.SetActive(true);
        AntennaHolderVehicle.SetActive(false);
        AntennaHolderManpack.SetActive(false);
    }

    public void VehicleHolderShow()
    {
        AntennaHolderBase.SetActive(false);
        AntennaHolderVehicle.SetActive(true);
        AntennaHolderManpack.SetActive(false);
        selectVehicleAntenna.SetActive(false);
        pickAntenna.SetActive(true);
        DragAntennaText.SetActive(true);
        SelectVehicleAntennaText.SetActive(false);
    }
    public void ManpackHolderShow()
    {
        AntennaHolderBase.SetActive(false);
        AntennaHolderVehicle.SetActive(false);
        AntennaHolderManpack.SetActive(true);
    }
    public void systemTypePanelOpen()
    {
        systemTypePanel.SetActive(true);
    }
    public void systemTypePanelClose()
    {
        systemTypePanel.SetActive(false);
    }

    public void selectAnalyzer()
    {
        PlayerPrefs.SetInt("selectedType",1);
        systemTypePanel.SetActive(false);
        Debug.Log(selected);
    }

    public void selectMDF()
    {
        PlayerPrefs.SetInt("selectedType", 2);
        systemTypePanel.SetActive(false);
        Debug.Log(selected);
    }
    public void selectZammer()
    {
        PlayerPrefs.SetInt("selectedType", 3);
        systemTypePanel.SetActive(false);
        Debug.Log(selected);
    }

    public void analyzerButtonFunction()
    {
        systemTypePanel.SetActive(true);
    }

    public void MDFbuttonFunction()
    {
        systemTypePanel.SetActive(true);
    }

    public void jammerButtonFunction()
    {
        systemTypePanel.SetActive(true);
    }
}
