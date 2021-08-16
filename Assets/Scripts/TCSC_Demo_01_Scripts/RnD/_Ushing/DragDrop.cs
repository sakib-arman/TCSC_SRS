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
   // public GameObject Deactive_Image;

    public GameObject Transform_Active_Cube;
    private Vector3 mOffset;
    private float mZCoord;
    private Vector3 resetPosition;
    public GameObject cube1;


    public GameObject pickRadio;
    public GameObject pickAntenna;
    public GameObject pickAccessories;
    public GameObject pickHandsetAccessories;

    public GameObject selectAntenna;
    public GameObject selectAccessories;

    public GameObject handsetArrow;

    //Texts
    public GameObject DragRadio;
    public GameObject PressB;
    public GameObject SelectAnteena;
    public GameObject SelectVehicleAntenna;
    public GameObject DragAntenna;
    public GameObject SelectAccessoriesText;
    public GameObject DragCableHead;
    public GameObject pressF;
    public GameObject DragHeadSet;


    public static int counter;

   // public GameObject NextButton;

    private void Start()
    {
        //counter = 0;
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
        if (Mathf.Abs(Transform_Active_Cube.transform.localPosition.x - cube1.transform.localPosition.x) <= .5f &&
            Transform_Active_Cube.transform.localPosition.y - cube1.transform.localPosition.y <= .5f)
        {
            Transform_Active_Cube.transform.localPosition = new Vector3(cube1.transform.localPosition.x, cube1.transform.localPosition.y, cube1.transform.localPosition.z);
            canvasGroup.alpha = 0f;
            Deactive_Cube.SetActive(false);

            ////turzo/////
            if(counter==0)
            {
                pickRadio.SetActive(false);
                //selectAntenna.SetActive(true);
                DragRadio.SetActive(false);
                PressB.SetActive(true);
                counter++;
            }
            else if(counter==1)
            {
                pickAntenna.SetActive(false);
                DragAntenna.SetActive(false);
                selectAccessories.SetActive(true);
                SelectAccessoriesText.SetActive(true);
                counter++;
            }

            else if(counter==2)
            {
                pickAccessories.SetActive(false);
                //pickHandsetAccessories.SetActive(true);
                DragCableHead.SetActive(false);
                pressF.SetActive(true);

                //NextButton.SetActive(true);
            }

            else if(counter==3)
            {
                DragHeadSet.SetActive(false);
                pickHandsetAccessories.SetActive(false);
                handsetArrow.SetActive(false);
            }

            
        }
        else
        {
            Transform_Active_Cube.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
            rectTransform.anchoredPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
            canvasGroup.alpha = 1f;
            Transform_Active_Cube.SetActive(false);
        }
    }

    //public void showHideGuide(int )
    //{

    //}

}
