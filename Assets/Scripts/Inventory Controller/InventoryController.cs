
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject Item;
    int counter;
    public Transform panel;
    public Sprite[] sprites;
    public GameObject[] spawnObjects;
    InventoryItem inventoryItem;
    void Start()
    {
        counter = spawnObjects.Length - 1;
        while (counter >= 0)
        {
            inventoryItem = Item.GetComponent<InventoryItem>();
            inventoryItem.counter = UnityEngine.Random.Range(2, 10);
            inventoryItem.icon = sprites[counter];
            inventoryItem.spawnObject = spawnObjects[counter];
            GameObject a = Instantiate(Item) as GameObject;
            a.transform.SetParent(panel.transform, false);
            counter--;
        }

    }
}
