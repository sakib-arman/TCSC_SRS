using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using System.Globalization;

public class ChannelSetting : MonoBehaviour
{
    private static bool isRxTxSet = false;
    public static int cursorIndex = 0;
    private static bool isNumberPadActive = false;
    private static bool isRxEditable = false;
    private static int counter_prog = 0;
    private static int counter_prog_Tx = 0;
    private static int counter_stage = 0;

    //XV3088 variable
    public static bool isSdxEnable = false;
    public static bool isSelCallEnable = false;
    public static bool isSecureCallEnable = false;
    public static bool isSqlCallEnable = false;
    public static bool isProgEnable = false;

    //Q-MAC 90
    private static int count_press = 0;
    /*======================
    Barrett2090 Settings Start
    ======================*/
    public static void ChannelSwitching(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        // NumberPadSetting.NumberPad(radio, buttonID);
        switch (buttonID)
        {
            //Number pad Button setup 
            case ButtonKeyCode.NUM_0:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('0', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_1:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('1', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_2:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('2', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_3:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('3', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_4:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('4', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_5:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('5', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_6:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('6', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_7:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('7', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_8:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('8', cursorIndex);
                cursorIndex++;
                break;
            case ButtonKeyCode.NUM_9:
                if (cursorIndex >= 4)
                    return;
                radio.displayControllerBarrett2090.SetChannelNumber('9', cursorIndex);
                cursorIndex++;
                break;
            //Number Pad button setting End 
            case ButtonKeyCode.CANCEL:
                cursorIndex--;
                if (cursorIndex < 0)
                {
                    cursorIndex = 0;
                    radio.Navigator.Pop();
                    radio.RefreshHomeScreen();
                }
                else
                {
                    radio.displayControllerBarrett2090.SetChannelNumber('-', cursorIndex);
                }

                break;
            //channel switching
            case ButtonKeyCode.CHANNEL:
                string channelText = radio.displayControllerBarrett2090.ChannelNumber.text;
                string formattedText = "";
                // validation for channel setup
                if (channelText.Equals("----") || channelText.Equals("0---") || channelText.Equals("00--") || channelText.Equals("000-") || channelText.Equals("0000"))
                    return;

                int countDas = channelText.Count(c => c == '-');  // count total hyphen using LINQ
                for (int i = 0; i < channelText.Length; i++)
                {
                    if (i < countDas)
                        formattedText += 0; // add zero -- hyphen
                    else
                        formattedText += channelText.ToCharArray()[i - countDas]; //set 0 in first and other string in last
                }
                for (int i = 0; i < radio.channelList.Count; i++) // checking channel list
                {
                    if (radio.channelList[i].channelNumber == channelText || radio.channelList[i].channelNumber == formattedText)
                    {
                        radio.activeChannel = radio.channelList[i];  // set channel in an object 
                        radio.activeChannelIndex = i;
                        radio.displayControllerBarrett2090.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.channelList[i].channelNumber.ToString();
                        radio.displayControllerBarrett2090.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel";
                        radio.RefreshHomeScreen();
                        radio.displayControllerBarrett2090.LeftUpConrer.gameObject.SetActive(true);
                        radio.Navigator.Pop();
                        return;
                    }
                }
                radio.channelList.Add(new Channel(formattedText, 000000.000, 000000.000, "Undefinite", Modulation.AM, ChannelPowerMode.LOW, CallFormat.INTERNATIONAL));  // Manually channel set
                radio.activeChannelIndex = radio.channelList.Count - 1;
                radio.activeChannel = radio.channelList[radio.activeChannelIndex];  // set channel in an object
                radio.displayControllerBarrett2090.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.channelList[radio.channelList.Count - 1].channelNumber.ToString();
                radio.displayControllerBarrett2090.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel";
                //radio.displayControllerBarrett2090.ChannelInputPanel.SetActive(false);
                radio.RefreshHomeScreen();
                radio.displayControllerBarrett2090.LeftUpConrer.gameObject.SetActive(true);
                radio.Navigator.Pop();
                break;
        }
    }

    //Active Channel changing Increment and decrement
    public static void ChannelUp(Barrett2090 radio)
    {
        radio.activeChannelIndex++;
        if (radio.activeChannelIndex >= radio.channelList.Count) // check active channel in channelList
        {
            radio.activeChannelIndex = 0;
        }
        radio.activeChannel = radio.channelList[radio.activeChannelIndex];
        Logger.Log(radio, radio.activeChannel.channelNumber);
        radio.displayControllerBarrett2090.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.activeChannel.channelNumber.ToString();
        radio.displayControllerBarrett2090.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel";
        radio.RefreshHomeScreen();

    }
    public static void ChannelDown(Barrett2090 radio)
    {
        radio.activeChannelIndex--;
        if (radio.activeChannelIndex < 0)
        {
            radio.activeChannelIndex = radio.channelList.Count - 1;
        }
        radio.activeChannel = radio.channelList[radio.activeChannelIndex];
        Logger.Log(radio, radio.activeChannel.channelNumber);
        radio.displayControllerBarrett2090.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.activeChannel.channelNumber.ToString();
        radio.displayControllerBarrett2090.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel";
        radio.RefreshHomeScreen();
    }
    //End Active Channel changing Increment and decrement
    public static void RxSetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        if (!isRxEditable && buttonID == ButtonKeyCode.NUM_0 || buttonID == ButtonKeyCode.NUM_1 || buttonID == ButtonKeyCode.NUM_2 || buttonID == ButtonKeyCode.NUM_3 || buttonID == ButtonKeyCode.NUM_4 || buttonID == ButtonKeyCode.NUM_5 || buttonID == ButtonKeyCode.NUM_6 || buttonID == ButtonKeyCode.NUM_7 || buttonID == ButtonKeyCode.NUM_8 || buttonID == ButtonKeyCode.NUM_9)
        {
            NumberPadSetting.NumberPader(radio, buttonID);
            counter_prog = 0;
            isNumberPadActive = true;
        }

        switch (buttonID)
        {
            case ButtonKeyCode.PROG:
                if (counter_prog == 0 && isNumberPadActive)
                {
                    string RxText = radio.displayControllerBarrett2090.RxNumber.text;
                    var splitted = RxText.Split('.');  // spilt the number by dot
                    string formattedText = "";
                    int countDas = splitted[0].Count(c => c == '-'); // count total hyphen using LINQ
                    for (int i = 0; i < splitted[0].Length; i++)
                    {
                        if (i < countDas)
                            formattedText += 0; // add zero -- hyphen
                        else
                            formattedText += splitted[0].ToCharArray()[i - countDas]; //set 0 in first and other string in last

                    }

                    char[] afterDot = splitted[1].ToCharArray(); // value after dot
                    for (int i = 0; i < afterDot.Length; i++)
                    {
                        if (afterDot[i] == '-')
                        {
                            afterDot[i] = '0'; // replace ( - ) by 0
                        }
                    }
                    //print(formattedText + "." + new string(afterDot));
                    double InputRxNumber = Convert.ToDouble((formattedText + "." + new string(afterDot)));
                    if (InputRxNumber <= 250.000)  // Min 250 KHz
                    {
                        string lowRxValue = radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "00250.000";
                        string active = String.Format("{0:00000.000}", lowRxValue);
                        radio.activeChannel.rx = Convert.ToDouble(lowRxValue); // set Rx Min value
                    }
                    else if (InputRxNumber >= 30000.000) // Max 30000 KHz
                    {
                        string MaxRxValue = radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "30000.000";
                        radio.activeChannel.rx = Convert.ToDouble(MaxRxValue); // set Rx Max value 
                    }
                    else
                    {
                        radio.activeChannel.rx = Convert.ToDouble(InputRxNumber);
                        string active = String.Format("{0:00000.000}", radio.activeChannel.rx);
                        radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active;
                    }
                    NumberPadSetting.cursorIndex = 0; // number pad cursor index set zero
                    counter_prog = 1;
                    isNumberPadActive = false; //number pad active disable
                }
                else
                {
                    // set Rx in an object 
                    // End Rx setting
                    // display Tx
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "";
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<CursorController>().CursorIndex = 1;
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("CH").GetComponent<Text>().text = "Tx";
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Header").GetComponent<Text>().text = "Tx Frequency";
                    string activeTx = String.Format("{0:00000.000}", radio.activeChannel.tx); // tx formater
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = activeTx;
                    double TxIndex = Convert.ToDouble(radio.displayControllerBarrett2090.RxNumber.text.ToString());
                    radio.activeChannel.tx = TxIndex;  // set tx in a active channel list
                    radio.Navigator.Pop();
                    radio.Navigator.Push("TX");
                    counter_prog = 0;
                }
                return;
            case ButtonKeyCode.CANCEL:
                if (!isRxEditable && isNumberPadActive)
                {
                    NumberPadSetting.cursorIndex--;
                    if (NumberPadSetting.cursorIndex < 0)
                    {
                        string active = String.Format("{0:00000.000}", radio.activeChannel.rx);
                        radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active;
                        NumberPadSetting.cursorIndex = 0;
                        isNumberPadActive = false;
                    }
                    else if (NumberPadSetting.cursorIndex == 5) // ignore dot 
                    {
                        NumberPadSetting.cursorIndex--;
                        radio.displayControllerBarrett2090.SetRxNumber('-', NumberPadSetting.cursorIndex);
                    }
                    else
                    {
                        radio.displayControllerBarrett2090.SetRxNumber('-', NumberPadSetting.cursorIndex);
                    }
                }
                else
                {
                    // return home panel
                    radio.displayControllerBarrett2090.FrequencyInputPanel.SetActive(false);
                    NumberPadSetting.cursorIndex = 0;
                    radio.Navigator.Pop();
                    radio.RefreshHomeScreen(); // refresh home screen
                }
                break;
        }
    }
    public static void TxSetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        if (!isRxEditable && buttonID == ButtonKeyCode.NUM_0 || buttonID == ButtonKeyCode.NUM_1 || buttonID == ButtonKeyCode.NUM_2 || buttonID == ButtonKeyCode.NUM_3 || buttonID == ButtonKeyCode.NUM_4 || buttonID == ButtonKeyCode.NUM_5 || buttonID == ButtonKeyCode.NUM_6 || buttonID == ButtonKeyCode.NUM_7 || buttonID == ButtonKeyCode.NUM_8 || buttonID == ButtonKeyCode.NUM_9)
        {
            NumberPadSetting.NumberPader(radio, buttonID);
            counter_prog_Tx = 0;
            isNumberPadActive = true;
        }
        switch (buttonID)
        {
            case ButtonKeyCode.PROG:
                if (counter_prog_Tx == 0 && isNumberPadActive)
                {
                    string TxText = radio.displayControllerBarrett2090.RxNumber.text;
                    var splitted = TxText.Split('.');
                    string formattedText = "";
                    int countDas = splitted[0].Count(c => c == '-');  // count total hyphen using LINQ
                    for (int i = 0; i < splitted[0].Length; i++)
                    {
                        if (i < countDas)
                            formattedText += 0; // add zero -- hyphen
                        else
                            formattedText += splitted[0].ToCharArray()[i - countDas]; //set 0 in first and other string in last
                    }

                    char[] afterDot = splitted[1].ToCharArray();   // value after dot
                    for (int i = 0; i < afterDot.Length; i++)
                    {
                        if (afterDot[i] == '-')
                        {
                            afterDot[i] = '0';     // replace ( - ) by 0
                        }
                    }
                    //print(formattedText + "." + new string(afterDot));
                    double InputTxNumber = Convert.ToDouble((formattedText + "." + new string(afterDot)));
                    if (InputTxNumber <= 1600.000)  // Min 1600.000 KHz
                    {
                        string lowTxValue = radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "01600.000";
                        string active = String.Format("{0:00000.000}", lowTxValue);
                        radio.activeChannel.tx = Convert.ToDouble(lowTxValue);
                    }
                    else if (InputTxNumber >= 30000.000) // Max 30000.000 KHz
                    {
                        string MaxTxValue = radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "30000.000";
                        radio.activeChannel.tx = Convert.ToDouble(MaxTxValue);
                    }
                    else
                    {
                        radio.activeChannel.tx = Convert.ToDouble(InputTxNumber);
                        string active = String.Format("{0:00000.000}", radio.activeChannel.tx);
                        radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active;
                    }
                    NumberPadSetting.cursorIndex = 0; // number pad cursor index set zero
                    counter_prog_Tx = 1;
                    isNumberPadActive = false;
                }
                else
                {
                    // Rx and Tx number panel disable
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("CH").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Unit").gameObject.SetActive(false);
                    cursorIndex = 0;
                    //Load Channel label 
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").gameObject.SetActive(true);
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Header").GetComponent<Text>().text = "Label";
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                    radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.activeChannel.channelLabel.ToString();
                    radio.Navigator.Pop();
                    radio.Navigator.Push("CHANNEL_LABEL");
                    counter_prog_Tx = 0;
                }
                break;
            case ButtonKeyCode.CANCEL:
                if (!isRxEditable && isNumberPadActive)
                {
                    NumberPadSetting.cursorIndex--;
                    if (NumberPadSetting.cursorIndex < 0)
                    {
                        string active = String.Format("{0:00000.000}", radio.activeChannel.tx);
                        radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active;
                        NumberPadSetting.cursorIndex = 0;
                        isNumberPadActive = false;
                    }
                    else if (NumberPadSetting.cursorIndex == 5) // ignore dot 
                    {
                        NumberPadSetting.cursorIndex--;
                        radio.displayControllerBarrett2090.SetRxNumber('-', NumberPadSetting.cursorIndex);
                    }
                    else
                    {
                        radio.displayControllerBarrett2090.SetRxNumber('-', NumberPadSetting.cursorIndex);
                    }
                }
                else
                {
                    // return home panel
                    radio.displayControllerBarrett2090.FrequencyInputPanel.SetActive(false);
                    NumberPadSetting.cursorIndex = 0;
                    radio.Navigator.Pop();
                    radio.RefreshHomeScreen(); // refresh homepanel
                }
                break;

        }
    }
    public static void ChannelLabelSetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {

        //NumberPadSetting.NumberPad(radio, buttonID);
        switch (buttonID)
        {
            case ButtonKeyCode.UP_ARROW:
                radio.channelLabelIndex++;
                if (radio.channelLabelIndex >= radio.channelLabelList.Count)
                {
                    radio.channelLabelIndex = 0;
                }
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.channelLabelList[radio.channelLabelIndex].ToString();
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.channelLabelIndex--;
                if (radio.channelLabelIndex < 0)
                {
                    radio.channelLabelIndex = radio.channelLabelList.Count - 1;
                }
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.channelLabelList[radio.channelLabelIndex].ToString();
                break;
            case ButtonKeyCode.PROG:
                radio.activeChannel.channelLabel = radio.channelLabelList[radio.channelLabelIndex];
                //Load Modulation
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.activeChannel.modulation.ToString();
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Header").GetComponent<Text>().text = "Modulation";
                radio.Navigator.Pop();
                radio.Navigator.Push("MODULATION");
                break;
            case ButtonKeyCode.CANCEL:
                radio.displayControllerBarrett2090.FrequencyInputPanel.SetActive(false);
                radio.Navigator.Pop();
                radio.RefreshHomeScreen();
                break;
        }
    }
    public static void ModulationSetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        switch (buttonID)
        {
            case ButtonKeyCode.UP_ARROW:
                radio.modulationIndex++;
                if (radio.modulationIndex >= BarrettMenu.ModulationList.Length)
                {
                    radio.modulationIndex = 0;
                }
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = BarrettMenu.ModulationList[radio.modulationIndex].ToString();
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.modulationIndex--;
                if (radio.modulationIndex < 0)
                {
                    radio.modulationIndex = BarrettMenu.ModulationList.Length - 1;
                }
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = BarrettMenu.ModulationList[radio.modulationIndex].ToString();
                break;
            case ButtonKeyCode.PROG:
                radio.activeChannel.modulation = BarrettMenu.ModulationList[radio.modulationIndex];
                // Load Channel Power Mode
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.activeChannel.power.ToString();
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Header").GetComponent<Text>().text = "Power";
                radio.Navigator.Pop();
                radio.Navigator.Push("CHANNEL_POWER_MODE");
                break;
            case ButtonKeyCode.CANCEL:
                radio.displayControllerBarrett2090.FrequencyInputPanel.SetActive(false);
                radio.Navigator.Pop();
                radio.RefreshHomeScreen();
                break;
        }
    }
    public static void ChannelPowerSetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        switch (buttonID)
        {
            case ButtonKeyCode.UP_ARROW:
                radio.powerIndex++;
                if (radio.powerIndex >= BarrettMenu.ChannelPowerModeList.Length)
                {
                    radio.powerIndex = 0;
                }
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = BarrettMenu.ChannelPowerModeList[radio.powerIndex].ToString();
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.powerIndex--;
                if (radio.powerIndex < 0)
                {
                    radio.powerIndex = BarrettMenu.ChannelPowerModeList.Length - 1;
                }
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = BarrettMenu.ChannelPowerModeList[radio.powerIndex].ToString();
                break;
            case ButtonKeyCode.PROG:
                radio.activeChannel.power = BarrettMenu.ChannelPowerModeList[radio.powerIndex];
                // Load Call Format
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.activeChannel.callFormat.ToString();
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Header").GetComponent<Text>().text = "Selcall";
                radio.Navigator.Pop();
                radio.Navigator.Push("CALL_FORMAT");
                break;
            case ButtonKeyCode.CANCEL:
                radio.displayControllerBarrett2090.FrequencyInputPanel.SetActive(false);
                radio.Navigator.Pop();
                radio.RefreshHomeScreen();
                break;
        }

    }
    public static void CallFormatSetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        switch (buttonID)
        {
            case ButtonKeyCode.UP_ARROW:
                radio.callFormateIndex++;
                if (radio.callFormateIndex >= BarrettMenu.CallFormatList.Length)
                {
                    radio.callFormateIndex = 0;
                }
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = BarrettMenu.CallFormatList[radio.callFormateIndex].ToString();
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.callFormateIndex--;
                if (radio.callFormateIndex < 0)
                {
                    radio.callFormateIndex = BarrettMenu.CallFormatList.Length - 1;
                }
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = BarrettMenu.CallFormatList[radio.callFormateIndex].ToString();
                break;
            case ButtonKeyCode.PROG:
                radio.activeChannel.callFormat = BarrettMenu.CallFormatList[radio.callFormateIndex];
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").gameObject.SetActive(false);
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Header").gameObject.SetActive(false);
                radio.displayControllerBarrett2090.FrequencyInputPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                radio.displayControllerBarrett2090.FrequencyInputPanel.SetActive(false);
                radio.Navigator.Pop();
                Logger.Log(radio, radio.Navigator.Peek().ToString());
                radio.RefreshHomeScreen();
                break;
            case ButtonKeyCode.CANCEL:
                radio.displayControllerBarrett2090.FrequencyInputPanel.SetActive(false);
                radio.Navigator.Pop();
                radio.RefreshHomeScreen();
                break;

        }
    }
    public static void ReceiverTuneSetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        CursorController CursorController = radio.displayControllerBarrett2090.ReceiverTunePanel.transform.Find("EntryField").GetComponent<CursorController>();//reference  for cursor
        switch (buttonID)
        {
            case ButtonKeyCode.NUM_3:
                CursorController.CursorIndex++;
                cursorIndex++;
                if (CursorController.CursorIndex > 9 && cursorIndex > 9)
                {
                    CursorController.CursorIndex = 9;
                    cursorIndex = 9;
                }
                //if (CursorController.CursorIndex == 5)
                //{
                //    CursorController.CursorIndex++;
                //}
                break;
            case ButtonKeyCode.NUM_1:

                CursorController.CursorIndex--;
                cursorIndex--;
                if (CursorController.CursorIndex < 0 && cursorIndex < 0)
                {
                    CursorController.CursorIndex = 0;
                    cursorIndex = 0;
                }
                //if (CursorController.CursorIndex == 5)
                //{
                //    CursorController.CursorIndex--;   
                //}
                break;
            case ButtonKeyCode.UP_ARROW:
                Logger.Log("Cursor Index : " + CursorController.CursorIndex);
                string stringFrequency = radio.displayControllerBarrett2090.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text;
                float numFrequency = float.Parse(stringFrequency);
                if (cursorIndex == 0)
                {
                    numFrequency = numFrequency + 10000;
                }
                else if (cursorIndex == 1)
                {
                    numFrequency = numFrequency + 1000;
                }
                else if (cursorIndex == 2)
                {
                    numFrequency = numFrequency + 100;
                }
                else if (cursorIndex == 3)
                {
                    numFrequency = numFrequency + 10;
                }
                else if (cursorIndex == 4)
                {
                    numFrequency = numFrequency + 1;
                }
                else if (cursorIndex == 5)
                {
                    numFrequency = numFrequency + 0.1f;
                }
                else if (cursorIndex == 6)
                {
                    numFrequency = numFrequency + 0.01f;
                }
                else if (cursorIndex == 7)
                {
                    numFrequency = numFrequency + 0.001f;
                }
                //else if (cursorIndex == 8)
                //{
                //    numFrequency = numFrequency + 0.001f;
                //}
                stringFrequency = string.Format("{0:00000.000}", numFrequency);
                radio.displayControllerBarrett2090.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text = stringFrequency;
                break;

            case ButtonKeyCode.DOWN_ARROW:
                Logger.Log("Cursor Index : " + CursorController.CursorIndex);
                string stringFrequencyD = radio.displayControllerBarrett2090.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text;
                float numFrequencyD = float.Parse(stringFrequencyD);
                if (cursorIndex == 0)
                {
                    numFrequencyD = numFrequencyD - 10000;
                }
                else if (cursorIndex == 1)
                {
                    numFrequencyD = numFrequencyD - 1000;
                }
                else if (cursorIndex == 2)
                {
                    numFrequencyD = numFrequencyD - 100;
                }
                else if (cursorIndex == 3)
                {
                    numFrequencyD = numFrequencyD - 10;
                }
                else if (cursorIndex == 4)
                {
                    numFrequencyD = numFrequencyD - 1;
                }
                else if (cursorIndex == 5)
                {
                    numFrequencyD = numFrequencyD - 0.1f;
                }
                else if (cursorIndex == 6)
                {
                    numFrequencyD = numFrequencyD - 0.01f;
                }
                else if (cursorIndex == 7)
                {
                    numFrequencyD = numFrequencyD - 0.001f;
                }
                //else if (cursorIndex == 8)
                //{
                //    numFrequencyD = numFrequencyD - 0.001f;
                //}
                stringFrequencyD = string.Format("{0:00000.000}", numFrequencyD);
                radio.displayControllerBarrett2090.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text = stringFrequencyD;

                break;
            case ButtonKeyCode.CANCEL:
                radio.displayControllerBarrett2090.ReceiverTunePanel.SetActive(false);
                radio.displayControllerBarrett2090.HomePanel.transform.Find("InfoPanel").gameObject.SetActive(true);
                radio.displayControllerBarrett2090.HomePanel.transform.Find("GPSPanel").gameObject.SetActive(true);
                radio.Navigator.Pop();
                radio.RefreshHomeScreen();
                break;

        }
    }

    /*======================
    Barrett2090 Settings End
    ======================*/
    /*======================
    Barrett2050 Settings Start
    ======================*/
    //public static void ChannelSwitching(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    // NumberPadSetting.NumberPad(radio, buttonID);
    //    switch (buttonID)
    //    {
    //        //Number pad Button setup 
    //        case ButtonKeyCode.NUM_0:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('0', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_1:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('1', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_2:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('2', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_3:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('3', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_4:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('4', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_5:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('5', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_6:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('6', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_7:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('7', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_8:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('8', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        case ButtonKeyCode.NUM_9:
    //            if (cursorIndex >= 4)
    //                return;
    //            radio.displayControllerBarrett2050.SetChannelNumber('9', cursorIndex);
    //            cursorIndex++;
    //            break;
    //        //Number Pad button setting End 
    //        case ButtonKeyCode.CANCEL:
    //            cursorIndex--;
    //            if (cursorIndex < 0)
    //            {
    //                cursorIndex = 0;
    //                radio.Navigator.Pop();
    //                radio.RefreshHomeScreen();
    //            }
    //            else
    //            {
    //                radio.displayControllerBarrett2050.SetChannelNumber('-', cursorIndex);
    //            }

    //            break;
    //        //channel switching
    //        case ButtonKeyCode.CHANNEL:
    //            string channelText = radio.displayControllerBarrett2050.ChannelNumber.text;
    //            string formattedText = "";
    //            // validation for channel setup
    //            if (channelText.Equals("----") || channelText.Equals("0---") || channelText.Equals("00--") || channelText.Equals("000-") || channelText.Equals("0000"))
    //                return;

    //            int countDas = channelText.Count(c => c == '-');  // count total hyphen using LINQ
    //            for (int i = 0; i < channelText.Length; i++)
    //            {
    //                if (i < countDas)
    //                    formattedText += 0; // add zero -- hyphen
    //                else
    //                    formattedText += channelText.ToCharArray()[i - countDas]; //set 0 in first and other string in last
    //            }
    //            for (int i = 0; i < radio.channelList.Count; i++) // checking channel list
    //            {
    //                if (radio.channelList[i].channelNumber == channelText || radio.channelList[i].channelNumber == formattedText)
    //                {
    //                    radio.activeChannel = radio.channelList[i];  // set channel in an object 
    //                    radio.activeChannelIndex = i;
    //                    radio.displayControllerBarrett2050.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.channelList[i].channelNumber.ToString();
    //                    radio.displayControllerBarrett2050.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel";
    //                    radio.RefreshHomeScreen();
    //                    radio.displayControllerBarrett2050.LeftUpConrer.gameObject.SetActive(true);
    //                    radio.Navigator.Pop();
    //                    return;
    //                }
    //            }
    //            radio.channelList.Add(new Channel(formattedText, 000000.000, 000000.000, "Undefinite", Modulation.AM, ChannelPowerMode.LOW, CallFormat.INTERNATIONAL));  // Manually channel set
    //            radio.activeChannelIndex = radio.channelList.Count - 1;
    //            radio.activeChannel = radio.channelList[radio.activeChannelIndex];  // set channel in an object
    //            radio.displayControllerBarrett2050.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.channelList[radio.channelList.Count - 1].channelNumber.ToString();
    //            radio.displayControllerBarrett2050.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel";
    //            //radio.displayControllerBarrett2050.ChannelInputPanel.SetActive(false);
    //            radio.RefreshHomeScreen();
    //            radio.displayControllerBarrett2050.LeftUpConrer.gameObject.SetActive(true);
    //            radio.Navigator.Pop();
    //            break;
    //    }
    //}

    ////Active Channel changing Increment and decrement
    //public static void ChannelUp(Barrett2050 radio)
    //{
    //    radio.activeChannelIndex++;
    //    if (radio.activeChannelIndex >= radio.channelList.Count) // check active channel in channelList
    //    {
    //        radio.activeChannelIndex = 0;
    //    }
    //    radio.activeChannel = radio.channelList[radio.activeChannelIndex];
    //    Logger.Log(radio, radio.activeChannel.channelNumber);
    //    radio.displayControllerBarrett2050.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.activeChannel.channelNumber.ToString();
    //    radio.displayControllerBarrett2050.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel";
    //    radio.RefreshHomeScreen();

    //}
    //public static void ChannelDown(Barrett2050 radio)
    //{
    //    radio.activeChannelIndex--;
    //    if (radio.activeChannelIndex < 0)
    //    {
    //        radio.activeChannelIndex = radio.channelList.Count - 1;
    //    }
    //    radio.activeChannel = radio.channelList[radio.activeChannelIndex];
    //    Logger.Log(radio, radio.activeChannel.channelNumber);
    //    radio.displayControllerBarrett2050.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.activeChannel.channelNumber.ToString();
    //    radio.displayControllerBarrett2050.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel";
    //    radio.RefreshHomeScreen();
    //}
    ////End Active Channel changing Increment and decrement
    //public static void RxSetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    if (!isRxEditable && buttonID == ButtonKeyCode.NUM_0 || buttonID == ButtonKeyCode.NUM_1 || buttonID == ButtonKeyCode.NUM_2 || buttonID == ButtonKeyCode.NUM_3 || buttonID == ButtonKeyCode.NUM_4 || buttonID == ButtonKeyCode.NUM_5 || buttonID == ButtonKeyCode.NUM_6 || buttonID == ButtonKeyCode.NUM_7 || buttonID == ButtonKeyCode.NUM_8 || buttonID == ButtonKeyCode.NUM_9)
    //    {
    //        NumberPadSetting.NumberPader(radio, buttonID);
    //        counter_prog = 0;
    //        isNumberPadActive = true;
    //    }

    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.PROG:
    //            if (counter_prog == 0 && isNumberPadActive)
    //            {
    //                string RxText = radio.displayControllerBarrett2050.RxNumber.text;
    //                var splitted = RxText.Split('.');  // spilt the number by dot
    //                string formattedText = "";
    //                int countDas = splitted[0].Count(c => c == '-'); // count total hyphen using LINQ
    //                for (int i = 0; i < splitted[0].Length; i++)
    //                {
    //                    if (i < countDas)
    //                        formattedText += 0; // add zero -- hyphen
    //                    else
    //                        formattedText += splitted[0].ToCharArray()[i - countDas]; //set 0 in first and other string in last

    //                }

    //                char[] afterDot = splitted[1].ToCharArray(); // value after dot
    //                for (int i = 0; i < afterDot.Length; i++)
    //                {
    //                    if (afterDot[i] == '-')
    //                    {
    //                        afterDot[i] = '0'; // replace ( - ) by 0
    //                    }
    //                }
    //                //print(formattedText + "." + new string(afterDot));
    //                double InputRxNumber = Convert.ToDouble((formattedText + "." + new string(afterDot)));
    //                if (InputRxNumber <= 250.000)  // Min 250 KHz
    //                {
    //                    string lowRxValue = radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "00250.000";
    //                    string active = String.Format("{0:00000.000}", lowRxValue);
    //                    radio.activeChannel.rx = Convert.ToDouble(lowRxValue); // set Rx Min value
    //                }
    //                else if (InputRxNumber >= 30000.000) // Max 30000 KHz
    //                {
    //                    string MaxRxValue = radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "30000.000";
    //                    radio.activeChannel.rx = Convert.ToDouble(MaxRxValue); // set Rx Max value 
    //                }
    //                else
    //                {
    //                    radio.activeChannel.rx = Convert.ToDouble(InputRxNumber);
    //                    string active = String.Format("{0:00000.000}", radio.activeChannel.rx);
    //                    radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active;
    //                }
    //                NumberPadSetting.cursorIndex = 0; // number pad cursor index set zero
    //                counter_prog = 1;
    //                isNumberPadActive = false; //number pad active disable
    //            }
    //            else
    //            {
    //                // set Rx in an object 
    //                // End Rx setting
    //                // display Tx
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "";
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<CursorController>().CursorIndex = 1;
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("CH").GetComponent<Text>().text = "Tx";
    //                string activeTx = String.Format("{0:00000.000}", radio.activeChannel.tx); // tx formater
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = activeTx;
    //                double TxIndex = Convert.ToDouble(radio.displayControllerBarrett2050.RxNumber.text.ToString());
    //                radio.activeChannel.tx = TxIndex;  // set tx in a active channel list
    //                radio.Navigator.Pop();
    //                radio.Navigator.Push("TX");
    //                counter_prog = 0;
    //            }
    //            return;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isRxEditable && isNumberPadActive)
    //            {
    //                NumberPadSetting.cursorIndex--;
    //                if (NumberPadSetting.cursorIndex < 0)
    //                {
    //                    string active = String.Format("{0:00000.000}", radio.activeChannel.rx);
    //                    radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active;
    //                    NumberPadSetting.cursorIndex = 0;
    //                    isNumberPadActive = false;
    //                }
    //                else if (NumberPadSetting.cursorIndex == 5) // ignore dot 
    //                {
    //                    NumberPadSetting.cursorIndex--;
    //                    radio.displayControllerBarrett2050.SetRxNumber('-', NumberPadSetting.cursorIndex);
    //                }
    //                else
    //                {
    //                    radio.displayControllerBarrett2050.SetRxNumber('-', NumberPadSetting.cursorIndex);
    //                }
    //            }
    //            else
    //            {
    //                // return home panel
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.SetActive(false);
    //                NumberPadSetting.cursorIndex = 0;
    //                radio.Navigator.Pop();
    //                radio.RefreshHomeScreen(); // refresh home screen
    //            }
    //            break;
    //    }
    //}
    //public static void TxSetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    if (!isRxEditable && buttonID == ButtonKeyCode.NUM_0 || buttonID == ButtonKeyCode.NUM_1 || buttonID == ButtonKeyCode.NUM_2 || buttonID == ButtonKeyCode.NUM_3 || buttonID == ButtonKeyCode.NUM_4 || buttonID == ButtonKeyCode.NUM_5 || buttonID == ButtonKeyCode.NUM_6 || buttonID == ButtonKeyCode.NUM_7 || buttonID == ButtonKeyCode.NUM_8 || buttonID == ButtonKeyCode.NUM_9)
    //    {
    //        NumberPadSetting.NumberPader(radio, buttonID);
    //        counter_prog_Tx = 0;
    //        isNumberPadActive = true;
    //    }
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.PROG:
    //            if (counter_prog_Tx == 0 && isNumberPadActive)
    //            {
    //                string TxText = radio.displayControllerBarrett2050.RxNumber.text;
    //                var splitted = TxText.Split('.');
    //                string formattedText = "";
    //                int countDas = splitted[0].Count(c => c == '-');  // count total hyphen using LINQ
    //                for (int i = 0; i < splitted[0].Length; i++)
    //                {
    //                    if (i < countDas)
    //                        formattedText += 0; // add zero -- hyphen
    //                    else
    //                        formattedText += splitted[0].ToCharArray()[i - countDas]; //set 0 in first and other string in last
    //                }

    //                char[] afterDot = splitted[1].ToCharArray();   // value after dot
    //                for (int i = 0; i < afterDot.Length; i++)
    //                {
    //                    if (afterDot[i] == '-')
    //                    {
    //                        afterDot[i] = '0';     // replace ( - ) by 0
    //                    }
    //                }
    //                //print(formattedText + "." + new string(afterDot));
    //                double InputTxNumber = Convert.ToDouble((formattedText + "." + new string(afterDot)));
    //                if (InputTxNumber <= 1600.000)  // Min 1600.000 KHz
    //                {
    //                    string lowTxValue = radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "01600.000";
    //                    string active = String.Format("{0:00000.000}", lowTxValue);
    //                    radio.activeChannel.tx = Convert.ToDouble(lowTxValue);
    //                }
    //                else if (InputTxNumber >= 30000.000) // Max 30000.000 KHz
    //                {
    //                    string MaxTxValue = radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = "30000.000";
    //                    radio.activeChannel.tx = Convert.ToDouble(MaxTxValue);
    //                }
    //                else
    //                {
    //                    radio.activeChannel.tx = Convert.ToDouble(InputTxNumber);
    //                    string active = String.Format("{0:00000.000}", radio.activeChannel.tx);
    //                    radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active;
    //                }
    //                NumberPadSetting.cursorIndex = 0; // number pad cursor index set zero
    //                counter_prog_Tx = 1;
    //                isNumberPadActive = false;
    //            }
    //            else
    //            {
    //                // Rx and Tx number panel disable
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("CH").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Unit").gameObject.SetActive(false);
    //                cursorIndex = 0;
    //                //Load Channel label 
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").gameObject.SetActive(true);
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.activeChannel.channelLabel.ToString();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Push("CHANNEL_LABEL");
    //                counter_prog_Tx = 0;
    //            }
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isRxEditable && isNumberPadActive)
    //            {
    //                NumberPadSetting.cursorIndex--;
    //                if (NumberPadSetting.cursorIndex < 0)
    //                {
    //                    string active = String.Format("{0:00000.000}", radio.activeChannel.tx);
    //                    radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active;
    //                    NumberPadSetting.cursorIndex = 0;
    //                    isNumberPadActive = false;
    //                }
    //                else if (NumberPadSetting.cursorIndex == 5) // ignore dot 
    //                {
    //                    NumberPadSetting.cursorIndex--;
    //                    radio.displayControllerBarrett2050.SetRxNumber('-', NumberPadSetting.cursorIndex);
    //                }
    //                else
    //                {
    //                    radio.displayControllerBarrett2050.SetRxNumber('-', NumberPadSetting.cursorIndex);
    //                }
    //            }
    //            else
    //            {
    //                // return home panel
    //                radio.displayControllerBarrett2050.FrequencyInputPanel.SetActive(false);
    //                NumberPadSetting.cursorIndex = 0;
    //                radio.Navigator.Pop();
    //                radio.RefreshHomeScreen(); // refresh homepanel
    //            }
    //            break;

    //    }
    //}
    //public static void ChannelLabelSetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{

    //    //NumberPadSetting.NumberPad(radio, buttonID);
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            radio.channelLabelIndex++;
    //            if (radio.channelLabelIndex >= radio.channelLabelList.Count)
    //            {
    //                radio.channelLabelIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.channelLabelList[radio.channelLabelIndex].ToString();
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.channelLabelIndex--;
    //            if (radio.channelLabelIndex < 0)
    //            {
    //                radio.channelLabelIndex = radio.channelLabelList.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.channelLabelList[radio.channelLabelIndex].ToString();
    //            break;
    //        case ButtonKeyCode.PROG:
    //            radio.activeChannel.channelLabel = radio.channelLabelList[radio.channelLabelIndex];
    //            //Load Modulation
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.activeChannel.modulation.ToString();
    //            radio.Navigator.Pop();
    //            radio.Navigator.Push("MODULATION");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.SetActive(false);
    //            radio.Navigator.Pop();
    //            radio.RefreshHomeScreen();
    //            break;
    //    }
    //}
    //public static void ModulationSetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            radio.modulationIndex++;
    //            if (radio.modulationIndex >= Barrett2050Menu.ModulationList.Length)
    //            {
    //                radio.modulationIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = Barrett2050Menu.ModulationList[radio.modulationIndex].ToString();
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.modulationIndex--;
    //            if (radio.modulationIndex < 0)
    //            {
    //                radio.modulationIndex = Barrett2050Menu.ModulationList.Length - 1;
    //            }
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = Barrett2050Menu.ModulationList[radio.modulationIndex].ToString();
    //            break;
    //        case ButtonKeyCode.PROG:
    //            radio.activeChannel.modulation = Barrett2050Menu.ModulationList[radio.modulationIndex];
    //            // Load Channel Power Mode
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.activeChannel.power.ToString();
    //            radio.Navigator.Pop();
    //            radio.Navigator.Push("CHANNEL_POWER_MODE");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.SetActive(false);
    //            radio.Navigator.Pop();
    //            radio.RefreshHomeScreen();
    //            break;
    //    }
    //}
    //public static void ChannelPowerSetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            radio.powerIndex++;
    //            if (radio.powerIndex >= Barrett2050Menu.ChannelPowerModeList.Length)
    //            {
    //                radio.powerIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = Barrett2050Menu.ChannelPowerModeList[radio.powerIndex].ToString();
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.powerIndex--;
    //            if (radio.powerIndex < 0)
    //            {
    //                radio.powerIndex = Barrett2050Menu.ChannelPowerModeList.Length - 1;
    //            }
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = Barrett2050Menu.ChannelPowerModeList[radio.powerIndex].ToString();
    //            break;
    //        case ButtonKeyCode.PROG:
    //            radio.activeChannel.power = Barrett2050Menu.ChannelPowerModeList[radio.powerIndex];
    //            // Load Call Format
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = radio.activeChannel.callFormat.ToString();
    //            radio.Navigator.Pop();
    //            radio.Navigator.Push("CALL_FORMAT");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.SetActive(false);
    //            radio.Navigator.Pop();
    //            radio.RefreshHomeScreen();
    //            break;
    //    }

    //}
    //public static void CallFormatSetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            radio.callFormateIndex++;
    //            if (radio.callFormateIndex >= Barrett2050Menu.CallFormatList.Length)
    //            {
    //                radio.callFormateIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = Barrett2050Menu.CallFormatList[radio.callFormateIndex].ToString();
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.callFormateIndex--;
    //            if (radio.callFormateIndex < 0)
    //            {
    //                radio.callFormateIndex = Barrett2050Menu.CallFormatList.Length - 1;
    //            }
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").GetComponent<Text>().text = Barrett2050Menu.CallFormatList[radio.callFormateIndex].ToString();
    //            break;
    //        case ButtonKeyCode.PROG:
    //            radio.activeChannel.callFormat = Barrett2050Menu.CallFormatList[radio.callFormateIndex];
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.transform.Find("Label").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.SetActive(false);
    //            radio.Navigator.Pop();
    //            Logger.Log(radio, radio.Navigator.Peek().ToString());
    //            radio.RefreshHomeScreen();
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.displayControllerBarrett2050.FrequencyInputPanel.SetActive(false);
    //            radio.Navigator.Pop();
    //            radio.RefreshHomeScreen();
    //            break;

    //    }
    //}
    //public static void ReceiverTuneSetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    CursorController CursorController = radio.displayControllerBarrett2050.ReceiverTunePanel.transform.Find("EntryField").GetComponent<CursorController>();//reference  for cursor
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_3:
    //            CursorController.CursorIndex++;
    //            cursorIndex++;
    //            if (CursorController.CursorIndex > 9 && cursorIndex > 9)
    //            {
    //                CursorController.CursorIndex = 9;
    //                cursorIndex = 9;
    //            }
    //            //if (CursorController.CursorIndex == 5)
    //            //{
    //            //    CursorController.CursorIndex++;
    //            //}
    //            break;
    //        case ButtonKeyCode.NUM_1:

    //            CursorController.CursorIndex--;
    //            cursorIndex--;
    //            if (CursorController.CursorIndex < 0 && cursorIndex < 0)
    //            {
    //                CursorController.CursorIndex = 0;
    //                cursorIndex = 0;
    //            }
    //            //if (CursorController.CursorIndex == 5)
    //            //{
    //            //    CursorController.CursorIndex--;   
    //            //}
    //            break;
    //        case ButtonKeyCode.UP_ARROW:
    //            Logger.Log("Cursor Index : " + CursorController.CursorIndex);
    //            string stringFrequency = radio.displayControllerBarrett2050.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text;
    //            float numFrequency = float.Parse(stringFrequency);
    //            if (cursorIndex == 0)
    //            {
    //                numFrequency = numFrequency + 10000;
    //            }
    //            else if (cursorIndex == 1)
    //            {
    //                numFrequency = numFrequency + 1000;
    //            }
    //            else if (cursorIndex == 2)
    //            {
    //                numFrequency = numFrequency + 100;
    //            }
    //            else if (cursorIndex == 3)
    //            {
    //                numFrequency = numFrequency + 10;
    //            }
    //            else if (cursorIndex == 4)
    //            {
    //                numFrequency = numFrequency + 1;
    //            }
    //            else if (cursorIndex == 5)
    //            {
    //                numFrequency = numFrequency + 0.1f;
    //            }
    //            else if (cursorIndex == 6)
    //            {
    //                numFrequency = numFrequency + 0.01f;
    //            }
    //            else if (cursorIndex == 7)
    //            {
    //                numFrequency = numFrequency + 0.001f;
    //            }
    //            //else if (cursorIndex == 8)
    //            //{
    //            //    numFrequency = numFrequency + 0.001f;
    //            //}
    //            stringFrequency = string.Format("{0:00000.000}", numFrequency);
    //            radio.displayControllerBarrett2050.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text = stringFrequency;
    //            break;

    //        case ButtonKeyCode.DOWN_ARROW:
    //            Logger.Log("Cursor Index : " + CursorController.CursorIndex);
    //            string stringFrequencyD = radio.displayControllerBarrett2050.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text;
    //            float numFrequencyD = float.Parse(stringFrequencyD);
    //            if (cursorIndex == 0)
    //            {
    //                numFrequencyD = numFrequencyD - 10000;
    //            }
    //            else if (cursorIndex == 1)
    //            {
    //                numFrequencyD = numFrequencyD - 1000;
    //            }
    //            else if (cursorIndex == 2)
    //            {
    //                numFrequencyD = numFrequencyD - 100;
    //            }
    //            else if (cursorIndex == 3)
    //            {
    //                numFrequencyD = numFrequencyD - 10;
    //            }
    //            else if (cursorIndex == 4)
    //            {
    //                numFrequencyD = numFrequencyD - 1;
    //            }
    //            else if (cursorIndex == 5)
    //            {
    //                numFrequencyD = numFrequencyD - 0.1f;
    //            }
    //            else if (cursorIndex == 6)
    //            {
    //                numFrequencyD = numFrequencyD - 0.01f;
    //            }
    //            else if (cursorIndex == 7)
    //            {
    //                numFrequencyD = numFrequencyD - 0.001f;
    //            }
    //            //else if (cursorIndex == 8)
    //            //{
    //            //    numFrequencyD = numFrequencyD - 0.001f;
    //            //}
    //            stringFrequencyD = string.Format("{0:00000.000}", numFrequencyD);
    //            radio.displayControllerBarrett2050.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text = stringFrequencyD;

    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.displayControllerBarrett2050.ReceiverTunePanel.SetActive(false);
    //            radio.displayControllerBarrett2050.HomePanel.transform.Find("InfoPanel").gameObject.SetActive(true);
    //            radio.displayControllerBarrett2050.HomePanel.transform.Find("GPSPanel").gameObject.SetActive(true);
    //            radio.Navigator.Pop();
    //            radio.RefreshHomeScreen();
    //            break;

    //    }
    //}

    /*======================
    Barrett2050 Settings End
    ======================*/
    /*======================
    QMAC90M Settings
    ======================*/

    //public static void ChannelUp(QMAC90M radio)
    //{
    //    radio.activeChannelIndex++;
    //    if (radio.activeChannelIndex >= radio.QmachannelList.Count) // check active channel in channelList
    //    {
    //        radio.activeChannelIndex = 0;
    //    }
    //    radio.activeChannel = radio.QmachannelList[radio.activeChannelIndex];
    //    radio.qMACDisplayController.channelInput.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.activeChannel.channelNumber.ToString();

    //}
    //public static void ChannelDown(QMAC90M radio)
    //{
    //    radio.activeChannelIndex--;
    //    if (radio.activeChannelIndex < 0)
    //    {
    //        radio.activeChannelIndex = radio.QmachannelList.Count - 1;
    //    }
    //    radio.activeChannel = radio.QmachannelList[radio.activeChannelIndex];
    //    radio.qMACDisplayController.channelInput.transform.Find("ChannelNumber").GetComponent<Text>().text = radio.activeChannel.channelNumber.ToString();
    //}
    //Set Selcall ID to database
    //public static void QMacSelCallSetup(QMAC90M radio, ButtonKeyCode buttonID)
    //{
    //    Text SelCallText = radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_HASH:
    //            // validation
    //            if (SelCallText.text.Equals("-----"))  // ignore ------ das
    //                return;
    //            int countDas = SelCallText.text.Count(c => c == '-');  // count total hyphen using LINQ
    //            if (countDas > 0)
    //            {
    //                radio.qMACDisplayController.SelcallPanel.gameObject.SetActive(true);
    //                radio.qMACDisplayController.Selcall.gameObject.SetActive(false);
    //                radio.qMACDisplayController.sellcallID.gameObject.SetActive(false);
    //            }
    //            if (cursorIndex == 4)
    //            {
    //                SelCallText.text = SelCallText.text.Remove(SelCallText.text.Length - 1);  // ignore last 1 das
    //            }
    //            else if (cursorIndex == 3)
    //            {
    //                SelCallText.text = SelCallText.text.Remove(SelCallText.text.Length - 2);   // ignore last 2 das
    //            }
    //            else if (cursorIndex == 2)
    //            {
    //                SelCallText.text = SelCallText.text.Remove(SelCallText.text.Length - 3);  // ignore last 3 das
    //            }
    //            else if (cursorIndex == 1)
    //            {
    //                SelCallText.text = SelCallText.text.Remove(SelCallText.text.Length - 4);   // ignore last 4 das
    //            }
    //            radio.activeChannel.SelCall = Convert.ToInt32(SelCallText.text);
    //            Logger.Log(radio.activeChannel.SelCall);
    //            radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>().text = "Save";
    //            SelCallText.StartCoroutine(DisplayText(1f, radio));
    //            radio.Navigator.Pop();
    //            cursorIndex = 0;
    //            break;
    //        default:
    //            char input = NumberPadSetting.KeyPadInput(false, buttonID);
    //            if (cursorIndex < SelCallText.text.Length - 1)
    //            {
    //                char[] temp = SelCallText.text.ToCharArray();
    //                temp[cursorIndex] = input;
    //                SelCallText.text = new string(temp);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= SelCallText.text.Length ? SelCallText.text.Length : cursorIndex;
    //            }
    //            break;
    //    }
    //}

    //QMac-90 Channel Number Setup
    //public static void QMacChannelNumberSetup(QMAC90M radio, ButtonKeyCode buttonID)
    //{
    //    Text ChannelNumberText = radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_HASH:
    //            // selcall
    //            // validation
    //            if (ChannelNumberText.text.Equals("-----"))  // ignore ------ das
    //                return;
    //            int countDas = ChannelNumberText.text.Count(c => c == '-');  // count total hyphen using LINQ
    //            if (countDas > 0)
    //            {
    //                radio.qMACDisplayController.SelcallPanel.gameObject.SetActive(true);
    //                radio.qMACDisplayController.Selcall.gameObject.SetActive(false);
    //                radio.qMACDisplayController.sellcallID.gameObject.SetActive(false);
    //            }
    //            if (cursorIndex == 4)
    //            {
    //                ChannelNumberText.text = ChannelNumberText.text.Remove(ChannelNumberText.text.Length - 1);  // ignore last 1 das
    //            }
    //            else if (cursorIndex == 3)
    //            {
    //                ChannelNumberText.text = ChannelNumberText.text.Remove(ChannelNumberText.text.Length - 2);   // ignore last 2 das
    //            }
    //            else if (cursorIndex == 2)
    //            {
    //                ChannelNumberText.text = ChannelNumberText.text.Remove(ChannelNumberText.text.Length - 3);  // ignore last 3 das
    //            }
    //            else if (cursorIndex == 1)
    //            {
    //                ChannelNumberText.text = ChannelNumberText.text.Remove(ChannelNumberText.text.Length - 4);   // ignore last 4 das
    //            }
    //            radio.activeChannel.channelNumber = Convert.ToString(ChannelNumberText.text);
    //            Logger.Log(radio.activeChannel.channelNumber);
    //            radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>().text = "Save";
    //            ChannelNumberText.StartCoroutine(DisplayText(1f, radio));
    //            radio.Navigator.Pop();
    //            cursorIndex = 0;
    //            break;
    //        default:
    //            char input = NumberPadSetting.KeyPadInput(false, buttonID);
    //            if (cursorIndex < ChannelNumberText.text.Length)
    //            {
    //                char[] temp = ChannelNumberText.text.ToCharArray();
    //                temp[cursorIndex] = input;
    //                ChannelNumberText.text = new string(temp);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= ChannelNumberText.text.Length ? ChannelNumberText.text.Length : cursorIndex;
    //            }
    //            break;
    //    }
    //}
    //QMac-90 RX Setup
    //public static void QMacRxSetup(QMAC90M radio, ButtonKeyCode buttonID)
    //{
    //    Text RxText = radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_HASH:
    //            // selcall
    //            // validation
    //            if (RxText.text.Equals("------"))  // ignore ------ das
    //                return;
    //            int countDas = RxText.text.Count(c => c == '-');  // count total hyphen using LINQ
    //            if (countDas > 0)
    //            {
    //                radio.qMACDisplayController.SelcallPanel.gameObject.SetActive(true);
    //                radio.qMACDisplayController.Selcall.gameObject.SetActive(false);
    //                radio.qMACDisplayController.sellcallID.gameObject.SetActive(false);
    //            }
    //            if (cursorIndex == 5)
    //            {
    //                RxText.text = RxText.text.Remove(RxText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 4)
    //            {
    //                RxText.text = RxText.text.Remove(RxText.text.Length - 2);  // ignore last 2 das
    //            }
    //            else if (cursorIndex == 3)
    //            {
    //                RxText.text = RxText.text.Remove(RxText.text.Length - 3);   // ignore last 3 das
    //            }
    //            else if (cursorIndex == 2)
    //            {
    //                RxText.text = RxText.text.Remove(RxText.text.Length - 4);  // ignore last 4 das
    //            }
    //            else if (cursorIndex == 1)
    //            {
    //                RxText.text = RxText.text.Remove(RxText.text.Length - 5);   // ignore last 5 das
    //            }
    //            // save  // selcall number 
    //            radio.activeChannel.rx = Convert.ToDouble(RxText.text);
    //            Logger.Log(radio.activeChannel.rx);
    //            radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>().text = "Save";
    //            RxText.StartCoroutine(DisplayText(1f, radio));
    //            radio.Navigator.Pop();
    //            cursorIndex = 0;
    //            break;
    //        default:
    //            char input = NumberPadSetting.KeyPadInput(false, buttonID);
    //            if (cursorIndex < RxText.text.Length)
    //            {
    //                char[] temp = RxText.text.ToCharArray();
    //                if (cursorIndex == 2)
    //                {
    //                    temp[cursorIndex] = '.';
    //                    cursorIndex++;
    //                }
    //                temp[cursorIndex] = input;
    //                RxText.text = new string(temp);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= RxText.text.Length ? RxText.text.Length : cursorIndex;
    //            }
    //            break;
    //    }
    //}
    //QMac-90 TX Setup
    //public static void QMacTxSetup(QMAC90M radio, ButtonKeyCode buttonID)
    //{
    //    Text TxText = radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_HASH:
    //            // selcall
    //            // validation
    //            if (TxText.text.Equals("-----"))  // ignore ------ das
    //                return;
    //            int countDas = TxText.text.Count(c => c == '-');  // count total hyphen using LINQ
    //            if (countDas > 0)
    //            {
    //                radio.qMACDisplayController.SelcallPanel.gameObject.SetActive(true);
    //                radio.qMACDisplayController.Selcall.gameObject.SetActive(false);
    //                radio.qMACDisplayController.sellcallID.gameObject.SetActive(false);
    //            }
    //            if (cursorIndex == 5)
    //            {
    //                TxText.text = TxText.text.Remove(TxText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 4)
    //            {
    //                TxText.text = TxText.text.Remove(TxText.text.Length - 1);  // ignore last 2 das
    //            }
    //            else if (cursorIndex == 3)
    //            {
    //                TxText.text = TxText.text.Remove(TxText.text.Length - 2);   // ignore last 3 das
    //                TxText.text = TxText.text.Insert(0, "");
    //            }
    //            else if (cursorIndex == 2)
    //            {
    //                TxText.text = TxText.text.Remove(TxText.text.Length - 3);  // ignore last 4 das
    //                TxText.text = TxText.text.Insert(0, "");
    //            }
    //            else if (cursorIndex == 1)
    //            {
    //                TxText.text = TxText.text.Remove(TxText.text.Length - 4);   // ignore last 5 das
    //                TxText.text = TxText.text.Insert(0, "");
    //            }
    //            radio.activeChannel.tx = Convert.ToDouble(TxText.text);
    //            Logger.Log(radio.activeChannel.tx);
    //            // save  // selcall number 
    //            radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>().text = "Save";
    //            TxText.StartCoroutine(DisplayText(1f, radio));
    //            radio.Navigator.Pop();
    //            cursorIndex = 0;
    //            break;
    //        default:
    //            char input = NumberPadSetting.KeyPadInput(false, buttonID);
    //            if (cursorIndex < TxText.text.Length)
    //            {
    //                char[] temp = TxText.text.ToCharArray();
    //                if (cursorIndex == 2)
    //                {
    //                    temp[cursorIndex] = '.';
    //                    cursorIndex++;
    //                }
    //                temp[cursorIndex] = input;
    //                TxText.text = new string(temp);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= TxText.text.Length ? TxText.text.Length : cursorIndex;
    //            }
    //            break;
    //    }
    //}
    //QMac-90 Power Setup
    //public static void QMacPowerSetup(QMAC90M radio, ButtonKeyCode buttonID)
    //{
    //    Text PowerText = radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_1:
    //            PowerText.text = "P  50";
    //            break;
    //        case ButtonKeyCode.NUM_2:
    //            PowerText.text = "P 500";
    //            break;
    //        case ButtonKeyCode.NUM_3:
    //            PowerText.text = "P5000";
    //            break;
    //        case ButtonKeyCode.NUM_HASH:
    //            radio.activeChannel.power = Convert.ToString(PowerText.text);
    //            Logger.Log(radio.activeChannel.power);
    //            radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>().text = "Save";
    //            PowerText.StartCoroutine(DisplayText(1f, radio));
    //            //radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>().text = QmacMenu.qMacProgMenu[QmacMenu.qmacProgIndex];
    //            radio.Navigator.Pop();
    //            break;
    //    }
    //}
    //QMac-90 CODEH // DES Code Setup
    //public static void QMacCodeSetup(QMAC90M radio, ButtonKeyCode buttonID)
    //{
    //    Text CodeNumberText = radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_HASH:
    //            // selcall
    //            // validation
    //            if (CodeNumberText.text.Equals("------------"))  // ignore ------ das
    //                return;
    //            int countDas = CodeNumberText.text.Count(c => c == '-');  // count total hyphen using LINQ
    //            if (countDas > 0)
    //            {
    //                radio.qMACDisplayController.SelcallPanel.gameObject.SetActive(true);
    //                radio.qMACDisplayController.Selcall.gameObject.SetActive(false);
    //                radio.qMACDisplayController.sellcallID.gameObject.SetActive(false);
    //            }
    //            if (cursorIndex == 11)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 10)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 9)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 8)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 7)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 6)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 5)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 1);  // ignore last 2 das
    //            }
    //            if (cursorIndex == 4)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 1);  // ignore last 2 das
    //            }
    //            else if (cursorIndex == 3)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 2);   // ignore last 3 das
    //                CodeNumberText.text = CodeNumberText.text.Insert(0, "");
    //            }
    //            else if (cursorIndex == 2)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 3);  // ignore last 4 das
    //                CodeNumberText.text = CodeNumberText.text.Insert(0, "");
    //            }
    //            else if (cursorIndex == 1)
    //            {
    //                CodeNumberText.text = CodeNumberText.text.Remove(CodeNumberText.text.Length - 4);   // ignore last 5 das
    //                CodeNumberText.text = CodeNumberText.text.Insert(0, "");
    //            }
    //            radio.activeChannel.DESCode = Convert.ToDouble(CodeNumberText.text);
    //            Logger.Log(radio.activeChannel.DESCode);
    //            // save  // selcall number 
    //            radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>().text = "Save";
    //            CodeNumberText.StartCoroutine(DisplayText(1f, radio));
    //            radio.Navigator.Pop();
    //            cursorIndex = 0;
    //            break;
    //        default:
    //            char input = NumberPadSetting.KeyPadInput(false, buttonID);
    //            if (cursorIndex < CodeNumberText.text.Length)
    //            {
    //                char[] temp = CodeNumberText.text.ToCharArray();
    //                temp[cursorIndex] = input;
    //                CodeNumberText.text = new string(temp);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= CodeNumberText.text.Length ? CodeNumberText.text.Length : cursorIndex;
    //            }
    //            break;
    //    }
    //}
    //static IEnumerator DisplayText(float waitTime, QMAC90M radio)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    radio.qMACDisplayController.displayText.transform.Find("Program").GetComponent<Text>().text = QmacMenu.qMacProgMenu[QmacMenu.qmacProgIndex];
    //}
    /*======================
    QMAC90M Settings END
    ======================*/


    /*======================
    XV3088 3088 
    ======================*/

    //Channel Set Up
    //public static void Xv3088ChannelSetUp(XV3088 radio, ButtonKeyCode buttonID)
    //{
    //    Text channelText = radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("ChannelPanel").Find("ChannelNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            radio.tempChannel.channelNumber = channelText.text;
    //            break;
    //        case ButtonKeyCode.PROG:
    //            radio.tempChannel.channelNumber = channelText.text;
    //            break;
    //        case ButtonKeyCode.ENTER:
    //            Logger.Log(channelText.text);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").gameObject.SetActive(true);
    //            radio.tempChannel.channelNumber = channelText.text; //channel number set to temp      
    //            radio.Navigator.Push("frequencySetup");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            isNumberPadActive = false;
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("ChannelPanel").Find("ChannelNumber").GetComponent<Text>().text = radio.activeChannel.channelNumber.ToString(); //set active channel
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>().text = String.Format("{0:00.000}", radio.activeChannel.rx); //set active rx frequency
    //            radio.Navigator.Pop();
    //            break;
    //        case ButtonKeyCode.WH:
    //            Logger.Log("whisper Mode On");
    //            break;
    //        case ButtonKeyCode.PT_PressedDown:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>().text = String.Format("{0:00.000}", radio.activeChannel.tx);
    //            Logger.Log("tx frequency is" + radio.activeChannel.tx);
    //            break;
    //        case ButtonKeyCode.PT_PressedUp:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>().text = String.Format("{0:00.000}", radio.activeChannel.rx);
    //            Logger.Log("rx frequency is" + radio.activeChannel.rx);
    //            break;
    //        default:
    //            //Channel Number
    //            char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //            char[] temp = channelText.text.ToCharArray();
    //            temp[cursorIndex] = input;
    //            channelText.text = new string(temp);
    //            radio.tempChannel.channelNumber = input.ToString(); //channel number set to temp                                            //get Number and set channel Input Field
    //            break;
    //    }
    //}

    //Rx Frequency Setup
    //public static void Xv3088FrequencySetUp(XV3088 radio, ButtonKeyCode buttonID)
    //{
    //    Text frequencyText = radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            if (radio.tempChannel.isSdxEnable)
    //            {
    //                radio.xV3088DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(true); //sdx display enable
    //                radio.tempChannel.rx = Convert.ToDouble(frequencyText.text);
    //                isNumberPadActive = false;
    //                radio.tempChannel.isSdxEnable = true;
    //                radio.Navigator.Push("SdxTxFrequecySet");
    //            }
    //            else
    //            {
    //                radio.xV3088DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(true);
    //                radio.tempChannel.rx = Convert.ToDouble(frequencyText.text);
    //                isNumberPadActive = false;
    //                radio.tempChannel.isSdxEnable = false;
    //                radio.Navigator.Push("SelCallMode");
    //            }
    //            break;

    //        case ButtonKeyCode.CANCEL:
    //            frequencyText.text = "";
    //            //radio.Navigator.Pop();
    //            break;
    //        default:
    //            //Frequency Input
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                frequencyText.text = ""; //current Frequency Number null
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (frequencyText.text.Length == 2)
    //                {
    //                    frequencyText.text += '.'; //adding .
    //                }
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID); //number pad input
    //                frequencyText.text += input; //set frequency text
    //                if (frequencyText.text.Length > 6)
    //                {
    //                    frequencyText.text = frequencyText.text.Remove(frequencyText.text.Length - 1); //length bound
    //                }
    //                if (float.Parse(frequencyText.text) > 87.975)
    //                {
    //                    frequencyText.text = "87.975"; // highest limit
    //                }
    //                if (frequencyText.text.Length == 2 && float.Parse(frequencyText.text) <= 30)
    //                {
    //                    frequencyText.text = "30"; //lowest limit
    //                }
    //                string activeFrequency = String.Format("{0:00.000}", frequencyText.text);
    //                radio.tempChannel.rx = Convert.ToDouble(activeFrequency); // frquency set to temp
    //                radio.tempChannel.isSdxEnable = true; //sdx Mode enable
    //            }
    //            break;
    //    }
    //}

    //SDX Tx Frequency Setup
    //public static void Xv3088TxFrequencySet(XV3088 radio, ButtonKeyCode buttonID)
    //{
    //    Text frequencyText = radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            frequencyText.text = "";
    //            radio.tempChannel.isSdxEnable = true; //enable sdx
    //            radio.Navigator.Push("SelCallMode");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SelText").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(false);
    //            radio.tempChannel.isSdxEnable = false;
    //            radio.Navigator.Push("SecCallMode");
    //            break;

    //    }
    //}

    //sel call mode
    //public static void Xv3088SelCallMode(XV3088 radio, ButtonKeyCode buttonID)
    //{
    //    Text frequencyText = radio.xV3088DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SelText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = true;
    //            radio.Navigator.Push("SecCallMode");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SelText").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(false);
    //            radio.tempChannel.isSdxEnable = false;
    //            radio.Navigator.Push("SecCallMode");
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                frequencyText.text = ""; //current Frequency Number null
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (frequencyText.text.Length == 2)
    //                {
    //                    frequencyText.text += '.';
    //                }
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                frequencyText.text += input;
    //                if (frequencyText.text.Length > 6)
    //                {
    //                    frequencyText.text = frequencyText.text.Remove(frequencyText.text.Length - 1);
    //                }
    //                if (float.Parse(frequencyText.text) > 87.975)
    //                {
    //                    frequencyText.text = "87.975";
    //                }
    //                if (frequencyText.text.Length == 2 && float.Parse(frequencyText.text) <= 30)
    //                {
    //                    frequencyText.text = "30";
    //                }
    //                //Low Check
    //                string activeFrequency = String.Format("{0:00.000}", frequencyText.text);
    //                radio.tempChannel.tx = Convert.ToDouble(activeFrequency); // frquency set to temp

    //            }
    //            break;
    //    }
    //}

    //sec Call Mode
    //public static void Xv3088SecCallMode(XV3088 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SecText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = true;
    //            radio.Navigator.Push("sqlCallMode");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SelText").gameObject.SetActive(false);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SecText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = false;
    //            radio.Navigator.Push("sqlCallMode");
    //            break;
    //    }
    //}

    //Sql call Mode
    //public static void Xv3088SqlCallMode(XV3088 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SqText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = true;
    //            radio.Navigator.Push("ProgramCallMode");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SecText").gameObject.SetActive(false);
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SqText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = false;
    //            radio.Navigator.Push("ProgramCallMode");
    //            break;
    //    }
    //}

    //Programe Call mode
    //public static void Xv3088ProgrameCallMode(XV3088 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            radio.channelList.Add(radio.tempChannel);
    //            radio.activeChannel = radio.tempChannel;
    //            radio.RfreshHomeScreen();
    //            Logger.Log("Channel Set and Green Light Blink on the radio");
    //            isNumberPadActive = false;
    //            if (radio.tempChannel.isSdxEnable)
    //            {
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.tempChannel.isSdxEnable = false;
    //            }
    //            else
    //            {
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //            }
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(false);
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.xV3088DisplayController.HomePanel.transform.Find("Header").Find("SqText").gameObject.SetActive(false);
    //            break;
    //    }
    //}

    //-------------- FSG 90 --------------//
    //public static void ChannelNumberSetup(FSG90HI71 radio, ButtonKeyCode buttonID)
    //{
    //    Text ChannelNumber = radio.FSG90HI71DisplayController.HomePanel.transform.Find("LowerPart").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            //ChannelNumber.text 
    //            int Number = Int16.Parse(ChannelNumber.text);
    //            Number += 1;
    //            Number = Number > 2278 ? 2278 : Number;
    //            ChannelNumber.text = Number.ToString();
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            int DOwnNumber = Int16.Parse(ChannelNumber.text);
    //            DOwnNumber -= 1;
    //            DOwnNumber = DOwnNumber < 0 ? 0 : DOwnNumber;
    //            ChannelNumber.text = DOwnNumber.ToString();
    //            break;
    //        case ButtonKeyCode.PROG:
    //            radio.activeChannel.channelNumber = ChannelNumber.text;
    //            print(ChannelNumber.text);

    //            // Load Rx Number and Push RxSet
    //            string activeRx = String.Format("{0:000.000}", radio.activeChannel.rx);
    //            radio.FSG90HI71DisplayController.HomePanel.transform.Find("LowerPart").GetComponent<Text>().text = activeRx;
    //            radio.FSG90HI71DisplayController.HomePanel.transform.Find("CH").gameObject.SetActive(false);
    //            radio.Navigator.Push("RxSet");
    //            break;
    //    }
    //}
    //FSG 90 RX Setup
    private static int counter = 0;

    //public static void RxNumberSetup(FSG90HI71 radio, ButtonKeyCode buttonID)
    //{
    //    Text RxNumber = radio.FSG90HI71DisplayController.HomePanel.transform.Find("LowerPart").GetComponent<Text>();
    //    string active = String.Format("{0:000.000}", RxNumber.text);
    //    var splitted = active.Split('.');
    //    string afterDotValue = splitted[1];
    //    string beforeDotValue = splitted[0];
    //    int KhzValue = Int16.Parse(afterDotValue);
    //    int MhzValue = Int16.Parse(beforeDotValue);
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            if (counter == 0)  // Up arrow for KHz change
    //            {
    //                KhzValue += 5;
    //                RxNumber.text = splitted[0] + "." + KhzValue.ToString();
    //            }
    //            else if (counter == 1)  // Up arrow for MHz change
    //            {
    //                MhzValue += 5;
    //                RxNumber.text = MhzValue + "." + KhzValue.ToString();
    //            }
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            if (counter == 0)      // Up arrow for KHz change
    //            {
    //                KhzValue -= 5;
    //                KhzValue = KhzValue < 0 ? 0 : KhzValue;
    //                RxNumber.text = splitted[0] + "." + KhzValue.ToString();
    //            }
    //            else if (counter == 1)  // Up arrow for MHz change
    //            {
    //                MhzValue -= 5;
    //                MhzValue = MhzValue < 0 ? 0 : MhzValue;
    //                RxNumber.text = MhzValue + "." + KhzValue.ToString();
    //            }
    //            break;
    //        case ButtonKeyCode.CHANNEL:
    //            counter++;
    //            break;
    //        case ButtonKeyCode.TX_Start:
    //            radio.activeChannel.rx = Convert.ToDouble(RxNumber.text);
    //            print(radio.activeChannel.rx);
    //            counter = 0;
    //            // Load TX value
    //            string activeRx = String.Format("{0:000.000}", radio.activeChannel.rx);
    //            string activeTx = String.Format("{0:000.000}", radio.activeChannel.tx);
    //            radio.FSG90HI71DisplayController.HomePanel.transform.Find("UpperPart").GetComponent<Text>().text = activeRx;
    //            radio.FSG90HI71DisplayController.HomePanel.transform.Find("LowerPart").GetComponent<Text>().text = activeTx;
    //            radio.Navigator.Push("TxSet");
    //            break;
    //    }
    //}
    // FSG 90 Tx Setup
    //public static void TxNumberSetup(FSG90HI71 radio, ButtonKeyCode buttonID)
    //{
    //    Text TxNumber = radio.FSG90HI71DisplayController.HomePanel.transform.Find("LowerPart").GetComponent<Text>();
    //    string active = String.Format("{0:000.000}", TxNumber.text);
    //    var splitted = active.Split('.');
    //    string afterDotValue = splitted[1];
    //    string beforeDotValue = splitted[0];
    //    int KhzValue = Int16.Parse(afterDotValue);
    //    int MhzValue = Int16.Parse(beforeDotValue);
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            if (counter == 0)  // Up arrow for KHz change
    //            {
    //                KhzValue += 5;
    //                TxNumber.text = splitted[0] + "." + KhzValue.ToString();
    //            }
    //            else if (counter == 1)  // Up arrow for MHz change
    //            {
    //                MhzValue += 5;
    //                TxNumber.text = MhzValue + "." + KhzValue.ToString();
    //            }
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            if (counter == 0)      // Up arrow for KHz change
    //            {
    //                KhzValue -= 5;
    //                KhzValue = KhzValue < 0 ? 0 : KhzValue;
    //                TxNumber.text = splitted[0] + "." + KhzValue.ToString();
    //            }
    //            else if (counter == 1)  // Up arrow for MHz change
    //            {
    //                MhzValue -= 5;
    //                MhzValue = MhzValue < 0 ? 0 : MhzValue;
    //                TxNumber.text = MhzValue + "." + KhzValue.ToString();
    //            }
    //            break;
    //        case ButtonKeyCode.CHANNEL:
    //            counter++;
    //            break;
    //        case ButtonKeyCode.SQ:
    //            radio.activeChannel.tx = Convert.ToDouble(TxNumber.text);
    //            print(radio.activeChannel.tx);
    //            counter = 0;
    //            // Load TX value
    //            string activeRx = String.Format("{0:000.000}", radio.activeChannel.rx);
    //            string activeTx = String.Format("{0:000.000}", radio.activeChannel.tx);
    //            radio.FSG90HI71DisplayController.HomePanel.transform.Find("UpperPart").GetComponent<Text>().text = activeRx;
    //            radio.FSG90HI71DisplayController.HomePanel.transform.Find("LowerPart").GetComponent<Text>().text = activeTx;
    //            radio.FSG90HI71DisplayController.HomePanel.transform.Find("DisplayIcon").Find("Antena").gameObject.SetActive(true);
    //            radio.Navigator.Pop();
    //            radio.Navigator.Pop();
    //            radio.Navigator.Pop();
    //            radio.RefreshHomeScreen();
    //            break;
    //    }
    //}
        
   

    /*======================
    RF - 1350 Channel and Frequency Setup 
    ======================*/

    //Channel Set Up
    //public static void rf1350ChannelSetUp(RF1350 radio, ButtonKeyCode buttonID)
    //{
    //    Text channelText = radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("ChannelPanel").Find("ChannelNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            radio.tempChannel.channelNumber = channelText.text;
    //            break;
    //        case ButtonKeyCode.PROG:
    //            radio.tempChannel.channelNumber = channelText.text;
    //            break;
    //        case ButtonKeyCode.ENTER:
    //            Logger.Log(channelText.text);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").gameObject.SetActive(true);
    //            radio.tempChannel.channelNumber = channelText.text; //channel number set to temp      
    //            radio.Navigator.Push("frequencySetup");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            isNumberPadActive = false;
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("ChannelPanel").Find("ChannelNumber").GetComponent<Text>().text = radio.activeChannel.channelNumber.ToString(); //set active channel
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>().text = String.Format("{0:00.000}", radio.activeChannel.rx); //set active rx frequency
    //            radio.Navigator.Pop();
    //            break;
    //        case ButtonKeyCode.WH:
    //            Logger.Log("whisper Mode On");
    //            break;
    //        case ButtonKeyCode.PT_PressedDown:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>().text = String.Format("{0:00.000}", radio.activeChannel.tx);
    //            Logger.Log("tx frequency is" + radio.activeChannel.tx);
    //            break;
    //        case ButtonKeyCode.PT_PressedUp:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>().text = String.Format("{0:00.000}", radio.activeChannel.rx);
    //            Logger.Log("rx frequency is" + radio.activeChannel.rx);
    //            break;
    //        default:
    //            //Channel Number
    //            char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //            char[] temp = channelText.text.ToCharArray();
    //            temp[cursorIndex] = input;
    //            channelText.text = new string(temp);
    //            radio.tempChannel.channelNumber = input.ToString(); //channel number set to temp                                            //get Number and set channel Input Field
    //            break;
    //    }
    //}

    ////Rx Frequency Setup
    //public static void rf1350FrequencySetUp(RF1350 radio, ButtonKeyCode buttonID)
    //{
    //    Text frequencyText = radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            if (radio.tempChannel.isSdxEnable)
    //            {
    //                radio.rf1350DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(true); //sdx display enable
    //                radio.tempChannel.rx = Convert.ToDouble(frequencyText.text);
    //                isNumberPadActive = false;
    //                radio.tempChannel.isSdxEnable = true;
    //                radio.Navigator.Push("SdxTxFrequecySet");
    //            }
    //            else
    //            {
    //                radio.rf1350DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(true);
    //                radio.tempChannel.rx = Convert.ToDouble(frequencyText.text);
    //                isNumberPadActive = false;
    //                radio.tempChannel.isSdxEnable = false;
    //                radio.Navigator.Push("SelCallMode");
    //            }
    //            break;

    //        case ButtonKeyCode.CANCEL:
    //            frequencyText.text = "";
    //            //radio.Navigator.Pop();
    //            break;
    //        default:
    //            //Frequency Input
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                frequencyText.text = ""; //current Frequency Number null
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (frequencyText.text.Length == 2)
    //                {
    //                    frequencyText.text += '.'; //adding .
    //                }
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID); //number pad input
    //                frequencyText.text += input; //set frequency text
    //                if (frequencyText.text.Length > 6)
    //                {
    //                    frequencyText.text = frequencyText.text.Remove(frequencyText.text.Length - 1); //length bound
    //                }
    //                if (float.Parse(frequencyText.text) > 87.975)
    //                {
    //                    frequencyText.text = "87.975"; // highest limit
    //                }
    //                if (frequencyText.text.Length == 2 && float.Parse(frequencyText.text) <= 30)
    //                {
    //                    frequencyText.text = "30"; //lowest limit
    //                }
    //                string activeFrequency = String.Format("{0:00.000}", frequencyText.text);
    //                radio.tempChannel.rx = Convert.ToDouble(activeFrequency); // frquency set to temp
    //                radio.tempChannel.isSdxEnable = true; //sdx Mode enable
    //            }
    //            break;
    //    }
    //}

    //SDX Tx Frequency Setup
    //public static void rf1350TxFrequencySet(RF1350 radio, ButtonKeyCode buttonID)
    //{
    //    Text frequencyText = radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            frequencyText.text = "";
    //            radio.tempChannel.isSdxEnable = true; //enable sdx
    //            radio.Navigator.Push("SelCallMode");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SelText").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(false);
    //            radio.tempChannel.isSdxEnable = false;
    //            radio.Navigator.Push("SecCallMode");
    //            break;
    //    }
    //}

    //sel call mode
    //public static void rf1350SelCallMode(RF1350 radio, ButtonKeyCode buttonID)
    //{
    //    Text frequencyText = radio.rf1350DisplayController.HomePanel.transform.Find("Middle").Find("FrequencyPanel").Find("FequencyNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SelText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = true;
    //            radio.Navigator.Push("SecCallMode");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SelText").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(false);
    //            radio.tempChannel.isSdxEnable = false;
    //            radio.Navigator.Push("SecCallMode");
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                frequencyText.text = ""; //current Frequency Number null
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (frequencyText.text.Length == 2)
    //                {
    //                    frequencyText.text += '.';
    //                }
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                frequencyText.text += input;
    //                if (frequencyText.text.Length > 6)
    //                {
    //                    frequencyText.text = frequencyText.text.Remove(frequencyText.text.Length - 1);
    //                }
    //                if (float.Parse(frequencyText.text) > 87.975)
    //                {
    //                    frequencyText.text = "87.975";
    //                }
    //                if (frequencyText.text.Length == 2 && float.Parse(frequencyText.text) <= 30)
    //                {
    //                    frequencyText.text = "30";
    //                }
    //                //Low Check
    //                string activeFrequency = String.Format("{0:00.000}", frequencyText.text);
    //                radio.tempChannel.tx = Convert.ToDouble(activeFrequency); // frquency set to temp

    //            }
    //            break;
    //    }
    //}

    ////sec Call Mode
    //public static void rf1350SecCallMode(RF1350 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SecText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = true;
    //            radio.Navigator.Push("sqlCallMode");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SelText").gameObject.SetActive(false);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SecText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = false;
    //            radio.Navigator.Push("sqlCallMode");
    //            break;
    //    }
    //}

    //Sql call Mode
    //public static void rf13508SqlCallMode(RF1350 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SqText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = true;
    //            radio.Navigator.Push("ProgramCallMode");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").gameObject.SetActive(true);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SecText").gameObject.SetActive(false);
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SqText").gameObject.SetActive(true);
    //            radio.tempChannel.isSdxEnable = false;
    //            radio.Navigator.Push("ProgramCallMode");
    //            break;
    //    }
    //}

    //Programe Call mode
    //public static void rf1350ProgrameCallMode(RF1350 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            radio.channelList.Add(radio.tempChannel);
    //            radio.activeChannel = radio.tempChannel;
    //            radio.RfreshHomeScreen();
    //            Logger.Log("Channel Set and Green Light Blink on the radio");
    //            isNumberPadActive = false;
    //            if (radio.tempChannel.isSdxEnable)
    //            {
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.tempChannel.isSdxEnable = false;
    //            }
    //            else
    //            {
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //                radio.Navigator.Pop();
    //            }
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Bottom").gameObject.SetActive(false);
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.rf1350DisplayController.HomePanel.transform.Find("Header").Find("SqText").gameObject.SetActive(false);
    //            break;
    //    }
    //}

}