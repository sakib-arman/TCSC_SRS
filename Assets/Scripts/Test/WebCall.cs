using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class WebCall : MonoBehaviour
{
    public string url = "http://192.168.0.108:8000/api/send/fcm";
    public string response;
    public void PressGetButton()
    {
        StartCoroutine(simpleGetRequest());
    }

    IEnumerator simpleGetRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Logger.Log(this, request.error);
        }
        else
        {
            response = request.ToString();
            Logger.Log(this, request.downloadHandler.text);
        }
    }
}
