using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSettings : MonoBehaviour
{
    //Barrett2090 Start
    public static void PowerOnOff(Barrett2090 radio)
    {
        if (radio.isPowerConnected)
        {
            if (radio.isPowerOn)
            {
                radio.powerSource.powerOutput = radio.powerOutput;
            }
            else
            {
                radio.powerSource.powerOutput = 0;
            }
        }
        else
        {
            radio.isPowerOn = false;
            radio.powerSource.powerOutput = 0;
            //radio.audioSource.Stop();

        }
        radio.displayControllerBarrett2090.PowerOnOff(radio.isPowerOn);

    }

    public static void ChangePowerMode(Barrett2090 radio)
    {
        if (radio.isPowerOn)
        {
            if (radio.stationType == StationType.MANPACK)
            {
                if (radio.powerMode == PowerMode.MANPACK_LOW)
                {
                    radio.powerMode = PowerMode.MANPACK_HIGH;
                }
                else
                {
                    radio.powerMode = PowerMode.MANPACK_LOW;
                }
            }
            else
            {
                if (radio.powerMode == PowerMode.BASESTATION_LOW || radio.powerMode == PowerMode.VEHICLE_LOW)
                {
                    radio.powerMode = PowerMode.BASESTATION_MEDIUM;
                }
                else if (radio.powerMode == PowerMode.BASESTATION_MEDIUM || radio.powerMode == PowerMode.VEHICLE_MEDIUM)
                {
                    radio.powerMode = PowerMode.BASESTATION_HIGH;
                }
                else
                {
                    radio.powerMode = PowerMode.BASESTATION_LOW;
                }

            }

        }
        Logger.Log(radio, radio.powerMode.ToString());
        radio.powerSource.powerOutput = radio.powerOutput;

    }
    //Barrett2090 End
    //Barrett2050 Start
    //public static void PowerOnOff(Barrett2050 radio)
    //{
    //    if (radio.isPowerConnected)
    //    {
    //        if (radio.isPowerOn)
    //        {
    //            radio.powerSource.powerOutput = radio.powerOutput;
    //        }
    //        else
    //        {
    //            radio.powerSource.powerOutput = 0;
    //        }
    //    }
    //    else
    //    {
    //        radio.isPowerOn = false;
    //        radio.powerSource.powerOutput = 0;
    //        //radio.audioSource.Stop();

    //    }
    //    radio.displayControllerBarrett2050.PowerOnOff(radio.isPowerOn);

    //}

    //public static void ChangePowerMode(Barrett2050 radio)
    //{
    //    if (radio.isPowerOn)
    //    {
    //        if (radio.stationType == StationType.MANPACK)
    //        {
    //            if (radio.powerMode == PowerMode.MANPACK_LOW)
    //            {
    //                radio.powerMode = PowerMode.MANPACK_HIGH;
    //            }
    //            else
    //            {
    //                radio.powerMode = PowerMode.MANPACK_LOW;
    //            }
    //        }
    //        else
    //        {
    //            if (radio.powerMode == PowerMode.BASESTATION_LOW || radio.powerMode == PowerMode.VEHICLE_LOW)
    //            {
    //                radio.powerMode = PowerMode.BASESTATION_MEDIUM;
    //            }
    //            else if (radio.powerMode == PowerMode.BASESTATION_MEDIUM || radio.powerMode == PowerMode.VEHICLE_MEDIUM)
    //            {
    //                radio.powerMode = PowerMode.BASESTATION_HIGH;
    //            }
    //            else
    //            {
    //                radio.powerMode = PowerMode.BASESTATION_LOW;
    //            }

    //        }

    //    }
    //    Logger.Log(radio, radio.powerMode.ToString());
    //    radio.powerSource.powerOutput = radio.powerOutput;

    //}
    //Barrett2050 End
    //QMac90M Start
    //public static void PowerOnOff(QMAC90M radio)
    //{
    //    if (radio.isPowerConnected)
    //    {
    //        if (radio.isPowerOn)
    //        {
    //            radio.powerSource.powerOutput = radio.powerOutput;
    //        }
    //        else
    //        {
    //            radio.powerSource.powerOutput = 0;
    //        }
    //    }
    //    else
    //    {
    //        radio.isPowerOn = false;
    //        radio.powerSource.powerOutput = 0;
    //        //radio.audioSource.Stop();

    //    }
    //    radio.qMACDisplayController.PowerOnOff(radio.isPowerOn);
    //    radio.powerSource.powerOutput = radio.powerOutput;

    //}

    //-----------------------------------------//
    //       QMAC-90 POWER SETTING             //
    //-----------------------------------------//
    //public static void QMACPowerMode(QMAC90M radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CHANNEL:
    //            radio.qMACDisplayController.channelInput.SetActive(false);
    //            radio.qMACDisplayController.displayText.transform.Find("MODConfirmation").gameObject.SetActive(true);
    //            radio.qMACDisplayController.displayText.transform.Find("MODConfirmation").GetComponent<Text>().text = radio.activeChannel.power;
    //            radio.Navigator.Pop();
    //            radio.Invoke("ChangePowerDisplay", 2f);
    //            break;
    //        case ButtonKeyCode.CHANNEL_DOWN:
    //            radio.qMACDisplayController.channelInput.SetActive(false);
    //            radio.qMACDisplayController.displayText.transform.Find("MODConfirmation").gameObject.SetActive(true);
    //            radio.qMACDisplayController.displayText.transform.Find("MODConfirmation").GetComponent<Text>().text = radio.activeChannel.power;
    //            radio.Invoke("ChangePowerDisplay", 2f);
    //            break;
    //    }

    //}

    //QMac90M End

    //FSG90 Start
    //public static void PowerOnOff(FSG90HI71 radio)
    //{
    //    if (radio.isPowerConnected)
    //    {
    //        if (radio.isPowerOn)
    //        {
    //            radio.powerSource.powerOutput = radio.powerOutput;
    //        }
    //        else
    //        {
    //            radio.powerSource.powerOutput = 0;
    //        }
    //    }
    //    else
    //    {
    //        radio.isPowerOn = false;
    //        radio.powerSource.powerOutput = 0;
    //        //radio.audioSource.Stop();

    //    }
    //    radio.FSG90HI71DisplayController.PowerOnOff(radio.isPowerOn);
    //    radio.powerSource.powerOutput = radio.powerOutput;

    //}

    //-----------------------------------------//
    //       FSG-90             //
    //-----------------------------------------//

    //-----------------------------------------//
    //       CD116 POWER SETTING             //
    //-----------------------------------------//
    //public static void PowerOnOff(CD116 fieldExchange)
    //{
    //    if (fieldExchange.isPowerConnected)
    //    {
    //        if (fieldExchange.isPowerOn)
    //        {
    //            fieldExchange.powerSource.powerOutput = fieldExchange.powerOutput;
    //        }
    //        else
    //        {
    //            fieldExchange.powerSource.powerOutput = 0;
    //        }
    //    }
    //    else
    //    {
    //        fieldExchange.isPowerOn = false;
    //        fieldExchange.powerSource.powerOutput = 0;
    //        //radio.audioSource.Stop();

    //    }
    //    fieldExchange.cd116DisplayController.PowerOnOff(fieldExchange.isPowerOn);
    //    fieldExchange.powerSource.powerOutput = fieldExchange.powerOutput;

    //}
    //-----------------------------------------//
    //       CD116 POWER SETTING END           //
    //-----------------------------------------//
}
