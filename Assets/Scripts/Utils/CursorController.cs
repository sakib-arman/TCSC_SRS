using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{

    public bool isVertical = false;
    public GameObject Cursor;
    public float CursorDelay = 0.3f;
    private Text TextObject;
    public Vector3 CursorOffset = new Vector3(70, -25, 0);
    public Vector3 CursorOffsetHorizon = new Vector3(180, 100, 0);
    private float _timer = 0f;
    private RectTransform rectTransform;
    float widthScale = 0.75f;
    float heightScale = 0.125f;
    public int CursorIndex = 0;
    TextGenerator gen;
    int length = 1;
    void Start()
    {
        TextObject = this.GetComponent<Text>();
        rectTransform = Cursor.GetComponent<RectTransform>();
        Cursor.GetComponent<Image>().color = TextObject.color;
        gen = TextObject.cachedTextGenerator;

    }
    void Update()
    {
        //place cursor
        if (gen.characterCount > 1)
        {
            ReCheckLength();
            var v = gen.verts[gen.verts.Count - 1].position;
            v = TextObject.transform.TransformPoint(gen.verts[CursorIndex * 4 + 2].position);
            if (isVertical)
            {
                Cursor.transform.position = v + CursorOffset;
                rectTransform.sizeDelta = new Vector2(TextObject.fontSize * widthScale, TextObject.fontSize * heightScale);

            }
            else
            {
                Cursor.transform.position = v + CursorOffsetHorizon;
                rectTransform.sizeDelta = new Vector2(TextObject.fontSize * heightScale, TextObject.fontSize * widthScale);
            }

        }

        //do blink
        _timer += Time.deltaTime;
        if (_timer > CursorDelay)
        {
            _timer = 0f;
            Cursor.SetActive(!Cursor.activeSelf);
        }
    }
    public void ReCheckLength()
    {
        if (gen.characterCount <= 2)
        {
            CursorIndex = 0;
        }
        else if (length > gen.characterCount)
        {
            CursorIndex -= 1;
        }
        else if (length < gen.characterCount)
        {
            CursorIndex += 1;
        }
        length = gen.characterCount;

    }


}
