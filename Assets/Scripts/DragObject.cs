using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {
    private Vector3 point;
    private void OnMouseDrag () {
        Vector3 point = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        point.y = gameObject.transform.position.y;
        gameObject.transform.position = point;
    }
}