using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public GameObject Deactive_Cube;
    public GameObject Active_Cube;
    public GameObject Deactive_Image;

    public GameObject Transform_Active_Cube;
    private Vector3 mOffset;
    private float mZCoord;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0f;
        Transform_Active_Cube.SetActive(true);

        mZCoord = Camera.main.WorldToScreenPoint(Transform_Active_Cube.gameObject.transform.position).z;
        mOffset = Transform_Active_Cube.gameObject.transform.position - getmoueWoeldPos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        Transform_Active_Cube.gameObject.transform.position = getmoueWoeldPos() + mOffset;
    }
    private Vector3 getmoueWoeldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Active_Cube.SetActive(true);
        Deactive_Cube.SetActive(false);
        Transform_Active_Cube.SetActive(false);
    }

}
