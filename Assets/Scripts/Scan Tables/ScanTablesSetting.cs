using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScanTablesSetting : MonoBehaviour
{
    /*==========================
    Barret2090
    ==========================*/
    public static void AddEntry(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        switch (buttonID)
        {
            case ButtonKeyCode.DOWN_ARROW:
                radio.MenuIndex--;
                if (radio.MenuIndex < 0)
                {
                    radio.MenuIndex = radio.channelList.Count - 1;
                }
                radio.menuIndexStack.Pop();
                radio.menuIndexStack.Push(radio.MenuIndex);
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel;
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + radio.channelList[radio.MenuIndex].channelNumber + "  Entry: " + (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count);
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + radio.channelList[radio.MenuIndex].tx + " kHz";
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + radio.channelList[radio.MenuIndex].channelLabel;
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("FooterText").GetComponent<Text>().text = "Select Channel to Scan";
                break;
            case ButtonKeyCode.UP_ARROW:
                radio.MenuIndex++;
                if (radio.MenuIndex > radio.channelList.Count - 1)
                {
                    radio.MenuIndex = 0;
                }
                radio.menuIndexStack.Pop();
                radio.menuIndexStack.Push(radio.MenuIndex);
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel;
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + radio.channelList[radio.MenuIndex].channelNumber + "  Entry: " + (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count);
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + radio.channelList[radio.MenuIndex].tx + " kHz";
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + radio.channelList[radio.MenuIndex].channelLabel;
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("FooterText").GetComponent<Text>().text = "Select Channel to Scan";
                break;
            case ButtonKeyCode.OK:
                //scanTableChannelList[BarrettMenu.scanListIndexSelected].Add(MenuIndex);
                BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Add(radio.channelList[radio.MenuIndex]);
                radio.Navigator.Pop();
                radio.menuIndexStack.Pop();
                radio.MenuIndex = radio.menuIndexStack.Peek();
                radio.displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[radio.MenuIndex]);
                radio.displayControllerBarrett2090.ScanTablePanel.SetActive(false);
                break;
            case ButtonKeyCode.CANCEL:
                radio.Navigator.Pop();
                radio.menuIndexStack.Pop();
                radio.MenuIndex = radio.menuIndexStack.Peek();
                radio.displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[radio.MenuIndex]);
                radio.displayControllerBarrett2090.ScanTablePanel.SetActive(false);
                break;
        }
    }
    public static void SearchEntry(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        switch (buttonID)
        {
            case ButtonKeyCode.DOWN_ARROW:
                radio.MenuIndex--;
                if (radio.MenuIndex < 0)
                {
                    radio.MenuIndex = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count - 1;
                }
                radio.menuIndexStack.Pop();
                radio.menuIndexStack.Push(radio.MenuIndex);
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel;
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[radio.MenuIndex].channelNumber + "  Entry: " + (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count);
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[radio.MenuIndex].tx + " kHz";
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[radio.MenuIndex].channelLabel;
                break;
            case ButtonKeyCode.UP_ARROW:
                radio.MenuIndex++;
                if (radio.MenuIndex > BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count - 1)
                {
                    radio.MenuIndex = 0;
                }
                radio.menuIndexStack.Pop();
                radio.menuIndexStack.Push(radio.MenuIndex);
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel;
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[radio.MenuIndex].channelNumber + "  Entry: " + (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count);
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[radio.MenuIndex].tx + " kHz";
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[radio.MenuIndex].channelLabel;
                break;
            case ButtonKeyCode.OK:
                //scanTableChannelList[BarrettMenu.scanListIndexSelected].Add(MenuIndex);
                BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Add(radio.channelList[radio.MenuIndex]);
                radio.Navigator.Pop();
                radio.menuIndexStack.Pop();
                radio.MenuIndex = radio.menuIndexStack.Peek();
                radio.displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[radio.MenuIndex]);
                radio.displayControllerBarrett2090.ScanTablePanel.SetActive(false);
                break;
            case ButtonKeyCode.CANCEL:
                radio.Navigator.Pop();
                radio.menuIndexStack.Pop();
                radio.MenuIndex = radio.menuIndexStack.Peek();
                radio.displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[radio.MenuIndex]);
                radio.displayControllerBarrett2090.ScanTablePanel.SetActive(false);
                break;
        }
    }
    static bool isEditable = true;
    public static void ChangeScanTableLabel(Barrett2090 radio, ButtonKeyCode buttonID)
    {

        Text labelText = radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("ChangeLabelPanel").Find("LabelText").GetComponent<Text>();

        switch (buttonID)
        {
            case ButtonKeyCode.OK:
                BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel = labelText.text;
                radio.Navigator.Pop();
                radio.Navigator.Pop();
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("ChangeLabelPanel").gameObject.SetActive(false);
                radio.displayControllerBarrett2090.ScanTablePanel.SetActive(false);
                radio.keyboardType = KeyboardType.NUMERICAL;
                isEditable = false;
                break;
            case ButtonKeyCode.CANCEL:
                if (labelText.text.Length == 0)
                {
                    radio.Navigator.Pop();
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("ChangeLabelPanel").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.ScanTablePanel.SetActive(false);
                    radio.keyboardType = KeyboardType.NUMERICAL;
                }
                else
                {
                    labelText.text = labelText.text.Remove(labelText.text.Length - 1);
                }

                break;
            case ButtonKeyCode.CAPS_LOCK:
                radio.isCapsLockOn = !radio.isCapsLockOn;
                radio.displayControllerBarrett2090.ScanTablePanel.transform.Find("ChangeLabelPanel").Find("Footer").gameObject.SetActive(radio.isCapsLockOn);
                break;
            default:
                if (labelText.text.Length < 13)
                {
                    char input = NumberPadSetting.KeyPadInput(radio, buttonID);
                    labelText.text += input;
                }

                break;
        }


    }
    /*==========================
    Barret2090 End
    ==========================*/
    /*==========================
    Barret2050
    ==========================*/
    //public static void AddEntry(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.MenuIndex--;
    //            if (radio.MenuIndex < 0)
    //            {
    //                radio.MenuIndex = radio.channelList.Count - 1;
    //            }
    //            radio.menuIndexStack.Pop();
    //            radio.menuIndexStack.Push(radio.MenuIndex);
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].tableLabel;
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + radio.channelList[radio.MenuIndex].channelNumber + "  Entry: " + (Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList.Count);
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + radio.channelList[radio.MenuIndex].tx + " kHz";
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + radio.channelList[radio.MenuIndex].channelLabel;
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("FooterText").GetComponent<Text>().text = "Select Channel to Scan";
    //            break;
    //        case ButtonKeyCode.UP_ARROW:
    //            radio.MenuIndex++;
    //            if (radio.MenuIndex > radio.channelList.Count - 1)
    //            {
    //                radio.MenuIndex = 0;
    //            }
    //            radio.menuIndexStack.Pop();
    //            radio.menuIndexStack.Push(radio.MenuIndex);
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].tableLabel;
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + radio.channelList[radio.MenuIndex].channelNumber + "  Entry: " + (Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList.Count);
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + radio.channelList[radio.MenuIndex].tx + " kHz";
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + radio.channelList[radio.MenuIndex].channelLabel;
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("FooterText").GetComponent<Text>().text = "Select Channel to Scan";
    //            break;
    //        case ButtonKeyCode.OK:
    //            //scanTableChannelList[Barrett2050Menu.scanListIndexSelected].Add(MenuIndex);
    //            Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList.Add(radio.channelList[radio.MenuIndex]);
    //            radio.Navigator.Pop();
    //            radio.menuIndexStack.Pop();
    //            radio.MenuIndex = radio.menuIndexStack.Peek();
    //            radio.displayControllerBarrett2050.setMenuText(Barrett2050Menu.scanListOptions[radio.MenuIndex]);
    //            radio.displayControllerBarrett2050.ScanTablePanel.SetActive(false);
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.Navigator.Pop();
    //            radio.menuIndexStack.Pop();
    //            radio.MenuIndex = radio.menuIndexStack.Peek();
    //            radio.displayControllerBarrett2050.setMenuText(Barrett2050Menu.scanListOptions[radio.MenuIndex]);
    //            radio.displayControllerBarrett2050.ScanTablePanel.SetActive(false);
    //            break;
    //    }
    //}
    //public static void SearchEntry(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.MenuIndex--;
    //            if (radio.MenuIndex < 0)
    //            {
    //                radio.MenuIndex = Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList.Count - 1;
    //            }
    //            radio.menuIndexStack.Pop();
    //            radio.menuIndexStack.Push(radio.MenuIndex);
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].tableLabel;
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList[radio.MenuIndex].channelNumber + "  Entry: " + (Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList.Count);
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList[radio.MenuIndex].tx + " kHz";
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList[radio.MenuIndex].channelLabel;
    //            break;
    //        case ButtonKeyCode.UP_ARROW:
    //            radio.MenuIndex++;
    //            if (radio.MenuIndex > Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList.Count - 1)
    //            {
    //                radio.MenuIndex = 0;
    //            }
    //            radio.menuIndexStack.Pop();
    //            radio.menuIndexStack.Push(radio.MenuIndex);
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].tableLabel;
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList[radio.MenuIndex].channelNumber + "  Entry: " + (Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList.Count);
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList[radio.MenuIndex].tx + " kHz";
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList[radio.MenuIndex].channelLabel;
    //            break;
    //        case ButtonKeyCode.OK:
    //            //scanTableChannelList[Barrett2050Menu.scanListIndexSelected].Add(MenuIndex);
    //            Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].scanList.Add(radio.channelList[radio.MenuIndex]);
    //            radio.Navigator.Pop();
    //            radio.menuIndexStack.Pop();
    //            radio.MenuIndex = radio.menuIndexStack.Peek();
    //            radio.displayControllerBarrett2050.setMenuText(Barrett2050Menu.scanListOptions[radio.MenuIndex]);
    //            radio.displayControllerBarrett2050.ScanTablePanel.SetActive(false);
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.Navigator.Pop();
    //            radio.menuIndexStack.Pop();
    //            radio.MenuIndex = radio.menuIndexStack.Peek();
    //            radio.displayControllerBarrett2050.setMenuText(Barrett2050Menu.scanListOptions[radio.MenuIndex]);
    //            radio.displayControllerBarrett2050.ScanTablePanel.SetActive(false);
    //            break;
    //    }
    //}
    //public static void ChangeScanTableLabel(Barrett2050 radio, ButtonKeyCode buttonID)
    //{

    //    Text labelText = radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("ChangeLabelPanel").Find("LabelText").GetComponent<Text>();

    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.OK:
    //            Barrett2050Menu.scanListNames[Barrett2050Menu.scanListIndexSelected].tableLabel = labelText.text;
    //            radio.Navigator.Pop();
    //            radio.Navigator.Pop();
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("ChangeLabelPanel").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.ScanTablePanel.SetActive(false);
    //            radio.keyboardType = KeyboardType.NUMERICAL;
    //            isEditable = false;
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (labelText.text.Length == 0)
    //            {
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("ChangeLabelPanel").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.ScanTablePanel.SetActive(false);
    //                radio.keyboardType = KeyboardType.NUMERICAL;
    //            }
    //            else
    //            {
    //                labelText.text = labelText.text.Remove(labelText.text.Length - 1);
    //            }

    //            break;
    //        case ButtonKeyCode.CAPS_LOCK:
    //            radio.isCapsLockOn = !radio.isCapsLockOn;
    //            radio.displayControllerBarrett2050.ScanTablePanel.transform.Find("ChangeLabelPanel").Find("Footer").gameObject.SetActive(radio.isCapsLockOn);
    //            break;
    //        default:
    //            if (labelText.text.Length < 13)
    //            {
    //                char input = NumberPadSetting.KeyPadInput(radio, buttonID);
    //                labelText.text += input;
    //            }

    //            break;
    //    }


    //}
    /*==========================
    Barret2090 End
    ==========================*/
}
