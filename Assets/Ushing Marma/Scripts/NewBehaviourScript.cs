using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject cube1;
    private Vector3 mOffset;
    private float mZCoord;
    private bool moving;

    private Vector3 resetPosition;

    private void Start()
    {
        resetPosition = this.transform.localPosition;
    }
    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - getmoueWoeldPos();
    }
     private Vector3 getmoueWoeldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
     void OnMouseDrag()
    {
        transform.position = getmoueWoeldPos() + mOffset;
    }
    private void OnMouseUp()
    {
        if(Mathf.Abs(this.transform.localPosition.x - cube1.transform.localPosition.x)<= .8f &&
            this.transform.localPosition.y - cube1.transform.localPosition.y <= .8f)
        {
           this.transform.localPosition = new Vector3(cube1.transform.localPosition.x, cube1.transform.localPosition.y, cube1.transform.localPosition.z);
            cube1.SetActive(false);


        }
        else
        {
            this.transform.localPosition = new Vector3(resetPosition.x,resetPosition.y,resetPosition.z);
        }
    }
}
