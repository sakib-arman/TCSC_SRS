using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObject radio2090;
    public GameObject radio2400;
    public GameObject radioQmac;

    public GameObject SpawnPoint;

    public GameObject pickRadioArrow;
    public GameObject selectAntennaArrow;
    //public GameObject pickAntennaArrow;
    //public GameObject selectAccessoriesArrow;
    //public GameObject pickAccessoriesArrow;

    public void Start()
    {
        pickRadioArrow.SetActive(true);
        selectAntennaArrow.SetActive(false);
    }

    public void Instantiate2090()
    {
        pickRadioArrow.SetActive(false);
        selectAntennaArrow.SetActive(true);
        Instantiate(radio2090, SpawnPoint.transform.position, Quaternion.identity);
    }

    public void Instantiate2400()
    {
        Instantiate(radio2400, SpawnPoint.transform.position, Quaternion.identity);
    }
    public void InstantiateQmac()
    {
        Instantiate(radioQmac, SpawnPoint.transform.position , Quaternion.identity);
    }


}
