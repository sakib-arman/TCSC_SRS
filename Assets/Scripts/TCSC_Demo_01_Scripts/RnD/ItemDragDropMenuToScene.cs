using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragDropMenuToScene : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,  IDropHandler
{
    private Camera mainCamera;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector3 mOffset;
    private float mZCoord = 500f;

    [SerializeField] GameObject spawnObject;
    Transform spawnedTransform;
    Vector2 _spawnPosition;

    private Vector3 getMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //Logger.Log("Drag Start");
        //mZCoord = mainCamera.WorldToScreenPoint(spawnedTransform.position).z + 30f;
        //mZCoord = 500f;
        mOffset = new Vector3(_spawnPosition.x, _spawnPosition.y, mZCoord) - getMouseWorldPos();
        //spawnedTransform.localScale = Vector3.one;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 anchoredPosition = eventData.delta / canvas.scaleFactor;
        spawnedTransform.position = (Vector3)anchoredPosition + mOffset;
        //spawnedTransform.GetComponent<ItemDrag>().OnDrag(eventData);
        //throw new System.NotImplementedException();
        //Logger.Log("Dragging....");
        //spawnedTransform.position = Input.mousePosition;
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;// move instantiated obj/ rectT op alpha
        //spawnedTransform.position = getMouseWorldPos() + mOffset; //eventData.delta / canvas.scaleFactor;
        //Vector3 mousePt = Input.mousePosition;
        //mousePt.z = mZCoord;
        //spawnedTransform.position = new Vector3(mainCamera.WorldToScreenPoint(Input.mousePosition).x / canvas.scaleFactor, mainCamera.WorldToScreenPoint(Input.mousePosition).y / canvas.scaleFactor, mZCoord) + mOffset;
        //spawnedTransform.position = new Vector3(mainCamera.WorldToScreenPoint(Input.mousePosition).x, mainCamera.WorldToScreenPoint(Input.mousePosition).y, 50f);// + mOffset;
        //spawnedTransform.Translate(new Vector3(mainCamera.WorldToScreenPoint(Input.mousePosition).x, mainCamera.WorldToScreenPoint(Input.mousePosition).y, mZCoord) + mOffset);

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Logger.Log("Drag End");
        spawnedTransform.GetComponent<ItemDrag>().OnEndDrag(eventData);
    }
    public void OnDrop(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Logger.Log("Dropped");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Logger.Log("Pointer down");
        //Vector3 worldPos = mainCamera.WorldToScreenPoint(new Vector3(eventData.delta.x, eventData.delta.y, 0f));
        //Vector2 mousePos = new Vector2(worldPos.x, worldPos.y);
        if( RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, new Vector2(getMouseWorldPos().x, getMouseWorldPos().y) , mainCamera, out _spawnPosition))
        {
            //Logger.Log(_spawnPosition);
            Vector3 position = new Vector3(_spawnPosition.x, _spawnPosition.y, mZCoord);
            GameObject spawn = Instantiate(spawnObject, position, Quaternion.identity);
            spawnedTransform = spawn.transform;
            spawnedTransform.GetComponent<ItemDrag>().OnBeginDrag(eventData);
            //spawnedTransform.localScale = Vector3.one * 0.4f;
        }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        canvas = GameObject.Find("MenuCanvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
