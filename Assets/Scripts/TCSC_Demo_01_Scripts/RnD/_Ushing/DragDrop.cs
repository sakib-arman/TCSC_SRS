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
    public GameObject Deactive_Image;

    public GameObject Transform_Active_Cube;
    private Vector3 mOffset;
    private float mZCoord;

    private Vector3 resetPosition;
    public GameObject cube1;

    private void Start()
    {
        resetPosition = Transform_Active_Cube.transform.localPosition;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Transform_Active_Cube.SetActive(true);

        mZCoord = Camera.main.WorldToScreenPoint(Transform_Active_Cube.transform.position).z;
        mOffset = Transform_Active_Cube.transform.position - getmoueWoeldPos();
    }

    public void OnDrag(PointerEventData eventData)
    {

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Transform_Active_Cube.transform.position = getmoueWoeldPos() + mOffset;
        canvasGroup.alpha = 0f;
    }
    private Vector3 getmoueWoeldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(Transform_Active_Cube.transform.localPosition.x - cube1.transform.localPosition.x) <= .8f &&
            Transform_Active_Cube.transform.localPosition.y - cube1.transform.localPosition.y <= .8f)
        {
            Transform_Active_Cube.transform.localPosition = new Vector3(cube1.transform.localPosition.x, cube1.transform.localPosition.y, cube1.transform.localPosition.z);
            canvasGroup.alpha = 0f;
            Deactive_Cube.SetActive(false);
            
        }
        else
        {
            Transform_Active_Cube.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
            rectTransform.anchoredPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
            canvasGroup.alpha = 1f;
            Transform_Active_Cube.SetActive(false);
        }
    }

}
