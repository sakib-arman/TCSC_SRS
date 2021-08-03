using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCube : MonoBehaviour
{
    private Vector3 resetPosition;
    public GameObject cube1;

    private void Start()
    {
        resetPosition = this.transform.localPosition;
    }
    private void OnMouseUp()
    {
        if (Mathf.Abs(this.transform.localPosition.x - cube1.transform.localPosition.x) <= .1f &&
            this.transform.localPosition.y - cube1.transform.localPosition.y <= .1f)
        {
            this.transform.localPosition = new Vector3(cube1.transform.localPosition.x, cube1.transform.localPosition.y, cube1.transform.localPosition.z);
            //cube1.SetActive(false);


        }
        else
        {
            this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
        }
    }

}
