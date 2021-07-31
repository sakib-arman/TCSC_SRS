using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField]
    GameObject dropDownPanel;
    private bool isOn=true;

    [SerializeField]
    GameObject equipmentMenu;
    [SerializeField]
    GameObject AntennaMenu;

    [SerializeField]
    GameObject AccessoriesPanel;
    [SerializeField]
    GameObject DocumentsPanel;

    [SerializeField]
    GameObject equipmentHolder;
    [SerializeField]
    GameObject AntennaHolder;

    [SerializeField]
    GameObject EquipText;
    [SerializeField]
    GameObject AntennaText;
    [SerializeField]
    GameObject accessText;
    [SerializeField]
    GameObject docuText;


    private void Start()
    {
        dropDownPanel.SetActive(false);
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

    public void equipmentShow()
    {
        equipmentMenu.SetActive(true);
        equipmentHolder.SetActive(true);

        AntennaMenu.SetActive(false);
        AntennaHolder.SetActive(false);

        AccessoriesPanel.SetActive(false);

        DocumentsPanel.SetActive(false);

        EquipText.SetActive(true);
        AntennaText.SetActive(false);
        accessText.SetActive(false);
        docuText.SetActive(false);
    }
    public void antennaShow()
    {
        equipmentMenu.SetActive(false);
        equipmentHolder.SetActive(false);

        AntennaMenu.SetActive(true);
        AntennaHolder.SetActive(true);

        AccessoriesPanel.SetActive(false);

        DocumentsPanel.SetActive(false);

        EquipText.SetActive(false);

        EquipText.SetActive(false);
        AntennaText.SetActive(true);
        accessText.SetActive(false);
        docuText.SetActive(false);
    }
    public void accessoriesShoiw()
    {
        equipmentMenu.SetActive(false);
        equipmentHolder.SetActive(false);

        AntennaMenu.SetActive(false);
        AntennaHolder.SetActive(false);

        AccessoriesPanel.SetActive(true);

        DocumentsPanel.SetActive(false);

        EquipText.SetActive(false);
        AntennaText.SetActive(false);
        accessText.SetActive(true);
        docuText.SetActive(false);
    }

    public void documentShow()
    {
        equipmentMenu.SetActive(false);
        equipmentHolder.SetActive(false);

        AntennaMenu.SetActive(false);
        AntennaHolder.SetActive(false);

        AccessoriesPanel.SetActive(false);

        DocumentsPanel.SetActive(true);

        EquipText.SetActive(false);
        AntennaText.SetActive(false);
        accessText.SetActive(false);
        docuText.SetActive(true);
    }
}
