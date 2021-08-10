using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectImage : MonoBehaviour, IPointerDownHandler
{
    //Prefabs that need to be instantiated
    public GameObject inistatePrefab;
    //Instantiated object
    private GameObject inistateObj;
    public float scaleFactor = 0.6f;

    // Use this for initialization
    void Start()
    {
        if (inistatePrefab == null) return;
        //Instantiate prefab
        inistateObj = Instantiate(inistatePrefab) as GameObject;
        inistateObj.transform.localScale *= scaleFactor; //new add
        inistateObj.SetActive(false);
    }
    //Realize the mouse down interface
    public void OnPointerDown(PointerEventData eventData)
    {
        inistateObj.SetActive(true);

        //Pass the object that needs to be instantiated to the manager
        SelectObjManager.Instance.AttachNewObject(inistateObj);
    }
}
