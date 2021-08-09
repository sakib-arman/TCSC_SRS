
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    SpawnHandler spawnHandler;
    GameObject slectedGameObject;
    private void Start()
    {
        spawnHandler = GameObject.Find("GameController").GetComponent<SpawnHandler>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;
        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            slectedGameObject = eventData.pointerDrag.transform.parent.gameObject;
            Text counter = slectedGameObject.transform.GetComponentInChildren<Text>();
            if (int.Parse(counter.text) > 0)
            {
                counter.text = (int.Parse(counter.text) - 1).ToString();
                spawnHandler.SpawnObject(slectedGameObject.GetComponent<InventoryItem>().spawnObject);
            }




        }
    }
}
