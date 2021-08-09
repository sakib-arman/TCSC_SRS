using UnityEngine;
using System;
using System.Collections;

public class DateTimeSettings : MonoBehaviour
{
    public static void SetRadioTime(Barrett2090 radio , DateTime dateTime)
    {
        radio.radioDateTime = dateTime;
    }
    public static void PlayAlerm(Barrett2090 radio)
    {
        Logger.Log(radio, "Alarm On");
        
    }
   

}
