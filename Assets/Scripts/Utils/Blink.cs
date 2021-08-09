using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{

    public bool isBlinking = false;
    float timer = 0;
    float delay = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (isBlinking)
        {
            
            if (timer > delay )
            {
                this.gameObject.SetActive(!this.gameObject.activeSelf);
                Logger.Log(this.gameObject.activeSelf);
                timer = 0;
            }
            timer += Time.deltaTime;
        }


    }
}
