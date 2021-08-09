using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyAnimController : MonoBehaviour
{
    private Image[] layers;
    private Color32 FillColor = new Color32(45, 38, 43, 255);
    public bool isStart = false;
    public bool isIdle = false;
    public float time = 0;
    public float delay = 0.1f;
    public int index = 0;
    public bool isAntiClock = true;
    private Barrett2090 barrett;
    //private Barrett2050 barrett2050;
    //private QMAC90M qmac90m;




    private void Start()
    {
        layers = this.GetComponentsInChildren<Image>();
        barrett = GetComponentInParent<Barrett2090>();
        //barrett2050 = GetComponentInParent<Barrett2050>();
        //qmac90m = GetComponentInParent<QMAC90M>();
    }
    private void Update()
    {
        if (isStart)
        {
            PlayStartAnim();
        }
        if (isIdle)
        {
            int rand = Random.Range(1, 15);
            if (Time.time - time > 1)
            {
                for (int i = 0; i < layers.Length; i++)
                {
                    if (i <= rand)
                    {
                        layers[i].color = FillColor;
                    }
                    else
                    {
                        layers[i].color = barrett.displayControllerBarrett2090.BaretDisplayColor;
                        //layers[i].color = barrett2050.displayControllerBarrett2050.BaretDisplayColor;
                    }

                }
                time = Time.time;
            }
        }

    }
    public void startTransmitAnim(float seconds)
    {

        StartCoroutine(WaitAndStart(seconds));
    }
    private IEnumerator WaitAndStart(float waitTime)
    {
        this.isIdle = false;
        BlankAllSprite();
        yield return new WaitForSeconds(waitTime + 1f);
        this.isStart = true;
        index = 0;

    }
    private void PlayStartAnim()
    {
        if (isAntiClock)
        {
            if (Time.time - time > delay)
            {
                layers[index].color = FillColor;
                index++;
                if (index > layers.Length - 1)
                {
                    isAntiClock = false;
                    index--;

                }


                time = Time.time;
            }
        }
        else
        {
            if (Time.time - time > delay)
            {
                layers[index].color = barrett.displayControllerBarrett2090.BaretDisplayColor;
                //layers[index].color = barrett2050.displayControllerBarrett2050.BaretDisplayColor;
                index--;
                if (index <= 0)
                {
                    isStart = false;
                    isIdle = true;

                }

                time = Time.time;
            }

        }
    }
    private void BlankAllSprite()
    {
        foreach (Image layer in layers)
        {
            layer.color = barrett.displayControllerBarrett2090.BaretDisplayColor;
            //layer.color = barrett2050.displayControllerBarrett2050.BaretDisplayColor;
        }
        this.isAntiClock = true;
    }
}
