using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayControllerBarrett2090 : MonoBehaviour
{

    // Display Color Code 
    //Backlight High
    public Color32 HighColor = new Color32(219, 199, 199, 255);
    //Backlight Medium
    public Color32 MediumColor = new Color32(200, 200, 200, 255);
    //Backlight Low
    public Color32 LowColor = new Color32(150, 150, 150, 255);
    public Color32 LightOffColor = new Color32(100, 100, 100, 255);

    public Color32 BaretDisplayColor = new Color32(255, 255, 255, 255);

    public Text LeftUpConrer;
    public Text Middle;
    public Text TimeText;
    public Text ChannelNumber;
    public Text selCallNumber;
    public Text RxNumber;
    Barrett2090 barrett2090;
    public GameObject FrequencyInputPanel;
    public SplashScreen splashScreen;
    public GameObject ChannelInputPanel;
    public GameObject IdentificationPanel;
    public GameObject CallPanel;
    public GameObject HomePanel;
    public GameObject ScanTablePanel;
    public GameObject MenuPanel;
    public GameObject SellCallIdBook;
    public Text ContactID;
    public GameObject ALEPanel;
    [HideInInspector]
    public GameObject ProtectedPanel;
    public GameObject PhoneBook;
    public GameObject ReceiverTunePanel;
    public GameObject TXCVRPanel;

    private void Start()
    {
        this.gameObject.SetActive(false);
        this.ChannelInputPanel.SetActive(true);
        this.CallPanel.SetActive(false);
        this.FrequencyInputPanel.SetActive(false);
        this.IdentificationPanel.SetActive(false);
        this.ScanTablePanel.SetActive(false);
        this.MenuPanel.SetActive(false);
        this.ALEPanel.SetActive(false);
        this.ProtectedPanel = transform.Find("ProtectedMode").gameObject;
        this.ReceiverTunePanel.SetActive(false);
        this.TXCVRPanel.SetActive(false);
        // Splash Screen
        splashScreen = this.GetComponentInChildren<SplashScreen>();
        this.ProtectedPanel.SetActive(false);

        //checked panel
        this.taskCheckedPanel = transform.Find("TaskCheckedPanel").gameObject;
        this.taskCheckedPanel.SetActive(false);
    }

    public void setMenuText(string mssg)
    {
        Middle.text = mssg;
    }
    public void SetLeftCorner(string mssg)
    {
        LeftUpConrer.text = mssg;
    }
    private void Update()
    {
        // Update Time 
        TimeText.text = System.DateTime.Now.ToString("HH:mm");
    }
    public void PowerOnOff(bool status)
    {
        this.gameObject.SetActive(status);
    }
    public void SetChannelNumber(char input, int index)
    {
        char[] channeltext = ChannelNumber.text.ToCharArray();
        channeltext[index] = input;
        ChannelNumber.text = new string(channeltext);
    }

    public void SetRxNumber(char input, int index)
    {
        char[] channeltext = RxNumber.text.ToCharArray();

        channeltext[index] = input;
        for (int i = index + 1; i < channeltext.Length; i++)
        {
            if (channeltext[i] == '.')
                continue;

            channeltext[i] = '-';
        }
        RxNumber.text = new string(channeltext);
    }
    public void ShowHideIdentificationPanel()
    {
        IdentificationPanel.SetActive(!IdentificationPanel.activeSelf);
    }


    public void SetIdentificationPanelText(string message)
    {
        IdentificationPanel.GetComponentInChildren<Text>().text = message;
    }

    public void SetCallPanelText(string message)
    {
        CallPanel.GetComponentInChildren<Text>().text = message;
    }

    public void setMenuPanelHeader(string header)
    {
        MenuPanel.transform.Find("Header").GetComponent<Text>().text = header;
    }

    public void setReplaceableText(char input, int index, Text inputlabel)
    {
        char[] contactIdText = inputlabel.text.ToCharArray();

        contactIdText[index] = input;
        for (int i = index + 1; i < contactIdText.Length; i++)
        {
            contactIdText[i] = '-';
        }
        inputlabel.text = new string(contactIdText);

    }


    public void setCallIdNumber(char input)
    {
        char[] callInputText = ContactID.text.ToCharArray();
        //callInputText = input;
        ContactID.text = new string(callInputText);

    }
    public void ChangeBackLight(BacklightLavel lavel)
    {
        Image[] allPanel = GetComponentsInChildren<Image>();
        foreach (Image img in allPanel)
        {
            if (img.gameObject.tag == "DisplayPanel")
            {
                if (lavel == BacklightLavel.HIGH)
                {
                    BaretDisplayColor = HighColor;
                }
                else 
                if (lavel == BacklightLavel.MEDIUM)
                {
                    BaretDisplayColor = MediumColor;
                }
                else
                {
                    BaretDisplayColor = LowColor;
                }
                img.color = BaretDisplayColor;
            }
        }
    }
    public void PowerOffDisplayLight()
    {
        Image[] allPanel = GetComponentsInChildren<Image>();
        foreach (Image img in allPanel)
        {
            if (img.gameObject.tag == "DisplayPanel")
            {
                BaretDisplayColor = LightOffColor;

                img.color = BaretDisplayColor;
            }
        }
    }

    public GameObject taskCheckedPanel;

}
