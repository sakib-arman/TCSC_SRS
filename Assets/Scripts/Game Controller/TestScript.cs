using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    int dir = 1;
    [SerializeField] int amp = 10;
    [SerializeField] float min;
    [SerializeField] float max;
    [SerializeField] GameObject barrett;
    // Start is called before the first frame update
    void Start()
    {
        //while (true)
        //{
        //    yield return Sine(barrett.transform.Find("RootNode").GetChild(1)); 
        //}
        //Logger.Log(barrett.transform.Find("RootNode").childCount);
        //StartCoroutine(Sine(barrett.transform.Find("RootNode").GetChild(1)));
        //for (int i = 0; i < barrett.transform.Find("RootNode").childCount; i++)
        //{
        //    StartCoroutine(Sine(barrett.transform.Find("RootNode").GetChild(i)));
        //}

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Sine(Transform item)
    {
        dir *= -1;
        //Vector3 pos = item.position;
        Vector3 pos = item.position + Vector3.up * dir;
        while (Mathf.Abs(item.position.y) < Mathf.Abs(dir * amp))
        {
            //if (pos.y < Mathf.Abs(dir * amp))
            //{
            item.position = Vector3.Lerp(item.position, pos, 2f); //new Vector3(pos.x, pos.y + Mathf.Sin(Time.time * amp * dir), pos.z);
            yield return null;
            //}
        }
        item.position = new Vector3(pos.x, pos.y  * amp * dir, pos.z);
    }
}
