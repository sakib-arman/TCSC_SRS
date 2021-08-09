using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : MonoBehaviour
{
    //Drag only for the specified level
    public LayerMask _dragLayerMask;
    //Specify the object to be dragged
    public Transform currentTransform;
    //Whether the current object can be dragged
    public bool isDrag = false;
    //Used to store the coordinates of the object that needs to be dragged in the screen space
    Vector3 screenPos = Vector3.zero;
    //The offset of the coordinates of the object that needs to be dragged relative to the mouse in world space coordinates
    Vector3 offset = Vector3.zero;
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //Convert the mouse input point into a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            //If the current object collides with the specified level, it means that the current object can be dragged
            if (Physics.Raycast(ray, out hitinfo, 10000f, _dragLayerMask))
            {
                Logger.Log("Can Drag");
                isDrag = true;
                //Assign the object that needs to be dragged to the object that the ray collides with
                currentTransform = hitinfo.transform;
                //Convert the current object's world coordinates to screen coordinates
                screenPos = Camera.main.WorldToScreenPoint(currentTransform.position);
                //Convert the mouse's screen coordinates to world space coordinates, and then calculate the offset between them and the object to be dragged
                offset = currentTransform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
            }
            else
            {
                Logger.Log("Cant Drag");
                isDrag = false;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (isDrag == true)
            {

                var currentScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
                //Mouse's screen space coordinates are converted to world coordinates, plus the offset
                var currentPos = Camera.main.ScreenToWorldPoint(currentScreenPos) + offset;
                currentTransform.position = currentPos;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            currentTransform = null;
        }
    }
}
