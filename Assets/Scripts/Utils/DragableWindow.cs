
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragableWindow : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    // Private
    private RectTransform window;
    private Canvas ManiCanvas;
    private Image BackgroundImage;
    private Color BackgroundColor;
    // Public
    public bool isTransparent = true;
    public bool hasParent = true;



    // Start is called before the first frame update
    private void Awake()
    {


        if (window == null)
        {
            if (hasParent)
            {
                window = this.transform.parent.GetComponent<RectTransform>();
            }
            else
            {
                window = GetComponent<RectTransform>();
            }


        }
        if (ManiCanvas == null)
        {
            Transform TestCanvasTransform = transform.parent;
            while (TestCanvasTransform != null)
            {
                ManiCanvas = TestCanvasTransform.GetComponent<Canvas>();
                if (ManiCanvas != null)
                {
                    break;
                }
                TestCanvasTransform = TestCanvasTransform.parent;

            }
        }
        BackgroundImage = window.GetComponent<Image>();
        BackgroundColor = BackgroundImage.color;

    }

    public void OnDrag(PointerEventData eventData)
    {
        window.anchoredPosition += eventData.delta / ManiCanvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isTransparent)
        {
            BackgroundColor.a = 0.4f;
            BackgroundImage.color = BackgroundColor;
        }

        window.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isTransparent)
        {
            BackgroundColor.a = 1f;
            BackgroundImage.color = BackgroundColor;
        }
    }
}
