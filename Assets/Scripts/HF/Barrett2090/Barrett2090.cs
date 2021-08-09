using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
[RequireComponent(typeof(LoggerGUI))]
public class Barrett2090 : MonoBehaviour
{
    // Events
    public delegate void OnButtonPressed(ButtonKeyCode buttonID, float delay);
    public OnButtonPressed onPressed;
    // Controller
    public DisplayControllerBarrett2090 displayControllerBarrett2090;
    // Menu Controll
    public Stack Navigator = new Stack();
    public Stack<int> menuIndexStack = new Stack<int>();
    public int MenuIndex = 0;
    int interval = 1;
    float time = 0f;
    private float LightOffTimeOut = 10f;
    void Start()
    {
        // vHFDisplayController = gameObject.GetComponent(typeof(VHFDisplayController)) as VHFDisplayController;
        channelList.Add(new Channel("0001", 6565.5f, 6455.5f, "ARNOB", Modulation.USB, ChannelPowerMode.LOW, CallFormat.INTERNATIONAL));
        channelList.Add(new Channel("0002", 7557.6f, 6125.5f, "SAIFUL", Modulation.AM, ChannelPowerMode.LOW, CallFormat.INTERNATIONAL));
        channelList.Add(new Channel("0059", 4459.2f, 7565.5f, "TAREK", Modulation.CW, ChannelPowerMode.HIGH, CallFormat.INTERNATIONAL));
        channelList.Add(new Channel("0088", 8422.1f, 6355.5f, "ASHIK", Modulation.LSB, ChannelPowerMode.LOW, CallFormat.INTERNATIONAL));
        // Debug.Log(vHFDisplayController.menuText);
        //displayControllerBarrett2090.setMenuText("HOME");
        onPressed += GetButtonClick;
        Navigator.Push("HOME");
        menuIndexStack.Push(0);
        channelLabelList.Add("ZMS");
        channelLabelList.Add("TAREK");
        channelLabelList.Add("SAIFUL");
        channelLabelList.Add("ASHIQ");
        channelLabelList.Add("ARNOB");
        channelLabelList.Add("ASFIQ");

        //adressBook data insert
        adressBook.Add(new Contact("ZMS", 1000, true));
        adressBook.Add(new Contact("TAREK", 1001, false));
        adressBook.Add(new Contact("ASHIQ", 1002, false));
        adressBook.Add(new Contact("ARNOB", 1003, true));


        //phoneBook data insert
        phoneBook.Add(new Contact("Saiful", 09200, true));
        phoneBook.Add(new Contact("Ashfik", 01231000, true));
        phoneBook.Add(new Contact("ZMS", 01231000455, true));
        phoneBook.Add(new Contact("Arnob", 0123155, true));

        activeChannel = channelList[activeChannelIndex];
        //intialization of home screen
        RefreshHomeScreen();
    }

    public void RefreshHomeScreen()
    {
        //displayControllerBarrett2090.ChannelInputPanel.SetActive(true);
        displayControllerBarrett2090.ChangeBackLight(this.backlightLevel);
        displayControllerBarrett2090.ALEPanel.SetActive(this.isALEEnable);
        displayControllerBarrett2090.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel: ";
        displayControllerBarrett2090.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = activeChannel.channelNumber;
        displayControllerBarrett2090.HomePanel.transform.Find("FrequencyPanel").Find("FrequencyNumber").GetComponent<Text>().text = activeChannel.rx.ToString();
        displayControllerBarrett2090.HomePanel.transform.Find("FrequencyPanel").Find("Unit").GetComponent<Text>().text = "KHz";
        displayControllerBarrett2090.HomePanel.transform.Find("InfoPanel").Find("Modulation").Find("MOD").GetComponent<Text>().text = activeChannel.modulation.ToString();
        if (activeChannel.power == ChannelPowerMode.LOW)
        {
            displayControllerBarrett2090.HomePanel.transform.Find("InfoPanel").Find("Power").Find("PowerMode").GetComponent<Text>().text = "LP";
        }
        else if (activeChannel.power == ChannelPowerMode.HIGH)
        {
            displayControllerBarrett2090.HomePanel.transform.Find("InfoPanel").Find("Power").Find("PowerMode").GetComponent<Text>().text = "HP";
        }
        else
        {
            displayControllerBarrett2090.HomePanel.transform.Find("InfoPanel").Find("Power").Find("PowerMode").GetComponent<Text>().text = "MP";
        }

    }


    public void PowerOnOff(bool isPower)
    {
        if (isPower)
        {
            this.isPowerOn = true;
            PowerSettings.PowerOnOff(this);
            displayControllerBarrett2090.splashScreen.gameObject.SetActive(true);
            GetComponentInChildren<FrequencyAnimController>().startTransmitAnim(displayControllerBarrett2090.splashScreen.GetComponent<SplashScreen>().Seconds);

        }
        else
        {
            this.isPowerOn = false;
            PowerSettings.PowerOnOff(this);
        }
    }

    void GetButtonClick(ButtonKeyCode buttonID, float delay)
    {
        //Awake Device Display
        LightOffTimeOut = this.backlightTimeOut == BacklightTimeout.LONG_TIMEOUT ? Time.time + 50 : Time.time + 10;
        displayControllerBarrett2090.ChangeBackLight(this.backlightLevel);

        //Ui Power Button Only
        if (buttonID == ButtonKeyCode.POWER)
        {
            this.PowerOnOff(!this.isPowerOn);
        }
        //If redio is switched off;
        if (!isPowerOn)
        {
            return;
        }
        switch (Navigator.Peek())
        {
            //Shortcut function
            case "HOME":
                switch (buttonID)
                {
                    case ButtonKeyCode.MENU:
                        MenuIndex = 0;
                        menuIndexStack.Push(MenuIndex);
                        if (delay >= 2)
                        {
                            Navigator.Push("Protected");
                            displayControllerBarrett2090.ProtectedPanel.SetActive(true);
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, menuIndexStack.Peek()));
                        }
                        else
                        {
                            Navigator.Push("Standart Menu");
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress, menuIndexStack.Peek()));
                        }
                        displayControllerBarrett2090.MenuPanel.SetActive(true);
                        break;
                    case ButtonKeyCode.CHANNEL:
                        if (delay < 2f)
                        {
                            Navigator.Push("CHANNEL");
                            ChannelSetting.cursorIndex = 0;
                            displayControllerBarrett2090.ChannelInputPanel.transform.Find("CH").GetComponent<Text>().text = "Channel: ";
                            displayControllerBarrett2090.ChannelInputPanel.transform.Find("ChannelNumber").GetComponent<Text>().text = "----";
                            displayControllerBarrett2090.LeftUpConrer.gameObject.SetActive(false);
                            displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").gameObject.SetActive(false);
                            displayControllerBarrett2090.ChannelInputPanel.SetActive(true); //channel input Panel active
                        }
                        else
                        {
                            //displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label").GetComponent<Text>().text = string.Format("<b>RX Freq:</b> {0} kHz", this.activeChannel.rx);
                            //displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = string.Format("<b>TX Freq:</b> {0} kHz", this.activeChannel.tx);
                            //displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = string.Format("<b>Mode:</b> {0}", this.activeChannel.modulation);
                            //displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = string.Format("<b>Power:</b> {0}W", GetPowerOutput(this.activeChannel.power));
                            //displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").gameObject.SetActive(true);
                            //channelAttributeCounter = 0;
                            OpenChannelAttributesPrompt();
                        }

                        break;
                    case ButtonKeyCode.CANCEL:
                        // more conditions may be added 
                        if (displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").gameObject.activeSelf)
                        {
                            displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").gameObject.SetActive(false); 
                        }
                        break;
                    case ButtonKeyCode.RIGHT_ARROW:
                        PowerSettings.ChangePowerMode(this);
                        break;
                    case ButtonKeyCode.UP_ARROW:
                        //channel attribute action
                        if (displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").gameObject.activeSelf)
                        {
                            ChannelAttributesUpButtonPressed();
                        }
                        else
                        {
                            //change channel
                            ChannelSetting.ChannelUp(this);
                        }
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        //channel attribute action
                        if (displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").gameObject.activeSelf)
                        {
                            ChannelAttributesDownButtonPressed();
                        }
                        //change channel
                        ChannelSetting.ChannelDown(this);
                        break;
                    case ButtonKeyCode.PROG:
                        //Frequency setup
                        Navigator.Push(ButtonKeyCode.PROG);
                        displayControllerBarrett2090.FrequencyInputPanel.SetActive(true);
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").gameObject.SetActive(true);
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("CH").gameObject.SetActive(true);
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Unit").gameObject.SetActive(true);
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Header").gameObject.SetActive(true);
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("CH").GetComponent<Text>().text = "Rx";
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Label").gameObject.SetActive(false);
                        string active = String.Format("{0:00000.000}", activeChannel.rx);
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("RxNumber").GetComponent<Text>().text = active.ToString();
                        displayControllerBarrett2090.FrequencyInputPanel.transform.Find("Header").GetComponent<Text>().text = "Rx Frequency";
                        break;
                    case ButtonKeyCode.CALL:
                        MenuIndex = 0;
                        menuIndexStack.Push(MenuIndex);
                        if (delay < 2)
                        {
                            Navigator.Push("Send Call");
                            displayControllerBarrett2090.CallPanel.SetActive(true); //Call input Panel active
                            displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                            displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call";
                        }
                        else
                        {
                            Navigator.Push("Call History");
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Call History"], MenuIndex));
                            displayControllerBarrett2090.MenuPanel.SetActive(true);
                        }

                        break;
                    case ButtonKeyCode.SCAN:
                        Logger.Log("Select Scan Table");
                        break;
                    case ButtonKeyCode.SCRAM:
                        if (this.hoppingPin.ToString().Length > 0 && isGPSEnable)
                        {
                            this.isHoppingEnable = true;
                            Logger.Log(" Device Hopping : " + this.isHoppingEnable);
                        }
                        else
                        {
                            Logger.Log(" GPS is Not Available");
                        }
                        break;
                    case ButtonKeyCode.NUM_4: // Receiver tune
                        Navigator.Push("RECEIVER");
                        displayControllerBarrett2090.ReceiverTunePanel.SetActive(true);
                        displayControllerBarrett2090.HomePanel.transform.Find("InfoPanel").gameObject.SetActive(false);
                        displayControllerBarrett2090.HomePanel.transform.Find("GPSPanel").gameObject.SetActive(false);
                        displayControllerBarrett2090.ReceiverTunePanel.transform.Find("Name").GetComponent<Text>().text = "Receiver Tune";
                        displayControllerBarrett2090.ReceiverTunePanel.transform.Find("Unit").GetComponent<Text>().text = "KHz";
                        string receiverTuneNumber = string.Format("{0:00000.000}", activeChannel.rx);
                        displayControllerBarrett2090.ReceiverTunePanel.transform.Find("EntryField").GetComponent<Text>().text = receiverTuneNumber;
                        break;
                }
                break;
            case "CHANNEL":
                ChannelSetting.ChannelSwitching(this, buttonID); // channel swiching with cursor setting
                break;
            case ButtonKeyCode.PROG:
                // ChannelSetting.FrequencySetup(this, buttonID);
                ChannelSetting.RxSetting(this, buttonID);
                break;
            case "TX":
                ChannelSetting.TxSetting(this, buttonID);
                break;
            case "CHANNEL_LABEL":
                ChannelSetting.ChannelLabelSetting(this, buttonID);
                break;
            case "MODULATION":
                ChannelSetting.ModulationSetting(this, buttonID);
                break;
            case "CHANNEL_POWER_MODE":
                ChannelSetting.ChannelPowerSetting(this, buttonID);
                break;
            case "CALL_FORMAT":
                ChannelSetting.CallFormatSetting(this, buttonID);
                break;
            case "RECEIVER":
                ChannelSetting.ReceiverTuneSetting(this, buttonID);
                break;
            case "Protected":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info.Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info.Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.info, MenuIndex);
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);
                        if (selectedOption == "General")
                        {
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info[selectedOption], MenuIndex));
                            displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = "";
                        }
                        else if (selectedOption == "Scan Tables")
                        {
                            displayControllerBarrett2090.setMenuText(BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel);
                            displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count == 0 ? "Empty" : BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count + " Entries";
                        }
                        else if (selectedOption == "ALE Setting")
                        {
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info[selectedOption], MenuIndex));
                            displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.isALEEnable ? "Enable" : "Disable";
                        }
                        else if (selectedOption == "I/O Setting")
                        {
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info[selectedOption], MenuIndex));
                            displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.isRs323Enable ? "Enabled" : "Disabled";
                        }
                        else if (selectedOption == "RF Setting")
                        {
                            //Logger.Log("RF Setting Entered");
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info[selectedOption], MenuIndex));
                            displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = "";
                        }
                        else
                        {
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info[selectedOption], MenuIndex));
                        }

                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText("HOME");
                        break;
                        // Navigator.Pop();
                        // menuIndexStack.Pop();
                        // displayControllerBarrett2090.SetRightCorner(Navigator.Peek().ToString());
                        // break;
                }
                break;
            case "General":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["General"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["General"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        if (
                              getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex) != "Secure Call Code"
                              && getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex) != "Option Installation"

                              && getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex) != "Set Date"
                              && getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex) != "Set Clock"
                        )
                        {
                            string selectedOption = getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex);
                            MenuIndex = 0;
                            Navigator.Push(selectedOption);
                            menuIndexStack.Push(MenuIndex);

                            if (selectedOption == "Hopping Pin")
                            {
                                displayControllerBarrett2090.setMenuText("Enter Hopping Pin");
                                displayControllerBarrett2090.MenuPanel.transform.Find("HoppingPinInput").gameObject.SetActive(true);
                                displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                                displayControllerBarrett2090.MenuPanel.transform.Find("HoppingPinInput").GetComponent<Text>().text = "--------";
                            }
                            else if (selectedOption == "BITE Test")
                            {
                                Logger.Log("BITE Test Menu Opened");
                                //displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info[selectedOption], MenuIndex));
                                //displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                                //displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                                //displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = "";
                            }
                            else
                            {
                                displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"][selectedOption], MenuIndex));
                            }
                        }
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        break;
                        // Navigator.Pop();
                        // menuIndexStack.Pop();
                        // displayControllerBarrett2090.SetRightCorner(Navigator.Peek().ToString());
                        // break;
                }
                break;
            case "Mic up/down keys":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["General"]["Mic up/down keys"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Mic up/down keys"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["General"]["Mic up/down keys"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Mic up/down keys"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                }

                break;
            case "GPS":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["I/O Setting"]["GPSt"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["GPS"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["I/O Setting"]["GPS"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["GPS"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:

                        this.isGPSEnable = getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["GPS"], MenuIndex) == "Enable" ? true : false;
                        this.onPressed(ButtonKeyCode.CANCEL, 1);
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        break;
                }

                break;
            case "Internal Modem":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["General"]["Internal Modem"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Internal Modem"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["General"]["Internal Modem"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Internal Modem"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                }

                break;
            case "Upload Pack":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["General"]["Upload Pack"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Upload Pack"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["General"]["Upload Pack"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Upload Pack"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                }

                break;
            case "Security Level":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["General"]["Security Level"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Security Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["General"]["Security Level"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Security Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                }

                break;
            case "Channel Levels":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["General"]["Channel Levels"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Channel Levels"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["General"]["Channel Levels"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Channel Levels"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                }

                break;
            case "Transmit Time Out":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["General"]["Transmit Time Out"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Transmit Time Out"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["General"]["Transmit Time Out"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Transmit Time Out"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                }

                break;
            case "Tx Over Beep":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["General"]["Tx Over Beep"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Tx Over Beep"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["General"]["Tx Over Beep"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"]["Tx Over Beep"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["General"], MenuIndex));
                        break;
                }

                break;
            case "ALE Setting":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        if (getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex) == "ALE State")
                        {
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        }
                        else
                        {
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        }

                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.isALEEnable ? "Enable" : "Disable";
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        if (getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex) == "ALE State")
                        {
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        }
                        else
                        {
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        }
                        break;
                    case ButtonKeyCode.OK:
                        if (
                              getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex) != "Ber Threshold"
                              && getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex) != "Sinad Threshold"
                        )
                        {
                            string selectedOption = getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex);
                            MenuIndex = 0;
                            Navigator.Push(selectedOption);
                            menuIndexStack.Push(MenuIndex);
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"][selectedOption], MenuIndex));
                        }

                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        break;
                }
                break;
            case "ALE State":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["ALE State"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["ALE State"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["ALE State"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["ALE State"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        this.isALEEnable = getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["ALE State"], MenuIndex) == "Enabled" ? true : false;
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.isALEEnable ? "Enable" : "Disable";
                        this.RefreshHomeScreen();
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        break;
                }

                break;
            case "Threshold Test":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Threshold Test"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Threshold Test"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Threshold Test"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Threshold Test"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "LQA Decay Rate":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["LQA Decay Rate"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["LQA Decay Rate"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["LQA Decay Rate"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["LQA Decay Rate"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "LQA Averaging":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["LQA Averaging"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["LQA Averaging"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["LQA Averaging"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["LQA Averaging"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "Exchange Mode":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Exchange Mode"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Exchange Mode"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Exchange Mode"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Exchange Mode"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "LQA Exchange":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["LQA Exchange"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["LQA Exchange"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["LQA Exchange"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["LQA Exchange"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "Sounding Address":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Sounding Address"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Sounding Address"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Sounding Address"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Sounding Address"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "Sounding Control":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Sounding Control"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Sounding Control"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Sounding Control"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Sounding Control"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "Response Control":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Response Control"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Response Control"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Response Control"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Response Control"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "Transmit Control":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Transmit Control"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Transmit Control"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Transmit Control"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Transmit Control"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "Scan List":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Scan List"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Scan List"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Scan List"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Scan List"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "Autofill":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Autofill"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Autofill"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Autofill"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Autofill"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "I/O Setting":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["I/O Setting"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        string mssg = "";
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex);
                        if (selectedOption == "Antenna Type")
                        {
                            mssg = this.selectedAntenna.antennaName;
                        }
                        else if (selectedOption == "RS232 Out")
                        {
                            mssg = this.isRs323Enable ? "Enabled" : "Disabled";

                        }
                        else if (selectedOption == "GPS")
                        {
                            mssg = this.isGPSEnable ? "Enabled" : "Disabled";

                        }
                        else
                        {
                            mssg = "";
                        }
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = mssg;
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["I/O Setting"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        selectedOption = getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex);
                        if (selectedOption == "Antenna Type")
                        {
                            mssg = this.selectedAntenna.antennaName;
                        }
                        else if (selectedOption == "RS232 Out")
                        {
                            mssg = this.isRs323Enable ? "Enabled" : "Disabled";

                        }
                        else if (selectedOption == "GPS")
                        {
                            mssg = this.isGPSEnable ? "Enabled" : "Disabled";

                        }
                        else
                        {
                            mssg = "";
                        }
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = mssg;
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        selectedOption = getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex);
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"][selectedOption], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        break;
                }
                break;
            case "RS232 Out":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["I/O Setting"]["RS232 Out"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["RS232 Out"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["I/O Setting"]["RS232 Out"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["RS232 Out"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        this.isRs323Enable = getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["RS232 Out"], MenuIndex) == "Enabled" ? true : false;
                        //Logger.Log(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["RS232 Out"], MenuIndex) + " " + isRs323Enable);
                        ShowSuccessSplashScreen(string.Format("RS232 Out {0}", getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["RS232 Out"], MenuIndex)));
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.isRs323Enable ? "Enabled" : "Disabled";
                        this.onPressed(ButtonKeyCode.CANCEL, 1);
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        break;
                }

                break;
            case "Line in Level":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["I/O Setting"]["Line in Level"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["Line in Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["I/O Setting"]["Line in Level"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["Line in Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex));
                        break;
                }

                break;
            case "Line out Level":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["I/O Setting"]["Line out Level"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["Line out Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["I/O Setting"]["Line out Level"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["Line out Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex));
                        break;
                }

                break;
            case "Antenna Type":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["I/O Setting"]["Antenna Type"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["Antenna Type"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["I/O Setting"]["Antenna Type"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["Antenna Type"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        this.selectedAntenna.antennaName = getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"]["Antenna Type"], MenuIndex);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.selectedAntenna.antennaName;


                        this.onPressed(ButtonKeyCode.CANCEL, 1);
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["I/O Setting"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);

                        break;
                }

                break;
            case "Extra Alarm Type":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["ALE Setting"]["Extra Alarm Type"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Extra Alarm Type"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["ALE Setting"]["Extra Alarm Type"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"]["Extra Alarm Type"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["ALE Setting"], MenuIndex));
                        break;
                }

                break;
            case "RF Setting":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["RF Setting"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex);
                        string mssg = string.Empty;
                        if (selectedOption == "Clarify Range")
                        {
                            mssg = this.clarifyRange;
                        }

                        else
                        {
                            mssg = "";
                        }
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = mssg;
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["RF Setting"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        selectedOption = getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex);
                        mssg = string.Empty;
                        if (selectedOption == "Clarify Range")
                        {
                            mssg = this.clarifyRange;
                        }

                        else
                        {
                            mssg = "";
                        }
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = mssg;
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        selectedOption = getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex);
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);

                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"][selectedOption], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        break;
                }
                break;
            case "RX Preamp":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["RF Setting"]["RX Preamp"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["RX Preamp"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["RF Setting"]["RX Preamp"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["RX Preamp"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex));
                        break;
                }

                break;
            case "AGC Hang":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["RF Setting"]["AGC Hang"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["AGC Hang"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["RF Setting"]["AGC Hang"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["AGC Hang"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex));
                        break;
                }

                break;
            case "Power Level":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["RF Setting"]["Power Level"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Power Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["RF Setting"]["Power Level"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Power Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex));
                        break;
                }

                break;
            case "Noise Blanker":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["RF Setting"]["Noise Blanker"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Noise Blanker"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["RF Setting"]["Noise Blanker"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Noise Blanker"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex));
                        break;
                }

                break;
            case "Clarify Range":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["RF Setting"]["Clarify Range"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Clarify Range"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["RF Setting"]["Clarify Range"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Clarify Range"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        if (getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Clarify Range"], MenuIndex) == "0Hz")
                        {
                            this.clarifyRange = "0Hz";
                        }
                        else if (getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Clarify Range"], MenuIndex) == "50Hz")
                        {
                            this.clarifyRange = "50Hz";
                        }
                        else if (getFromDictionaryByIndex(BarrettMenu.info["RF Setting"]["Clarify Range"], MenuIndex) == "150Hz")
                        {
                            this.clarifyRange = "150Hz";
                        }
                        else
                        {
                            this.clarifyRange = "1kHz";
                        }
                        //displayControllerBarrett2090.taskCheckedPanel.transform.Find("TaskCheckedMessageText").GetComponent<Text>().text = "Clarify Range Set";
                        //displayControllerBarrett2090.taskCheckedPanel.GetComponentInChildren<SplashScreen>().gameObject.SetActive(true);

                        ShowSuccessSplashScreen(string.Format("Clarify Range set to {0} kHZ", this.clarifyRange));
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.clarifyRange;
                        this.onPressed(ButtonKeyCode.CANCEL, 1);
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["RF Setting"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        break;
                }

                break;
            case "Audio Setting":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Audio Setting"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Audio Setting"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex);
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"][selectedOption], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        break;
                }
                break;
            case "Audio Bandwidth":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Audio Setting"]["Audio Bandwidth"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Audio Bandwidth"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Audio Setting"]["Audio Bandwidth"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Audio Bandwidth"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex));
                        break;
                }

                break;
            case "Noise Reduction":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Audio Setting"]["Noise Reduction"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Noise Reduction"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Audio Setting"]["Noise Reduction"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Noise Reduction"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex));
                        break;
                }

                break;
            case "Line Audio":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Audio Setting"]["Line Audio"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Line Audio"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Audio Setting"]["Line Audio"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Line Audio"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex));
                        break;
                }

                break;
            case "Tx Configuration":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Audio Setting"]["Tx Configuration"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Tx Configuration"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Audio Setting"]["Tx Configuration"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Tx Configuration"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex));
                        break;
                }

                break;
            case "Rx Configuration":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Audio Setting"]["Rx Configuration"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Rx Configuration"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Audio Setting"]["Rx Configuration"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Rx Configuration"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex));
                        break;
                }

                break;
            case "Beep Level":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Audio Setting"]["Beep Level"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Beep Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Audio Setting"]["Beep Level"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"]["Beep Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Audio Setting"], MenuIndex));
                        break;
                }

                break;
            case "Selcall Setting":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Selcall Setting"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Selcall Setting"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        if (
                              getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex) != "Preamble"
                              && getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex) != "Selcall MMSI"
                              && getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex) != "Selcall OEM2"
                              && getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex) != "Selcall OEM1"
                              && getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex) != "Selcall INT2"
                              && getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex) != "Selcall INT1"
                        )
                        {
                            string selectedOption = getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex);
                            //MenuIndex = 0;
                            Navigator.Push(selectedOption);
                            menuIndexStack.Push(MenuIndex);

                            if (selectedOption == "TXCVR Lock")
                            {
                                displayControllerBarrett2090.TXCVRPanel.SetActive(true);
                                displayControllerBarrett2090.TXCVRPanel.transform.Find("PinNumberPanel").gameObject.SetActive(false);
                                displayControllerBarrett2090.TXCVRPanel.transform.Find("TransmitPanel").gameObject.SetActive(false);
                                displayControllerBarrett2090.TXCVRPanel.transform.Find("Header").GetComponent<Text>().text = Navigator.Peek().ToString();
                                displayControllerBarrett2090.TXCVRPanel.transform.Find("Footer").GetComponent<Text>().text = "Press Call to Continue";
                                displayControllerBarrett2090.TXCVRPanel.transform.Find("AddressBookEntry").GetComponent<Text>().text = "Address Book Empty";
                                displayControllerBarrett2090.TXCVRPanel.transform.Find("IDTag").GetComponent<Text>().text = "ID: ";
                                displayControllerBarrett2090.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>().text = this.adressBook[this.addressBookIndex].ID.ToString();
                            }
                            else
                            {
                                MenuIndex = 0;
                                displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"][selectedOption], MenuIndex));
                            }
                        }

                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        break;
                }
                break;
            case "TXCVR Lock":
                TXCVRSetting.TxCVRLock(this, buttonID);
                break;


            case "Self IDs":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Selcall Setting"]["Self IDs"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"]["Self IDs"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Selcall Setting"]["Self IDs"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"]["Self IDs"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex));
                        break;
                }

                break;
            case "OEM Privacy Key":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Selcall Setting"]["OEM Privacy Key"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"]["OEM Privacy Key"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Selcall Setting"]["OEM Privacy Key"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"]["OEM Privacy Key"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex));
                        break;
                }

                break;
            case "Set Audio In Tx":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Selcall Setting"]["Set Audio In Tx"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"]["Set Audio In Tx"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Selcall Setting"]["Set Audio In Tx"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"]["Set Audio In Tx"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex));
                        break;
                }

                break;
            case "Selcall Alarm":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Selcall Setting"]["Selcall Alarm"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"]["Selcall Alarm"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Selcall Setting"]["Selcall Alarm"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"]["Selcall Alarm"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Selcall Setting"], MenuIndex));
                        break;
                }

                break;
            case "Mute Setting":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Mute Setting"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Mute Setting"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"], MenuIndex);
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"][selectedOption], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        break;
                }
                break;
            case "Syllabic Mute Sensitivity":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Mute Setting"]["Syllabic Mute Sensitivity"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"]["Syllabic Mute Sensitivity"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Mute Setting"]["Syllabic Mute Sensitivity"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"]["Syllabic Mute Sensitivity"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"], MenuIndex));
                        break;
                }

                break;
            case "Signal Strength Mute Level":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Mute Setting"]["Signal Strength Mute Level"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"]["Signal Strength Mute Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Mute Setting"]["Signal Strength Mute Level"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"]["Signal Strength Mute Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Mute Setting"], MenuIndex));
                        break;
                }

                break;
            case "Scan Setting":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Scan Setting"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Scan Setting"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"], MenuIndex);
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"][selectedOption], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        break;
                }
                break;
            case "Scan Rate":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Scan Setting"]["Scan Rate"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"]["Scan Rate"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Scan Setting"]["Scan Rate"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"]["Scan Rate"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"], MenuIndex));
                        break;
                }

                break;
            case "Scan Select":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Scan Setting"]["Scan Select"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"]["Scan Select"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Scan Setting"]["Scan Select"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"]["Scan Select"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"], MenuIndex));
                        break;
                }

                break;
            case "Scan Resume Time":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.info["Scan Setting"]["Scan Resume Time"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"]["Scan Resume Time"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.info["Scan Setting"]["Scan Resume Time"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"]["Scan Resume Time"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info["Scan Setting"], MenuIndex));
                        break;
                }

                break;
            case "Scan Tables":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = 7;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        BarrettMenu.scanListIndexSelected = MenuIndex;
                        displayControllerBarrett2090.setMenuText(BarrettMenu.scanListNames[MenuIndex].tableLabel);
                        displayControllerBarrett2090.setMenuText(BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count == 0 ? "Empty" : BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count + " Entries";
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > 7)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        BarrettMenu.scanListIndexSelected = MenuIndex;
                        displayControllerBarrett2090.setMenuText(BarrettMenu.scanListNames[MenuIndex].tableLabel);
                        displayControllerBarrett2090.setMenuText(BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count == 0 ? "Empty" : BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count + " Entries";
                        break;
                    case ButtonKeyCode.OK:
                        string selectedOption = "ScanListAction";
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);
                        MenuIndex = 0;
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        if (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count == 0)
                        {
                            MenuIndex = 1;
                            displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[1]);
                        }
                        else
                        {
                            displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[0]);
                        }

                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.info, MenuIndex));
                        break;
                }
                break;
            case "ScanListAction":
                switch (buttonID)
                {
                    //search entry
                    //add entry
                    //change label
                    case ButtonKeyCode.UP_ARROW:

                        MenuIndex--;
                        if (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count == 0 && MenuIndex == 0)
                        {
                            MenuIndex--;
                        }
                        if (MenuIndex < 0)
                        {
                            MenuIndex = 2;
                        }

                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[MenuIndex]);
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > 2)
                        {
                            MenuIndex = 0;
                        }
                        if (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count == 0 && MenuIndex == 0)
                        {
                            MenuIndex++;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[MenuIndex]);
                        break;
                    case ButtonKeyCode.OK:


                        if (BarrettMenu.scanListOptions[MenuIndex].Equals("Search Entry"))
                        {
                            string selectedOption = "ScanList_Search";
                            Navigator.Push(selectedOption);
                            menuIndexStack.Push(MenuIndex);
                            MenuIndex = 0;
                            displayControllerBarrett2090.setMenuText(selectedOption);
                            displayControllerBarrett2090.ScanTablePanel.SetActive(true);
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel;
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[0].channelNumber + "  Entry: " + (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count);
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[0].tx + " kHz";
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList[0].channelLabel;
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("FooterText").GetComponent<Text>().text = "Enter to Edit Scan Table Entry";
                        }

                        else if (BarrettMenu.scanListOptions[MenuIndex].Equals("Add Entry"))
                        {
                            string selectedOption = "ScanList_AddEntry";
                            Navigator.Push(selectedOption);
                            menuIndexStack.Push(MenuIndex);
                            MenuIndex = 0;
                            displayControllerBarrett2090.setMenuText(selectedOption);
                            displayControllerBarrett2090.ScanTablePanel.SetActive(true);
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("HeadingText").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel;
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("ChannelEntryText").GetComponent<Text>().text = "Channel: " + channelList[MenuIndex].channelNumber + "  Entry: " + (BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].scanList.Count);
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("FrequencyText").GetComponent<Text>().text = "Frequency: " + channelList[MenuIndex].tx + " kHz";
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("LabelText").GetComponent<Text>().text = "Label: " + channelList[MenuIndex].channelLabel;
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("FooterText").GetComponent<Text>().text = "Select Channel to Scan";
                        }
                        else
                        {
                            string selectedOption = "ScanList_ChangeLabel";
                            Navigator.Push("ScanList_ChangeLabel");
                            Navigator.Push(selectedOption);
                            menuIndexStack.Push(MenuIndex);
                            MenuIndex = 0;
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("ChangeLabelPanel").gameObject.SetActive(true);
                            displayControllerBarrett2090.ScanTablePanel.SetActive(true);
                            displayControllerBarrett2090.ScanTablePanel.transform.Find("ChangeLabelPanel").Find("LabelText").GetComponent<Text>().text = BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel;
                        }

                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(BarrettMenu.scanListOptions[MenuIndex]);
                        break;
                }
                break;
            case "ScanList_AddEntry":
                ScanTablesSetting.AddEntry(this, buttonID);
                break;
            case "ScanList_Search":
                ScanTablesSetting.SearchEntry(this, buttonID);
                break;
            case "ScanList_ChangeLabel":
                this.keyboardType = KeyboardType.CHARACTER;
                ScanTablesSetting.ChangeScanTableLabel(this, buttonID);
                break;

            case "Standart Menu":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress.Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress, MenuIndex));

                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress.Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress, MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.menuShortPress, MenuIndex);
                        if (selectedOption == "Identification")
                        {
                            //Logger.Log("Open Identification Panel");
                            displayControllerBarrett2090.IdentificationPanel.SetActive(true);
                            identificationPageCounter = 0;
                            IdentificationPageUpdate(identificationPageCounter);
                            return;
                        }
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);                        
                        if (selectedOption == "Display Option")
                        {
                            displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.backlightLevel.ToString();
                        }
                        else if (selectedOption == "Address Book")
                        {
                            displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                            displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.adressBook.Count + " Entries";
                        }                        
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress[selectedOption], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        if (displayControllerBarrett2090.IdentificationPanel.activeSelf)
                        {
                            //Logger.Log("Close Identification Panel");
                            identificationPageCounter = 0;
                            displayControllerBarrett2090.IdentificationPanel.SetActive(false);
                            return;
                        }
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText("HOME");
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        break;
                    case ButtonKeyCode.NUM_1:
                        if (displayControllerBarrett2090.IdentificationPanel.activeSelf)
                        {
                            //Logger.Log("Left key in ID Pagination");
                            identificationPageCounter--;
                            identificationPageCounter = identificationPageCounter < 0 ? 0 : identificationPageCounter;
                            IdentificationPageUpdate(identificationPageCounter);
                        }
                        break;
                    case ButtonKeyCode.NUM_3:
                        if (displayControllerBarrett2090.IdentificationPanel.activeSelf)
                        {
                            //Logger.Log("Right key in ID Pagination");
                            identificationPageCounter++;
                            identificationPageCounter = identificationPageCounter > 3 ? 3 : identificationPageCounter;
                            IdentificationPageUpdate(identificationPageCounter);
                        }
                        break;
                }
                break;
            case "Identification":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Identification"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Identification"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Identification"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Identification"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress, MenuIndex));
                        break;
                }

                break;
            case "Address Book":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Address Book"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"], MenuIndex);
                        string msg = "";
                        if (selectedOption == "Selcall ID Book")
                        {
                            msg = this.adressBook.Count == 0 ? "Empty" : this.adressBook.Count + " Entries";
                        }
                        else if (selectedOption == "Phone Book")
                        {
                            msg = this.phoneBook.Count == 0 ? "Empty" : this.phoneBook.Count + " Entries";
                        }

                        else
                        {
                            msg = isAutoFill ? "Enabled" : "Disabled";
                        }
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = msg;
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Address Book"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        selectedOption = getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"], MenuIndex);
                        msg = "";
                        if (selectedOption == "Selcall ID Book")
                        {
                            msg = this.adressBook.Count == 0 ? "Empty" : this.adressBook.Count + " Entries";
                        }
                        else if (selectedOption == "Phone Book")
                        {
                            msg = this.phoneBook.Count == 0 ? "Empty" : this.phoneBook.Count + " Entries";
                        }

                        else
                        {
                            msg = isAutoFill ? "Enabled" : "Disabled";
                        }
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = msg;
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        selectedOption = getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"], MenuIndex);
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"][selectedOption], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress, MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        break;
                }
                break;
            case "Selcall ID Book":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Address Book"]["Selcall ID Book"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Selcall ID Book"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Address Book"]["Selcall ID Book"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Selcall ID Book"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        if (getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Selcall ID Book"], MenuIndex) == "Add Entry")
                        {
                            displayControllerBarrett2090.SellCallIdBook.SetActive(true);
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("Header").GetComponent<Text>().text = Navigator.Peek().ToString();
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("Footer").GetComponent<Text>().text = "Input Name of ID Book Entry";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("NameTag").GetComponent<Text>().text = "Name: ";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = "";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SelCallTag").GetComponent<Text>().text = "Selcall ID: ";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SellCallID").GetComponent<Text>().text = "";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("LinkedID").Find("LinkedTag").GetComponent<Text>().text = "Linked To Self ID: ";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>().text = "";
                            Navigator.Push("SellCallAddEntry");
                        }
                        else if (getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Selcall ID Book"], MenuIndex) == "Search Entries")
                        {
                            displayControllerBarrett2090.SellCallIdBook.SetActive(true);
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("Header").GetComponent<Text>().text = Navigator.Peek().ToString();
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("Footer").GetComponent<Text>().text = "Press Enter to edit Selcall ID";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("InLineArrows").gameObject.SetActive(true);
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("NameTag").GetComponent<Text>().text = "Name: ";
                            string sellCallIdList = adressBook[adressBook.Count - 1].Label.ToString();
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = sellCallIdList;
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SelCallTag").GetComponent<Text>().text = "Selcall ID: ";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SellCallID").GetComponent<Text>().text = adressBook[adressBook.Count - 1].ID.ToString();
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("LinkedID").Find("LinkedTag").GetComponent<Text>().text = "Linked To Self ID: ";
                            displayControllerBarrett2090.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>().text = adressBook[adressBook.Count - 1].isLinked == true ? "Yes" : "NO";
                            Navigator.Push("SellCallSearchEntry");
                        }
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        break;
                }

                break;
            case "Autofill Book":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Address Book"]["Autofill Book"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Autofill Book"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Address Book"]["Autofill Book"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Autofill Book"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        break;
                    case ButtonKeyCode.OK:
                        isAutoFill = getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Autofill Book"], MenuIndex) == "Enabled" ? true : false;
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = isAutoFill == true ? "Enabled" : "Disabled";
                        this.onPressed(ButtonKeyCode.CANCEL, 0);
                        break;
                }

                break;
            case "Phone Book":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Address Book"]["Phone Book"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Phone Book"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Address Book"]["Phone Book"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Phone Book"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        break;

                    case ButtonKeyCode.OK:
                        if (getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Phone Book"], MenuIndex) == "Add Entry")
                        {
                            displayControllerBarrett2090.PhoneBook.SetActive(true);
                            displayControllerBarrett2090.PhoneBook.transform.Find("Header").GetComponent<Text>().text = Navigator.Peek().ToString();
                            displayControllerBarrett2090.PhoneBook.transform.Find("Footer").GetComponent<Text>().text = "Input Name of Phone Book Entry";
                            displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("NameTag").GetComponent<Text>().text = "Name: ";
                            displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = "";
                            displayControllerBarrett2090.PhoneBook.transform.Find("PhonePanel").Find("PhoneTag").GetComponent<Text>().text = "Ph: ";
                            displayControllerBarrett2090.PhoneBook.transform.Find("PhonePanel").Find("PhoneID").GetComponent<Text>().text = "";
                            Navigator.Push("PhoneBookAddEntry");
                        }
                        else if (getFromDictionaryByIndex(BarrettMenu.menuShortPress["Address Book"]["Phone Book"], MenuIndex) == "Search Entry")
                        {
                            displayControllerBarrett2090.PhoneBook.SetActive(true);
                            displayControllerBarrett2090.PhoneBook.transform.Find("Header").GetComponent<Text>().text = Navigator.Peek().ToString();
                            displayControllerBarrett2090.PhoneBook.transform.Find("Footer").GetComponent<Text>().text = "Press Enter to edit Telnumber";
                            displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("InLineArrows").gameObject.SetActive(true);
                            displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("NameTag").GetComponent<Text>().text = "Name: ";
                            string PhoneBookList = phoneBook[phoneBook.Count - 1].Label.ToString();
                            displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = PhoneBookList;
                            displayControllerBarrett2090.PhoneBook.transform.Find("PhonePanel").Find("PhoneTag").GetComponent<Text>().text = "Ph: ";
                            displayControllerBarrett2090.PhoneBook.transform.Find("PhonePanel").Find("PhoneID").GetComponent<Text>().text = phoneBook[phoneBook.Count - 1].ID.ToString();
                            Navigator.Push("PhoneBookSearchEntry");
                        }
                        break;
                }

                break;
            case "Call History":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Call History"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Call History"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Call History"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Call History"], MenuIndex));
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        Logger.Log(Navigator.Peek());
                        if (Navigator.Peek() != "HOME")
                        {
                            displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress, MenuIndex));
                        }

                        break;
                }

                break;
            case "Display Option":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Display Option"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"], MenuIndex) == "Backlight Level" ? this.backlightLevel.ToString() : this.GetBackLightTimeout();
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Display Option"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"], MenuIndex) == "Backlight Level" ? this.backlightLevel.ToString() : this.GetBackLightTimeout();
                        break;
                    case ButtonKeyCode.OK:
                        string selectedOption = getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"], MenuIndex);
                        MenuIndex = 0;
                        Navigator.Push(selectedOption);
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"][selectedOption], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress, MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(false);
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(true);
                        break;
                }
                break;
            case "Backlight Level":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Display Option"]["Backlight Level"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"]["Backlight Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Display Option"]["Backlight Level"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"]["Backlight Level"], MenuIndex));
                        break;
                    case ButtonKeyCode.OK:
                        if (getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"]["Backlight Level"], MenuIndex) == "High")
                        {
                            this.backlightLevel = BacklightLavel.HIGH;
                        }
                        else if (getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"]["Backlight Level"], MenuIndex) == "Medium")
                        {
                            this.backlightLevel = BacklightLavel.MEDIUM;
                        }
                        else
                        {
                            this.backlightLevel = BacklightLavel.LOW;
                        }
                        displayControllerBarrett2090.ChangeBackLight(this.backlightLevel);
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.backlightLevel.ToString();
                        break;
                    case ButtonKeyCode.CANCEL:

                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        break;
                }

                break;
            case "Backlight Time Out":

                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        MenuIndex--;
                        if (MenuIndex < 0)
                        {
                            MenuIndex = BarrettMenu.menuShortPress["Display Option"]["Backlight Time Out"].Count - 1;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"]["Backlight Time Out"], MenuIndex));
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        MenuIndex++;
                        if (MenuIndex > BarrettMenu.menuShortPress["Display Option"]["Backlight Time Out"].Count - 1)
                        {
                            MenuIndex = 0;
                        }
                        menuIndexStack.Pop();
                        menuIndexStack.Push(MenuIndex);
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"]["Backlight Time Out"], MenuIndex));
                        break;

                    case ButtonKeyCode.OK:
                        if (getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"]["Backlight Time Out"], MenuIndex) == "Always On")
                        {
                            this.backlightTimeOut = BacklightTimeout.ALWAYS_ON;
                        }
                        else if (getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"]["Backlight Time Out"], MenuIndex) == "Long Time Out")
                        {
                            this.backlightTimeOut = BacklightTimeout.LONG_TIMEOUT;
                        }
                        else
                        {
                            this.backlightTimeOut = BacklightTimeout.SHORT_TIMEOUT;
                        }
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = this.GetBackLightTimeout();
                        break;

                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        menuIndexStack.Pop();
                        MenuIndex = menuIndexStack.Peek();
                        displayControllerBarrett2090.setMenuText(getFromDictionaryByIndex(BarrettMenu.menuShortPress["Display Option"], MenuIndex));
                        displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").gameObject.SetActive(true);
                        displayControllerBarrett2090.MenuPanel.transform.Find("Arrows").gameObject.SetActive(false);
                        break;
                }

                break;
            case "Send Call":
                switch (buttonID)
                {
                    case ButtonKeyCode.UP_ARROW:
                        BarrettMenu.CallIndex++;
                        if (BarrettMenu.CallIndex >= BarrettMenu.CallText.Length)
                        {
                            BarrettMenu.CallIndex = 0;
                        }
                        displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                        break;
                    case ButtonKeyCode.DOWN_ARROW:
                        BarrettMenu.CallIndex--;
                        if (BarrettMenu.CallIndex < 0)
                        {
                            BarrettMenu.CallIndex = BarrettMenu.CallText.Length - 1;
                        }
                        displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                        break;
                    case ButtonKeyCode.CALL:
                        Navigator.Push(BarrettMenu.CallText[BarrettMenu.CallIndex]);

                        //Display
                        displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = BarrettMenu.CallText[BarrettMenu.CallIndex].ToString(); //Header Text
                        displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(true); //ID panel true
                        displayControllerBarrett2090.CallPanel.transform.Find("ID").GetComponent<Text>().text = "ID:"; //ID Text
                        displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(false); //Call Text
                        displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(true); // Call Id panel
                        displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text= adressBook[0].ID.ToString(); //call id
                        displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                        displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Press Enter For AddressBook"; //set Contact label
                        displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(false); //Arrows
                        displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(true); //Info Text panel
                        displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Info text
                        break;
                    case ButtonKeyCode.CANCEL:
                        Navigator.Pop();
                        this.displayControllerBarrett2090.CallPanel.gameObject.SetActive(false);
                        displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call";
                        break;

                }
                break;

            case "SelCall":
                CallSettings.setSelCallId(this, buttonID);
                break;
            case "TeleCall":
                CallSettings.setTeleCallId(this, buttonID);
                break;
            case "Telephone Number":
                CallSettings.TeleCallInput(this, buttonID);
                break;

            case "Hang Up":
                CallSettings.setHangupCallId(this, buttonID);
                break;

            case "Page Call":
                CallSettings.setPageCallId(this, buttonID);
                break;
            case "Page_Call_Message":
                this.keyboardType = KeyboardType.CHARACTER;
                CallSettings.PageCallInput(this, buttonID);
                break;
            case "GPS Request":
                CallSettings.setGpsRequestCallId(this, buttonID);
                break;
            case "Status Request":
                CallSettings.setStatusRequestCallId(this, buttonID);
                break;
            case "Secure Call":
                CallSettings.setSecureCallId(this, buttonID);
                break;
            case "Beacon":
                CallSettings.setBeaconCallId(this, buttonID);
                break;
            case "SellCallAddEntry":
                SellCallIDBook.SellCallAddEntrySetting(this, buttonID);
                break;
            case "PhoneBookAddEntry":
                PhoneBookAddEntry.PhoneBookAddEntrySetting(this, buttonID);
                break;
            case "Hopping Pin":
                HoppingSetting.SetHoppingPin(this, buttonID);
                break;
            case "SellCallSearchEntry":
                SellCallIDBook.SellCallSearchEntrySetting(this, buttonID);
                break;
            case "PhoneBookSearchEntry":
                PhoneBookAddEntry.PhoneBookSearchEntrySetting(this, buttonID);
                break;
        }

        if (Navigator.Peek() == "HOME")
        {
            displayControllerBarrett2090.MenuPanel.SetActive(false);
        }
        else if (Navigator.Peek() == "ScanListAction")
        {
            displayControllerBarrett2090.setMenuPanelHeader(BarrettMenu.scanListNames[BarrettMenu.scanListIndexSelected].tableLabel);
        }
        else
        {
            displayControllerBarrett2090.setMenuPanelHeader(Navigator.Peek().ToString());
        }
    }


    public string getFromDictionaryByIndex(Dictionary<string, dynamic> dict, int indexNum)
    {
        int ind = 0;
        foreach (var kvp in dict)
        {
            if (ind == indexNum)
            {
                return kvp.Key;
            }
            ind++;
        }
        return "";
    }
    // End channel Select
    // Barrett 2090 Property
    public string version { set; get; } = "Version 12.23.1";
    public double power { set; get; } = 12; // unit v
    public double watt { set; get; } = 100; // unit W
    public List<Channel> channelList = new List<Channel>();
    public List<Contact> adressBook = new List<Contact>();
    public List<Contact> phoneBook = new List<Contact>();
 
    public List<String> channelLabelList = new List<String>();
    public int channelLabelIndex { set; get; } = 0;
    public int modulationIndex { set; get; } = 0;
    public int powerIndex { set; get; } = 0;
    public int callFormateIndex { set; get; } = 0;

    public int addressBookIndex { set; get; } = 0;
    public int phoneBookIndex { set; get; } = 0;
    public int TxCVRIndex { set; get; } = 0;
    public bool isPowerOn { set; get; } = false;
    public bool isPowerConnected = false;
    public bool isBaseStation = false;
    public PowerMode powerMode = PowerMode.MANPACK_LOW;
    public PowerSource powerSource;
    public StationType stationType = StationType.MANPACK;
    public string clarifyRange = "0Hz";
    public BacklightLavel backlightLevel = BacklightLavel.MEDIUM;
    public BacklightTimeout backlightTimeOut = BacklightTimeout.SHORT_TIMEOUT;
    public int numbPadLength = 6;
    public bool isAutoFill = false;
    public bool isRs323Enable = false;

    private int identificationPageCounter = 0;
    public double powerOutput
    {
        get
        {
            if (powerMode == PowerMode.MANPACK_LOW || powerMode == PowerMode.BASESTATION_LOW || powerMode == PowerMode.VEHICLE_LOW)
            {
                return 10;
            }
            else if (powerMode == PowerMode.MANPACK_HIGH || powerMode == PowerMode.BASESTATION_MEDIUM || powerMode == PowerMode.VEHICLE_MEDIUM)
            {
                return 30;
            }
            else
            {
                return 100;
            }
        }

        set { }

    }
    public double voltage
    {
        get
        {
            if (stationType == StationType.MANPACK)
            {
                return 16.8;
            }
            else if (stationType == StationType.VEHICLE)
            {
                return 12;
            }
            else
            {
                Random rnd = new Random();
                return rnd.Next(88, 256);
            }
        }
        set { }
    }
    public AudioSource radioNoise;
    public bool isAudioMuteOn = true;
    public bool isSelectiveCallMuteOn = true;
    public bool isSslMuteOn = true;

    public Channel activeChannel = new Channel("0001", 000000.00, 000000.00, "Undefinite", Modulation.USB, ChannelPowerMode.HIGH, CallFormat.INTERNATIONAL);// new object for channel switching
    public int activeChannelIndex { set; get; } = 0; // Active channel index in channel switching

    public float clicked = 0;
    public float clicktime = 0;
    public float clickdelay = 0.1f;
    //DateTime time & Alarm
    public DateTime radioDateTime = DateTime.Now;
    public bool isAlarmOn = false;
    public bool isGPSEnable = false;
    public bool isALEEnable = false;
    public bool isHoppingEnable = false;
    public int hoppingPin = 0;
    public DateTime alarmTime = DateTime.Now.AddSeconds(15);
    public KeyboardType keyboardType = KeyboardType.NUMERICAL;
    public bool isCapsLockOn = false;
    public Antennas selectedAntenna = new Antennas(RADIO.BARRET_2090, AntennaType.BASESTATION_ANTENNA, DirectionType.BIDIRECTIONAL, 30, 6, "Base Status");
    public int channelAttributeCounter = 0;

    private void Update()
    {
        if (time >= interval)
        {
            this.radioDateTime = this.radioDateTime.AddSeconds(1);
            time = 0;
        }
        time += Time.deltaTime * GLOBALTAG.TIMESCALE;
        if (!isAlarmOn && radioDateTime == alarmTime)
        {
            // Logger.Log(this, "Alarm On");
            StartCoroutine(AlarmOff(2.0f));
        }
        //Backlight Timeout
        if (Time.time > LightOffTimeOut && this.backlightTimeOut != BacklightTimeout.ALWAYS_ON)
        {
            displayControllerBarrett2090.PowerOffDisplayLight();
        }
        //Info Change in Home Screen 
        if (endSwitch < Time.time)
        {
            InfoTextSwitcher();
        }

    }
    IEnumerator AlarmOff(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // print("Alarm off " + Time.time);
    }
    string GetBackLightTimeout()
    {
        if (backlightTimeOut == BacklightTimeout.ALWAYS_ON)
        {
            return "Always On";
        }
        else if (backlightTimeOut == BacklightTimeout.LONG_TIMEOUT)
        {
            return "Long Timeout";
        }
        else
        {
            return "Short Timeout";
        }
    }
    private float endSwitch = 0;
    int sec = 0;
    public void InfoTextSwitcher()
    {
        int delay = UnityEngine.Random.Range(2, 5);
        endSwitch = Time.time + delay;
        if (sec == 1 && this.isGPSEnable)
        {
            //GPS Info
            displayControllerBarrett2090.HomePanel.transform.Find("GPSPanel").Find("GPS").GetComponent<Text>().text = "GPS Unavailable";
        }
        else if (sec == 2)
        {
            //Channel Label
            displayControllerBarrett2090.HomePanel.transform.Find("GPSPanel").Find("GPS").GetComponent<Text>().text = this.activeChannel.channelLabel;

        }
        sec = UnityEngine.Random.Range(0, 3);
    }

    private int GetPowerOutput(ChannelPowerMode channelPowerMode)
    {
        if (this.stationType == StationType.BASESTATION && this.stationType == StationType.VEHICLE)
        {
            if (channelPowerMode == ChannelPowerMode.HIGH)
            {
                return 100;
            }
            else if (channelPowerMode == ChannelPowerMode.MEDIUM)
            {
                return 30;
            }
            else
            {
                return 10;
            }

        }
        else
        {
            if (channelPowerMode == ChannelPowerMode.HIGH)
            {
                return 30;
            }
            else
            {
                return 10;

            }
        }
    }

    private void ShowSuccessSplashScreen(string message)
    {
        displayControllerBarrett2090.taskCheckedPanel.transform.Find("TaskCheckedMessageText").GetComponent<Text>().text = message;
        displayControllerBarrett2090.taskCheckedPanel.GetComponentInChildren<SplashScreen>().gameObject.SetActive(true);
    }

    private void OpenChannelAttributesPrompt()
    {
        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = "RX Freq:";
        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = "TX Freq:";
        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = "Mode:";
        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").GetComponent<Text>().text = "Power:";
        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.rx);
        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.tx);
        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.modulation);
        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").GetComponent<Text>().text = string.Format("{0}W", GetPowerOutput(this.activeChannel.power));

        displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").gameObject.SetActive(true);
        channelAttributeCounter = 0;
    }

    private void ChannelAttributesDownButtonPressed()
    {
        channelAttributeCounter = channelAttributeCounter < 0 ? 0 : channelAttributeCounter;
        channelAttributeCounter++;
        switch (channelAttributeCounter)
        {
            case 1:
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").GetComponent<Text>().text = "RX Freq:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = "TX Freq:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = "Mode:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = "Power:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").GetComponent<Text>().text = "Antenna:";

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.rx);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.tx);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.modulation);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3_Data").GetComponent<Text>().text = string.Format("{0}W", GetPowerOutput(this.activeChannel.power));
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").GetComponent<Text>().text = string.Format("{0}", this.selectedAntenna.antennaName);

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Header").gameObject.SetActive(false);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").gameObject.SetActive(true);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").gameObject.SetActive(true);

                break;
            case 2:
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").GetComponent<Text>().text = "TX Freq:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = "Mode:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = "Power:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = "Antenna:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").GetComponent<Text>().text = "SC Format:";

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.tx);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.modulation);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2_Data").GetComponent<Text>().text = string.Format("{0}W", GetPowerOutput(this.activeChannel.power));
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3_Data").GetComponent<Text>().text = string.Format("{0}", this.selectedAntenna.antennaName);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.callFormat);
                break;
            case 3:
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").GetComponent<Text>().text = "Mode:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = "Power:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = "Antenna:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = "SC Format:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").GetComponent<Text>().text = string.Empty;

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.modulation);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1_Data").GetComponent<Text>().text = string.Format("{0}W", GetPowerOutput(this.activeChannel.power));
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2_Data").GetComponent<Text>().text = string.Format("{0}", this.selectedAntenna.antennaName);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.callFormat);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").GetComponent<Text>().text = string.Empty;
                break;
            default:
                break;
        }
        return;
    }

    private void ChannelAttributesUpButtonPressed()
    {
        channelAttributeCounter = channelAttributeCounter > 3 ? 3 : channelAttributeCounter;
        channelAttributeCounter--;
        switch (channelAttributeCounter)
        {
            case 0:
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = "RX Freq:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = "TX Freq:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = "Mode:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").GetComponent<Text>().text = "Power:";

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.rx);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.tx);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.modulation);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").GetComponent<Text>().text = string.Format("{0}W", GetPowerOutput(this.activeChannel.power));

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Header").gameObject.SetActive(true);

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").gameObject.SetActive(true);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").gameObject.SetActive(true);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").gameObject.SetActive(false);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").gameObject.SetActive(false);
                break;
            case 1:
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").GetComponent<Text>().text = "RX Freq:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = "TX Freq:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = "Mode:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = "Power:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").GetComponent<Text>().text = "Antenna:";

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.rx);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.tx);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.modulation);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3_Data").GetComponent<Text>().text = string.Format("{0}W", GetPowerOutput(this.activeChannel.power));
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").GetComponent<Text>().text = string.Format("{0}", this.selectedAntenna.antennaName);

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").gameObject.SetActive(true);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").gameObject.SetActive(true);
                break;
            case 2:
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").GetComponent<Text>().text = "TX Freq:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = "Mode:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = "Power:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = "Antenna:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").GetComponent<Text>().text = "SC Format:";

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").GetComponent<Text>().text = string.Format("{0:00000.0} kHz", this.activeChannel.tx);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.modulation);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2_Data").GetComponent<Text>().text = string.Format("{0}W", GetPowerOutput(this.activeChannel.power));
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3_Data").GetComponent<Text>().text = string.Format("{0}", this.selectedAntenna.antennaName);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.callFormat);
                break;
            case 3:
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0").GetComponent<Text>().text = "Mode:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1").GetComponent<Text>().text = "Power:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2").GetComponent<Text>().text = "Antenna:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3").GetComponent<Text>().text = "SC Format:";
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4").GetComponent<Text>().text = string.Empty;

                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label0_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.modulation);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label1_Data").GetComponent<Text>().text = string.Format("{0}W", GetPowerOutput(this.activeChannel.power));
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label2_Data").GetComponent<Text>().text = string.Format("{0}", this.selectedAntenna.antennaName);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label3_Data").GetComponent<Text>().text = string.Format("{0}", this.activeChannel.callFormat);
                displayControllerBarrett2090.HomePanel.transform.Find("ChannelAttributePanel").Find("Label4_Data").GetComponent<Text>().text = string.Empty;
                break;
            default:
                break;
        }
    }
    private void IdentificationPageUpdate(int pageCounter)
    {
        //Logger.Log("Update Id page " + pageCounter);        
        displayControllerBarrett2090.IdentificationPanel.transform.Find("Header").GetComponent<Text>().text = string.Format("< Identification Page {0} >", pageCounter + 1);
        switch (identificationPageCounter)
        {
            //This text are currently held by placeholders dependent on other functionalities. These will be replaced after the dependencies are met.
            case 0:
                displayControllerBarrett2090.IdentificationPanel.transform.Find("info").GetComponent<Text>().text = string.Format("<b>TxcvrType:</b> 2090\n<b>S/N:</b> 294967295\n<b>Options:</b> No Options Enabled");
                break;
            case 1:
                displayControllerBarrett2090.IdentificationPanel.transform.Find("info").GetComponent<Text>().text = string.Format("<b>Software Version:</b> 2.16\n<b>DSP Version:</b> 2.14\n<b>Core Version:</b> 1.21");
                break;
            case 2:
                displayControllerBarrett2090.IdentificationPanel.transform.Find("info").GetComponent<Text>().text = string.Format("<b>Battery RX:</b> 14.5\n<b>Battery TX:</b> 14.5\n<b>PA Temperature:</b> 30\x00B0");
                break;
            case 3:
                displayControllerBarrett2090.IdentificationPanel.transform.Find("info").GetComponent<Text>().text = string.Format("<b>Iternal Modem:</b> Disabled\n");
                break;
            default:
                break;
        }
    }
}

