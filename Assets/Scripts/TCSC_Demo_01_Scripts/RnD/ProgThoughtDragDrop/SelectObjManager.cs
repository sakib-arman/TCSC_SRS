using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjManager : MonoBehaviour
{
    private static SelectObjManager _instance;
    public static SelectObjManager Instance
    {
        get { return _instance; }
    }
    //The length of the z axis of the object from the camera
    public float _zDistance = 50f;
    //The scaling factor of the object
    public float _scaleFactor = 1.2f;
    //Ground level
    public LayerMask _groundLayerMask;
    int touchID;
    bool isDragging = false;
    bool isTouchInput = false;
    //Whether it is a valid placement (True if placed on the ground, otherwise False)
    [SerializeField]bool isPlaceSuccess = false;
    //The current object to be placed
    public GameObject currentPlaceObj = null;
    //The offset of the coordinate on the Y axis
    public float _YOffset = 0.5F;

    void Awake()
    {
        _instance = this;
    }
    void Update()
    {
        if (currentPlaceObj == null) return;

        if (CheckUserInput())
        {
            MoveCurrentPlaceObj();
        }
        else if (isDragging)
        {
            CheckIfPlaceSuccess();
        }
    }
    /// <summary>
    ///Detect user's current input
    /// </summary>
    /// <returns></returns>
    bool CheckUserInput()
    {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        if (Input.touches.Length > 0) {
            if (!isTouchInput) {
                isTouchInput = true;
                touchID = Input.touches[0].fingerId;
                return true;
            } else if (Input.GetTouch (touchID).phase == TouchPhase.Ended) {
                isTouchInput = false;
                return false;
            } else {
                return true;
            }
        }
        return false;
#else
        return Input.GetMouseButton(0);
#endif
    }
    /// <summary>
    ///Make the current object follow the mouse
    /// </summary>
    void MoveCurrentPlaceObj()
    {
        isDragging = true;
        Vector3 point;
        Vector3 screenPosition;
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        Touch touch = Input.GetTouch (touchID);
        screenPosition = new Vector3 (touch.position.x, touch.position.y, 0);
#else
        screenPosition = Input.mousePosition;
#endif
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000, _groundLayerMask))
        {
            point = hitInfo.point;
            isPlaceSuccess = true;
        }
        else
        {
            point = ray.GetPoint(_zDistance);
            isPlaceSuccess = false;
        }
        currentPlaceObj.transform.position = point + new Vector3(0, _YOffset, 0);
        currentPlaceObj.transform.localEulerAngles = new Vector3(0, -150, 0);
    }
    /// <summary>
    ///Make an object at a specified position
    /// </summary>
    void CreatePlaceObj()
    {
        GameObject obj = Instantiate(currentPlaceObj) as GameObject;

        obj.transform.position = currentPlaceObj.transform.position;
        obj.transform.localEulerAngles = currentPlaceObj.transform.localEulerAngles;
        obj.transform.localScale = Vector3.one;
        obj.transform.localScale *= _scaleFactor;
        //Change the Layer of this object to Drag for subsequent drag detection
        obj.layer = LayerMask.NameToLayer("Draggable");
    }
    /// <summary>
    ///Check if the placement is successful
    /// </summary>
    void CheckIfPlaceSuccess()
    {
        if (isPlaceSuccess)
        {
            Logger.Log("Place Success");
            CreatePlaceObj();
        }
        isDragging = false;
        currentPlaceObj.SetActive(false);
        currentPlaceObj = null;
    }
    /// <summary>
    ///  Pass the object to be created to the current object manager
    /// </summary>
    /// <param name="newObject"></param>
    public void AttachNewObject(GameObject newObject)
    {
        if (currentPlaceObj)
        {
            currentPlaceObj.SetActive(false);
        }
        currentPlaceObj = newObject;
    }
}
