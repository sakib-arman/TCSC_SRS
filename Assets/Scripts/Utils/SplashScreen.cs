using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public float Seconds = 3f;
    void OnEnable()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(HideSplashScreen(Seconds));
    }
    private IEnumerator HideSplashScreen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }

}
