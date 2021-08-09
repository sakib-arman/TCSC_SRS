

using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Sprite icon;
    public int counter;
    public GameObject spawnObject;
    private void Start()
    {
        transform.GetComponentInChildren<Text>().text = counter.ToString();
        transform.GetChild(0).GetComponent<Image>().sprite = icon;
        transform.GetChild(1).GetComponent<Image>().sprite = icon;

    }


}
