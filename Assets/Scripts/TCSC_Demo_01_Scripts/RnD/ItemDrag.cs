using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Logger.Log("Drag Start ....");
        //throw new System.NotImplementedException();
        //transform.position = 
    }

    public void OnDrag(PointerEventData eventData)
    {
        Logger.Log("Dragging....");
        //throw new System.NotImplementedException();
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
