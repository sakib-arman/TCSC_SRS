using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoppingSetting : MonoBehaviour
{
    private static int CursorIndex = 0;
    /*==========================
    Barret2090
    ==========================*/
    public static void SetHoppingPin(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text HoppingPinInput = radio.displayControllerBarrett2090.MenuPanel.transform.Find("HoppingPinInput").GetComponent<Text>();
        char[] text = HoppingPinInput.text.ToCharArray();
        switch (buttonID)
        {
            case ButtonKeyCode.OK:
                if (CursorIndex == 0)
                {
                    return;
                }
                HoppingPinInput.text = ReplaceDash(HoppingPinInput.text);
                radio.hoppingPin = int.Parse(HoppingPinInput.text);
                radio.Navigator.Pop();
                radio.menuIndexStack.Pop();
                radio.displayControllerBarrett2090.MenuPanel.transform.Find("HoppingPinInput").gameObject.SetActive(false);
                radio.displayControllerBarrett2090.setMenuText(radio.getFromDictionaryByIndex(BarrettMenu.info["General"], radio.menuIndexStack.Peek()));
                 radio?.onPressed(ButtonKeyCode.REFRESH, 1);
                break;
            case ButtonKeyCode.CANCEL:

                if (CursorIndex > 0)
                {
                    text[CursorIndex] = '-';
                    HoppingPinInput.text = new string(text);
                    CursorIndex--;
                    CursorIndex = CursorIndex < 0 ? 0 : CursorIndex;
                }
                else
                {
                    CursorIndex = 0;
                    radio.Navigator.Pop();
                    radio.menuIndexStack.Pop();
                    radio.displayControllerBarrett2090.MenuPanel.transform.Find("HoppingPinInput").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.setMenuText(radio.getFromDictionaryByIndex(BarrettMenu.info["General"], radio.menuIndexStack.Peek()));
                    radio?.onPressed(ButtonKeyCode.REFRESH, 1);

                }
                break;
            default:
                if (CursorIndex < 8)
                {
                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                    text[CursorIndex] = input;
                    HoppingPinInput.text = new string(text);
                    CursorIndex++;
                    CursorIndex = CursorIndex >= 8 ? 7 : CursorIndex;
                }
                break;
        }
    }
    public static string ReplaceDash(string str)
    {
        int dash = (str.Length) - CursorIndex;
        string formated_text = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (i < dash)
            {
                formated_text += '0';
            }
            else
            {
                formated_text += str.ToCharArray()[i - dash];
            }

        }
        return formated_text;
    }
    /*==========================
    Barret2090 End
    ==========================*/
    /*==========================
    Barret2050
    ==========================*/
    //public static void SetHoppingPin(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text HoppingPinInput = radio.displayControllerBarrett2050.MenuPanel.transform.Find("HoppingPinInput").GetComponent<Text>();
    //    char[] text = HoppingPinInput.text.ToCharArray();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.OK:
    //            if (CursorIndex == 0)
    //            {
    //                return;
    //            }
    //            HoppingPinInput.text = ReplaceDash(HoppingPinInput.text);
    //            radio.hoppingPin = int.Parse(HoppingPinInput.text);
    //            radio.Navigator.Pop();
    //            radio.menuIndexStack.Pop();
    //            radio.displayControllerBarrett2050.MenuPanel.transform.Find("HoppingPinInput").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.setMenuText(radio.getFromDictionaryByIndex(BarrettMenu.info["General"], radio.menuIndexStack.Peek()));
    //             radio?.onPressed(ButtonKeyCode.REFRESH, 1);
    //            break;
    //        case ButtonKeyCode.CANCEL:

    //            if (CursorIndex > 0)
    //            {
    //                text[CursorIndex] = '-';
    //                HoppingPinInput.text = new string(text);
    //                CursorIndex--;
    //                CursorIndex = CursorIndex < 0 ? 0 : CursorIndex;
    //            }
    //            else
    //            {
    //                CursorIndex = 0;
    //                radio.Navigator.Pop();
    //                radio.menuIndexStack.Pop();
    //                radio.displayControllerBarrett2050.MenuPanel.transform.Find("HoppingPinInput").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.setMenuText(radio.getFromDictionaryByIndex(BarrettMenu.info["General"], radio.menuIndexStack.Peek()));
    //                radio?.onPressed(ButtonKeyCode.REFRESH, 1);

    //            }
    //            break;
    //        default:
    //            if (CursorIndex < 8)
    //            {
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                text[CursorIndex] = input;
    //                HoppingPinInput.text = new string(text);
    //                CursorIndex++;
    //                CursorIndex = CursorIndex >= 8 ? 7 : CursorIndex;
    //            }
    //            break;
    //    }
    //}
    /*==========================
    Barret2090
    ==========================*/
}
