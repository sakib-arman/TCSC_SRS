using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class CallSettings : MonoBehaviour
{

    //call settings property
    public static bool callIdEditable = false;
    private static bool isNumberPadActive = false;
    public static int cursorIndex = 0;

    /*==========================
    SelCallSettings Barret2090
    ==========================*/
    public static void setSelCallId(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text call_text = radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                Logger.Log(radio.Navigator.Peek());
                Logger.Log("Sel Call Start");
                break;
            case ButtonKeyCode.CANCEL:
                if (!isNumberPadActive)
                {
                    radio.Navigator.Pop();
                    //display Control
                    radio.displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel

                }

                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        call_text.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }
                break;

            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                if (radio.addressBookIndex >= radio.adressBook.Count)
                {
                    radio.addressBookIndex = 0;
                }
                //Display control
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                if (radio.addressBookIndex < 0)
                {
                    radio.addressBookIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[radio.addressBookIndex].ID.ToString());
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.OK:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                    radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[0].ID.ToString());
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
                }
                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (cursorIndex == 4)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 2);
                    }

                    else if (cursorIndex == 5)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 1);
                        call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
                    }
                    else if (cursorIndex == 3)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 3);
                        call_text.text = call_text.text.Insert(0, "0");
                    }
                    else if (cursorIndex == 2)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 4);
                        call_text.text = call_text.text.Insert(0, "00");
                    }
                    else if (cursorIndex == 1)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 5);
                        call_text.text = call_text.text.Insert(0, "000");
                    }
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
                }
                break;
            default:
                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    call_text.text = "------";
                    cursorIndex = 0;
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < call_text.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex] = input;
                        call_text.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    }

                }
                break;
        }
    }

    /*==========================
    Tele Call Settings
    ==========================*/
    public static void setTeleCallId(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text call_text = radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>();
        Text tele_input = radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("TelephoneNumberText").GetComponent<Text>();//tele input panel
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").gameObject.SetActive(true);
                radio.Navigator.Push("Telephone Number");
                radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("Header").GetComponent<Text>().text = "Telephone Number";
                radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //Call Id Panel
                if (radio.phoneBook.Count != 0)
                {
                    tele_input.text = radio.phoneBook[0].ID.ToString().PadRight(16, '-');
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>().text = "Press Enter For Phone Book";
                }
                else
                {
                    tele_input.text = "----------------";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>().text = "Phone Book Empty";
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("CallInfo").GetComponent<Text>().text = "Press Call to Continue";

                break;
            case ButtonKeyCode.CANCEL:
                if (!isNumberPadActive)
                {
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
                }

                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        call_text.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }
                break;

            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                if (radio.addressBookIndex >= radio.adressBook.Count)
                {
                    radio.addressBookIndex = 0;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                if (radio.addressBookIndex < 0)
                {
                    radio.addressBookIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;

            case ButtonKeyCode.OK:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                    radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[0].ID.ToString());
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                }
                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (cursorIndex == 4)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 2);
                    }

                    else if (cursorIndex == 5)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 1);
                        call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
                    }
                    else if (cursorIndex == 3)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 3);
                        call_text.text = call_text.text.Insert(0, "0");
                    }
                    else if (cursorIndex == 2)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 4);
                        call_text.text = call_text.text.Insert(0, "00");
                    }
                    else if (cursorIndex == 1)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 5);
                        call_text.text = call_text.text.Insert(0, "000");
                    }
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
                }
                break;
            default:
                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    call_text.text = "------";
                    cursorIndex = 0;
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < call_text.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex] = input;
                        call_text.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    }

                }
                break;
        }
    }

    /*==========================
       Tele Call input
    ==========================*/
    public static void TeleCallInput(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text tele_input = radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("TelephoneNumberText").GetComponent<Text>();//tele input panel
        Text tele_phonebook_info = radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>();//tele input panel
        Text tele_info = radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("CallInfo").GetComponent<Text>();
        switch (buttonID)
        {

            case ButtonKeyCode.CALL:
                Logger.Log("Tele Call Start");
                break;

            case ButtonKeyCode.CANCEL:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(true); //Call Id Panel
                    radio.Navigator.Pop();
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = tele_input.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        tele_input.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }
                break;

            case ButtonKeyCode.OK:

                if (!isNumberPadActive)
                {
                    if (radio.phoneBook.Count != 0)
                    {
                        tele_input.text = radio.phoneBook[radio.phoneBookIndex].ID.ToString().PadRight(16, '-');
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>().text = "Press Enter for Phone Book";
                    }
                    else
                    {
                        tele_input.text = "----------------";
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>().text = "Phone Book Empty";
                    }
                }

                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (radio.phoneBook.Count != 0)
                    {
                        tele_input.text = tele_input.text;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
                        tele_phonebook_info.text = "Press Enter for Phone Book";
                        tele_info.text = "Please Call to Continue";
                    }
                    else
                    {
                        tele_input.text = tele_input.text;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
                        tele_phonebook_info.text = "Phone Book Emplty";
                        tele_info.text = "Please Call to continue";
                    }
                }

                break;

            case ButtonKeyCode.UP_ARROW:
                radio.phoneBookIndex++;
                if (radio.phoneBookIndex >= radio.phoneBook.Count)
                {
                    radio.phoneBookIndex = 0;
                }
                tele_input.text = radio.phoneBook[radio.phoneBookIndex].ID.ToString().PadRight(16, '-');
                tele_phonebook_info.text = radio.phoneBook[radio.phoneBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.phoneBookIndex--;
                if (radio.phoneBookIndex < 0)
                {
                    radio.phoneBookIndex = radio.phoneBook.Count - 1;
                }
                tele_input.text = radio.phoneBook[radio.phoneBookIndex].ID.ToString().PadRight(16, '-');
                tele_phonebook_info.text = radio.phoneBook[radio.phoneBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
                break;

            default:
                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    tele_input.text = "----------------";
                    cursorIndex = 0;
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < tele_input.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = tele_input.text.ToCharArray();
                        temp[cursorIndex] = input;
                        tele_input.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= tele_input.text.Length ? tele_input.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(false); //contact label 
                        tele_info.text = "Press Enter to Accept New Number \nPress Call to Continue"; //Static Call Text label
                    }

                }
                break;
        }
    }


    /*==========================
       Hang Up call Settings
    ==========================*/

    public static void setHangupCallId(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text call_text = radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                Logger.Log("Hang Up Call Start");
                break;
            case ButtonKeyCode.CANCEL:
                if (!isNumberPadActive)
                {
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel

                }

                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        call_text.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }
                break;

            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                if (radio.addressBookIndex >= radio.adressBook.Count)
                {
                    radio.addressBookIndex = 0;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                if (radio.addressBookIndex < 0)
                {
                    radio.addressBookIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.OK:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                    radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[0].ID.ToString());
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                }
                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (cursorIndex == 4)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 2);
                    }

                    else if (cursorIndex == 5)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 1);
                        call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
                    }
                    else if (cursorIndex == 3)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 3);
                        call_text.text = call_text.text.Insert(0, "0");
                    }
                    else if (cursorIndex == 2)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 4);
                        call_text.text = call_text.text.Insert(0, "00");
                    }
                    else if (cursorIndex == 1)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 5);
                        call_text.text = call_text.text.Insert(0, "000");
                    }
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
                }
                break;
            default:
                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    call_text.text = "------";
                    cursorIndex = 0;
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < call_text.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex] = input;
                        call_text.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    }

                }
                break;
        }
    }

    /*==========================
     Page call Settings
    ==========================*/
    public static void setPageCallId(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text call_text = radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                radio.displayControllerBarrett2090.CallPanel.transform.Find("TextMessagePanel").gameObject.SetActive(true);
                radio.displayControllerBarrett2090.CallPanel.transform.Find("TextMessagePanel").Find("Header").GetComponent<Text>().text = "Text Message";
                radio.Navigator.Push("Page_Call_Message");
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //Call Id Panel
                break;
            case ButtonKeyCode.CANCEL:

                if (!isNumberPadActive)
                {
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel

                }

                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        call_text.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }

                break;

            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                if (radio.addressBookIndex >= radio.adressBook.Count)
                {
                    radio.addressBookIndex = 0;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                if (radio.addressBookIndex < 0)
                {
                    radio.addressBookIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.OK:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                    radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[0].ID.ToString());
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                }
                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (cursorIndex == 4)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 2);
                    }

                    else if (cursorIndex == 5)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 1);
                        call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
                    }
                    else if (cursorIndex == 3)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 3);
                        call_text.text = call_text.text.Insert(0, "0");
                    }
                    else if (cursorIndex == 2)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 4);
                        call_text.text = call_text.text.Insert(0, "00");
                    }
                    else if (cursorIndex == 1)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 5);
                        call_text.text = call_text.text.Insert(0, "000");
                    }
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
                }

                break;

            default:

                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    call_text.text = "------";
                    cursorIndex = 0;
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < call_text.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex] = input;
                        call_text.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    }

                }

                //Logger.Log(input);
                break;
        }
    }

    /*==========================
    Page call Input
   ==========================*/
    public static void PageCallInput(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text text_message = radio.displayControllerBarrett2090.CallPanel.transform.Find("TextMessagePanel").Find("TextMessage").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                Logger.Log("Page Call Start");
                break;

            case ButtonKeyCode.CANCEL:

                if (text_message.text.Length == 0)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("TextMessagePanel").gameObject.SetActive(false);
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(true); //Call Id Panel
                }
                else
                {
                    text_message.text = text_message.text.Remove(text_message.text.Length - 1, 1);
                }
                break;

            default:
                if (text_message.text.Length >= 32)
                {
                    return;
                }
                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                text_message.text += input;
                break;
        }
    }

    /*==========================
   Gps Request call Settings
   ==========================*/

    public static void setGpsRequestCallId(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text call_text = radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                Logger.Log("Gps Request Call Start");
                break;
            case ButtonKeyCode.CANCEL:
                if (!isNumberPadActive)
                {
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
                }

                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        call_text.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }
                break;

            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                if (radio.addressBookIndex >= radio.adressBook.Count)
                {
                    radio.addressBookIndex = 0;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                if (radio.addressBookIndex < 0)
                {
                    radio.addressBookIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.OK:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                    radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[0].ID.ToString());
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                }
                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (cursorIndex == 4)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 2);
                    }

                    else if (cursorIndex == 5)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 1);
                        call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
                    }
                    else if (cursorIndex == 3)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 3);
                        call_text.text = call_text.text.Insert(0, "0");
                    }
                    else if (cursorIndex == 2)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 4);
                        call_text.text = call_text.text.Insert(0, "00");
                    }
                    else if (cursorIndex == 1)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 5);
                        call_text.text = call_text.text.Insert(0, "000");
                    }
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
                }
                break;
            default:
                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    call_text.text = "------";
                    cursorIndex = 0;
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < call_text.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex] = input;
                        call_text.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    }

                }
                break;
        }
    }

    /*==========================
    status Request call Settings
    ==========================*/

    public static void setStatusRequestCallId(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text call_text = radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                Logger.Log("Status Request Call Start");
                break;
            case ButtonKeyCode.CANCEL:
                if (!isNumberPadActive)
                {
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
                }

                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        call_text.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }
                break;

            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                if (radio.addressBookIndex >= radio.adressBook.Count)
                {
                    radio.addressBookIndex = 0;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[radio.addressBookIndex].ID.ToString());
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                if (radio.addressBookIndex < 0)
                {
                    radio.addressBookIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.OK:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                }
                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (cursorIndex == 4)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 2);
                    }

                    else if (cursorIndex == 5)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 1);
                        call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
                    }
                    else if (cursorIndex == 3)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 3);
                        call_text.text = call_text.text.Insert(0, "0");
                    }
                    else if (cursorIndex == 2)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 4);
                        call_text.text = call_text.text.Insert(0, "00");
                    }
                    else if (cursorIndex == 1)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 5);
                        call_text.text = call_text.text.Insert(0, "000");
                    }
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
                }
                break;
            default:
                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    call_text.text = "------";
                    cursorIndex = 0;
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < call_text.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex] = input;
                        call_text.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    }

                }
                break;
        }

    }


    /*==========================
      set Secure call Settings
      ==========================*/
    public static void setSecureCallId(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text call_text = radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                Logger.Log("Secure Call Start");
                break;
            case ButtonKeyCode.CANCEL:
                if (!isNumberPadActive)
                {
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
                }

                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        call_text.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }
                break;

            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                if (radio.addressBookIndex >= radio.adressBook.Count)
                {
                    radio.addressBookIndex = 0;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                if (radio.addressBookIndex < 0)
                {
                    radio.addressBookIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;
            case ButtonKeyCode.OK:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                    radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[0].ID.ToString());
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                }
                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (cursorIndex == 4)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 2);
                    }

                    else if (cursorIndex == 5)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 1);
                        call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
                    }
                    else if (cursorIndex == 3)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 3);
                        call_text.text = call_text.text.Insert(0, "0");
                    }
                    else if (cursorIndex == 2)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 4);
                        call_text.text = call_text.text.Insert(0, "00");
                    }
                    else if (cursorIndex == 1)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 5);
                        call_text.text = call_text.text.Insert(0, "000");
                    }
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
                }
                break;
            default:
                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    call_text.text = "------";
                    cursorIndex = 0;
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < call_text.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex] = input;
                        call_text.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    }

                }
                break;
        }
    }


    /*==========================
      Becaon call Settings
     ==========================*/

    public static void setBeaconCallId(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text call_text = radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.CALL:
                Logger.Log("Beacon Call Start");
                break;
            case ButtonKeyCode.CANCEL:
                if (!isNumberPadActive)
                {
                    radio.Navigator.Pop();
                    radio.displayControllerBarrett2090.SetCallPanelText(BarrettMenu.CallText[BarrettMenu.CallIndex]);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
                }

                if (isNumberPadActive)
                {
                    if (cursorIndex > 0)
                    {
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex - 1] = '-';
                        call_text.text = new string(temp);
                        cursorIndex--;
                        if (cursorIndex <= 0)
                        {
                            isNumberPadActive = false;
                            cursorIndex = 0;
                        }
                    }
                }
                break;


            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                if (radio.addressBookIndex >= radio.adressBook.Count)
                {
                    radio.addressBookIndex = 0;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;

            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                if (radio.addressBookIndex < 0)
                {
                    radio.addressBookIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                radio.displayControllerBarrett2090.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
                radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                break;

            case ButtonKeyCode.OK:
                if (!isNumberPadActive)
                {
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
                    radio.displayControllerBarrett2090.SetCallPanelText(radio.adressBook[0].ID.ToString());
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
                }
                if (isNumberPadActive)
                {
                    isNumberPadActive = false;
                    if (cursorIndex == 4)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 2);
                    }

                    else if (cursorIndex == 5)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 1);
                        call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
                    }
                    else if (cursorIndex == 3)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 3);
                        call_text.text = call_text.text.Insert(0, "0");
                    }
                    else if (cursorIndex == 2)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 4);
                        call_text.text = call_text.text.Insert(0, "00");
                    }
                    else if (cursorIndex == 1)
                    {
                        call_text.text = call_text.text.Remove(call_text.text.Length - 5);
                        call_text.text = call_text.text.Insert(0, "000");
                    }
                    radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
                }
                break;

            default:
                if (!isNumberPadActive)
                {
                    isNumberPadActive = true;
                    call_text.text = "------";
                    cursorIndex = 0;
                }
                if (isNumberPadActive)
                {
                    if (cursorIndex < call_text.text.Length)
                    {
                        char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                        char[] temp = call_text.text.ToCharArray();
                        temp[cursorIndex] = input;
                        call_text.text = new string(temp);
                        cursorIndex++;
                        cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
                        radio.displayControllerBarrett2090.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    }

                }
                break;
        }
    }
    /*======================
    Barret2090 End
    ======================*/
    /*==========================
    SelCallSettings Barret2050
    ==========================*/
    //public static void setSelCallId(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text call_text = radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            Logger.Log(radio.Navigator.Peek());
    //            Logger.Log("Sel Call Start");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isNumberPadActive)
    //            {
    //                radio.Navigator.Pop();
    //                //display Control
    //                radio.displayControllerBarrett2050.SetCallPanelText(Barrett2050Menu.CallText[Barrett2050Menu.CallIndex]);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel

    //            }

    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    call_text.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }
    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            if (radio.addressBookIndex >= radio.adressBook.Count)
    //            {
    //                radio.addressBookIndex = 0;
    //            }
    //            //Display control
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            if (radio.addressBookIndex < 0)
    //            {
    //                radio.addressBookIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[radio.addressBookIndex].ID.ToString());
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.OK:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //                radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[0].ID.ToString());
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
    //            }
    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (cursorIndex == 4)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 2);
    //                }

    //                else if (cursorIndex == 5)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 1);
    //                    call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 3);
    //                    call_text.text = call_text.text.Insert(0, "0");
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 4);
    //                    call_text.text = call_text.text.Insert(0, "00");
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 5);
    //                    call_text.text = call_text.text.Insert(0, "000");
    //                }
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
    //            }
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                call_text.text = "------";
    //                cursorIndex = 0;
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < call_text.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    call_text.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                }

    //            }
    //            break;
    //    }
    //}

    /*==========================
    Tele Call Settings
    ==========================*/
    //public static void setTeleCallId(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text call_text = radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>();
    //    Text tele_input = radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("TelephoneNumberText").GetComponent<Text>();//tele input panel
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").gameObject.SetActive(true);
    //            radio.Navigator.Push("Telephone Number");
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("Header").GetComponent<Text>().text = "Telephone Number";
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //Call Id Panel
    //            if (radio.phoneBook.Count != 0)
    //            {
    //                tele_input.text = radio.phoneBook[0].ID.ToString().PadRight(16, '-');
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>().text = "Press Enter For Phone Book";
    //            }
    //            else
    //            {
    //                tele_input.text = "----------------";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>().text = "Phone Book Empty";
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("CallInfo").GetComponent<Text>().text = "Press Call to Continue";

    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isNumberPadActive)
    //            {
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.SetCallPanelText(Barrett2050Menu.CallText[Barrett2050Menu.CallIndex]);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
    //            }

    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    call_text.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }
    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            if (radio.addressBookIndex >= radio.adressBook.Count)
    //            {
    //                radio.addressBookIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            if (radio.addressBookIndex < 0)
    //            {
    //                radio.addressBookIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;

    //        case ButtonKeyCode.OK:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //                radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[0].ID.ToString());
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            }
    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (cursorIndex == 4)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 2);
    //                }

    //                else if (cursorIndex == 5)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 1);
    //                    call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 3);
    //                    call_text.text = call_text.text.Insert(0, "0");
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 4);
    //                    call_text.text = call_text.text.Insert(0, "00");
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 5);
    //                    call_text.text = call_text.text.Insert(0, "000");
    //                }
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
    //            }
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                call_text.text = "------";
    //                cursorIndex = 0;
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < call_text.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    call_text.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                }

    //            }
    //            break;
    //    }
    //}

    /*==========================
       Tele Call input
    ==========================*/
    //public static void TeleCallInput(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text tele_input = radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("TelephoneNumberText").GetComponent<Text>();//tele input panel
    //    Text tele_phonebook_info = radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>();//tele input panel
    //    Text tele_info = radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("CallInfo").GetComponent<Text>();
    //    switch (buttonID)
    //    {

    //        case ButtonKeyCode.CALL:
    //            Logger.Log("Tele Call Start");
    //            break;

    //        case ButtonKeyCode.CANCEL:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(true); //Call Id Panel
    //                radio.Navigator.Pop();
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = tele_input.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    tele_input.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }
    //            break;

    //        case ButtonKeyCode.OK:

    //            if (!isNumberPadActive)
    //            {
    //                if (radio.phoneBook.Count != 0)
    //                {
    //                    tele_input.text = radio.phoneBook[radio.phoneBookIndex].ID.ToString().PadRight(16, '-');
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>().text = "Press Enter for Phone Book";
    //                }
    //                else
    //                {
    //                    tele_input.text = "----------------";
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").GetComponent<Text>().text = "Phone Book Empty";
    //                }
    //            }

    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (radio.phoneBook.Count != 0)
    //                {
    //                    tele_input.text = tele_input.text;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
    //                    tele_phonebook_info.text = "Press Enter for Phone Book";
    //                    tele_info.text = "Please Call to Continue";
    //                }
    //                else
    //                {
    //                    tele_input.text = tele_input.text;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(true);
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
    //                    tele_phonebook_info.text = "Phone Book Emplty";
    //                    tele_info.text = "Please Call to continue";
    //                }
    //            }

    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            radio.phoneBookIndex++;
    //            if (radio.phoneBookIndex >= radio.phoneBook.Count)
    //            {
    //                radio.phoneBookIndex = 0;
    //            }
    //            tele_input.text = radio.phoneBook[radio.phoneBookIndex].ID.ToString().PadRight(16, '-');
    //            tele_phonebook_info.text = radio.phoneBook[radio.phoneBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.phoneBookIndex--;
    //            if (radio.phoneBookIndex < 0)
    //            {
    //                radio.phoneBookIndex = radio.phoneBook.Count - 1;
    //            }
    //            tele_input.text = radio.phoneBook[radio.phoneBookIndex].ID.ToString().PadRight(16, '-');
    //            tele_phonebook_info.text = radio.phoneBook[radio.phoneBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("Arrows").gameObject.SetActive(true);
    //            break;

    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                tele_input.text = "----------------";
    //                cursorIndex = 0;
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < tele_input.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = tele_input.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    tele_input.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= tele_input.text.Length ? tele_input.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("TelephoneNumberPanel").Find("InfoPhonebook").gameObject.SetActive(false); //contact label 
    //                    tele_info.text = "Press Enter to Accept New Number \nPress Call to Continue"; //Static Call Text label
    //                }

    //            }
    //            break;
    //    }
    //}


    /*==========================
       Hang Up call Settings
    ==========================*/

    //public static void setHangupCallId(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text call_text = radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            Logger.Log("Hang Up Call Start");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isNumberPadActive)
    //            {
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.SetCallPanelText(Barrett2050Menu.CallText[Barrett2050Menu.CallIndex]);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel

    //            }

    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    call_text.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }
    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            if (radio.addressBookIndex >= radio.adressBook.Count)
    //            {
    //                radio.addressBookIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            if (radio.addressBookIndex < 0)
    //            {
    //                radio.addressBookIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.OK:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //                radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[0].ID.ToString());
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            }
    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (cursorIndex == 4)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 2);
    //                }

    //                else if (cursorIndex == 5)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 1);
    //                    call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 3);
    //                    call_text.text = call_text.text.Insert(0, "0");
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 4);
    //                    call_text.text = call_text.text.Insert(0, "00");
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 5);
    //                    call_text.text = call_text.text.Insert(0, "000");
    //                }
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
    //            }
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                call_text.text = "------";
    //                cursorIndex = 0;
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < call_text.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    call_text.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                }

    //            }
    //            break;
    //    }
    //}

    /*==========================
     Page call Settings
    ==========================*/
    //public static void setPageCallId(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text call_text = radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("TextMessagePanel").gameObject.SetActive(true);
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("TextMessagePanel").Find("Header").GetComponent<Text>().text = "Text Message";
    //            radio.Navigator.Push("Page_Call_Message");
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //Call Id Panel
    //            break;
    //        case ButtonKeyCode.CANCEL:

    //            if (!isNumberPadActive)
    //            {
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.SetCallPanelText(Barrett2050Menu.CallText[Barrett2050Menu.CallIndex]);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel

    //            }

    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    call_text.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }

    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            if (radio.addressBookIndex >= radio.adressBook.Count)
    //            {
    //                radio.addressBookIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            if (radio.addressBookIndex < 0)
    //            {
    //                radio.addressBookIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.OK:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //                radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[0].ID.ToString());
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            }
    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (cursorIndex == 4)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 2);
    //                }

    //                else if (cursorIndex == 5)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 1);
    //                    call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 3);
    //                    call_text.text = call_text.text.Insert(0, "0");
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 4);
    //                    call_text.text = call_text.text.Insert(0, "00");
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 5);
    //                    call_text.text = call_text.text.Insert(0, "000");
    //                }
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
    //            }

    //            break;

    //        default:

    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                call_text.text = "------";
    //                cursorIndex = 0;
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < call_text.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    call_text.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                }

    //            }

    //            //Logger.Log(input);
    //            break;
    //    }
    //}

    /*==========================
    Page call Input
   ==========================*/
    //public static void PageCallInput(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text text_message = radio.displayControllerBarrett2050.CallPanel.transform.Find("TextMessagePanel").Find("TextMessage").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            Logger.Log("Page Call Start");
    //            break;

    //        case ButtonKeyCode.CANCEL:

    //            if (text_message.text.Length == 0)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("TextMessagePanel").gameObject.SetActive(false);
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(true); //Call Id Panel
    //            }
    //            else
    //            {
    //                text_message.text = text_message.text.Remove(text_message.text.Length - 1, 1);
    //            }
    //            break;

    //        default:
    //            if (text_message.text.Length >= 32)
    //            {
    //                return;
    //            }
    //            char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //            text_message.text += input;
    //            break;
    //    }
    //}

    /*==========================
   Gps Request call Settings
   ==========================*/

    //public static void setGpsRequestCallId(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text call_text = radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            Logger.Log("Gps Request Call Start");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isNumberPadActive)
    //            {
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.SetCallPanelText(Barrett2050Menu.CallText[Barrett2050Menu.CallIndex]);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
    //            }

    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    call_text.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }
    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            if (radio.addressBookIndex >= radio.adressBook.Count)
    //            {
    //                radio.addressBookIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            if (radio.addressBookIndex < 0)
    //            {
    //                radio.addressBookIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.OK:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //                radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[0].ID.ToString());
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            }
    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (cursorIndex == 4)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 2);
    //                }

    //                else if (cursorIndex == 5)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 1);
    //                    call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 3);
    //                    call_text.text = call_text.text.Insert(0, "0");
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 4);
    //                    call_text.text = call_text.text.Insert(0, "00");
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 5);
    //                    call_text.text = call_text.text.Insert(0, "000");
    //                }
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
    //            }
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                call_text.text = "------";
    //                cursorIndex = 0;
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < call_text.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    call_text.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                }

    //            }
    //            break;
    //    }
    //}

    /*==========================
    status Request call Settings
    ==========================*/

    //public static void setStatusRequestCallId(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text call_text = radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            Logger.Log("Status Request Call Start");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isNumberPadActive)
    //            {
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.SetCallPanelText(Barrett2050Menu.CallText[Barrett2050Menu.CallIndex]);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
    //            }

    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    call_text.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }
    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            if (radio.addressBookIndex >= radio.adressBook.Count)
    //            {
    //                radio.addressBookIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[radio.addressBookIndex].ID.ToString());
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            if (radio.addressBookIndex < 0)
    //            {
    //                radio.addressBookIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.OK:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            }
    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (cursorIndex == 4)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 2);
    //                }

    //                else if (cursorIndex == 5)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 1);
    //                    call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 3);
    //                    call_text.text = call_text.text.Insert(0, "0");
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 4);
    //                    call_text.text = call_text.text.Insert(0, "00");
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 5);
    //                    call_text.text = call_text.text.Insert(0, "000");
    //                }
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
    //            }
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                call_text.text = "------";
    //                cursorIndex = 0;
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < call_text.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    call_text.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                }

    //            }
    //            break;
    //    }

    //}


    /*==========================
      set Secure call Settings
      ==========================*/
    //public static void setSecureCallId(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text call_text = radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            Logger.Log("Secure Call Start");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isNumberPadActive)
    //            {
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.SetCallPanelText(Barrett2050Menu.CallText[Barrett2050Menu.CallIndex]);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
    //            }

    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    call_text.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }
    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            if (radio.addressBookIndex >= radio.adressBook.Count)
    //            {
    //                radio.addressBookIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            if (radio.addressBookIndex < 0)
    //            {
    //                radio.addressBookIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;
    //        case ButtonKeyCode.OK:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //                radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[0].ID.ToString());
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            }
    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (cursorIndex == 4)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 2);
    //                }

    //                else if (cursorIndex == 5)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 1);
    //                    call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 3);
    //                    call_text.text = call_text.text.Insert(0, "0");
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 4);
    //                    call_text.text = call_text.text.Insert(0, "00");
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 5);
    //                    call_text.text = call_text.text.Insert(0, "000");
    //                }
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
    //            }
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                call_text.text = "------";
    //                cursorIndex = 0;
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < call_text.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    call_text.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                }

    //            }
    //            break;
    //    }
    //}


    /*==========================
      Becaon call Settings
     ==========================*/

    //public static void setBeaconCallId(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text call_text = radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.CALL:
    //            Logger.Log("Beacon Call Start");
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (!isNumberPadActive)
    //            {
    //                radio.Navigator.Pop();
    //                radio.displayControllerBarrett2050.SetCallPanelText(Barrett2050Menu.CallText[Barrett2050Menu.CallIndex]);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ID").gameObject.SetActive(false); //ID label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Arrows").gameObject.SetActive(true); //Arrows
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").gameObject.SetActive(false); //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("HeaderText").GetComponent<Text>().text = "Send Call"; //Header text
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallText").gameObject.SetActive(true); //call Text Panel
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").gameObject.SetActive(false); //CallFormat Id Panel
    //            }

    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex > 0)
    //                {
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex - 1] = '-';
    //                    call_text.text = new string(temp);
    //                    cursorIndex--;
    //                    if (cursorIndex <= 0)
    //                    {
    //                        isNumberPadActive = false;
    //                        cursorIndex = 0;
    //                    }
    //                }
    //            }
    //            break;


    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            if (radio.addressBookIndex >= radio.adressBook.Count)
    //            {
    //                radio.addressBookIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;

    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            if (radio.addressBookIndex < 0)
    //            {
    //                radio.addressBookIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("CallId").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label.ToString();
    //            radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            break;

    //        case ButtonKeyCode.OK:
    //            if (!isNumberPadActive)
    //            {
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(true); //contact label 
    //                radio.displayControllerBarrett2050.SetCallPanelText(radio.adressBook[0].ID.ToString());
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").GetComponent<Text>().text = "Up Down to cycle Address";
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Call to Continue"; //Static Call Text label
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(true);
    //            }
    //            if (isNumberPadActive)
    //            {
    //                isNumberPadActive = false;
    //                if (cursorIndex == 4)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 2);
    //                }

    //                else if (cursorIndex == 5)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 1);
    //                    call_text.text = call_text.text.Insert(0, "0");        // insert 0 at first index      
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 3);
    //                    call_text.text = call_text.text.Insert(0, "0");
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 4);
    //                    call_text.text = call_text.text.Insert(0, "00");
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    call_text.text = call_text.text.Remove(call_text.text.Length - 5);
    //                    call_text.text = call_text.text.Insert(0, "000");
    //                }
    //                radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter For AdressBook \nPress Call to Continue"; //Static Call Text label
    //            }
    //            break;

    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                call_text.text = "------";
    //                cursorIndex = 0;
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < call_text.text.Length)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                    char[] temp = call_text.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    call_text.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= call_text.text.Length ? call_text.text.Length : cursorIndex;
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("ContactLabel").gameObject.SetActive(false); //contact label 
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("Info").GetComponent<Text>().text = "Press Enter to Accept New ID \nPress Call to Continue"; //Static Call Text label
    //                    radio.displayControllerBarrett2050.CallPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //                }

    //            }
    //            break;
    //    }
    //}
    //public static void doALECall(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.OK:
    //            Logger.Log("Doing ALE Call");
    //            break;
    //    }

    //}
    /*======================
    Barret2050 End
    ======================*/
    /*======================
    QMAC90M SELCALL ENTRY
    ======================*/
    //public static void QmacSellCall(QMAC90M radio, ButtonKeyCode buttonID) //Do SelCall
    //{
    //    Text selcallInputID = radio.qMACDisplayController.sellcallID;
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_STAR:
    //            if (isNumberPadActive)
    //            {
    //                selcallInputID.text = "-----";
    //                radio.qMACDisplayController.SelcallPanel.transform.Find("Selcall").GetComponent<Text>().text = "SEnd";
    //                radio.StartCoroutine(WaitBefore(radio, 2f));
    //                radio.Navigator.Pop();
    //            }
    //            else
    //            {
    //                //do nothing
    //            }
    //            break;
    //        case ButtonKeyCode.NUM_HASH:
    //            if (isNumberPadActive)
    //            {
    //                if (selcallInputID.text.Equals("-----"))  // ignore ------ das
    //                    return;
    //                int countDas = selcallInputID.text.Count(c => c == '-');  // count total hyphen using LINQ
    //                if (countDas > 0)
    //                {
    //                    radio.qMACDisplayController.SelcallPanel.gameObject.SetActive(true);
    //                    radio.qMACDisplayController.Selcall.gameObject.SetActive(true);
    //                }
    //                if (cursorIndex == 4)
    //                {
    //                    selcallInputID.text = selcallInputID.text.Remove(selcallInputID.text.Length - 1);  // ignore last 2 das
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    selcallInputID.text = selcallInputID.text.Remove(selcallInputID.text.Length - 2);   // ignore last 3 das
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    selcallInputID.text = selcallInputID.text.Remove(selcallInputID.text.Length - 3);  // ignore last 4 das

    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    selcallInputID.text = selcallInputID.text.Remove(selcallInputID.text.Length - 4);   // ignore last 5 das
    //                }
    //                radio.activeChannel.SelCall = Convert.ToInt32(selcallInputID.text);
    //                Logger.Log(selcallInputID.text);
    //                radio.qMACDisplayController.channelInput.gameObject.SetActive(false);
    //                radio.qMACDisplayController.Selcall.gameObject.SetActive(true);
    //                radio.qMACDisplayController.sellcallID.gameObject.SetActive(false);
    //            }
    //            if (!isNumberPadActive)
    //            {
    //                //do nothing
    //            }
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            selcallInputID.text = "-----";
    //            radio.qMACDisplayController.SelcallPanel.gameObject.SetActive(false);
    //            radio.qMACDisplayController.channelInput.gameObject.SetActive(true);
    //            cursorIndex = 0;
    //            radio.Navigator.Pop();
    //            break;
    //        default:
    //            radio.qMACDisplayController.SelcallPanel.transform.Find("SelcallInput").gameObject.SetActive(true);
    //            if (!isNumberPadActive)
    //            {
    //                /*cursorIndex = 0;*/
    //                isNumberPadActive = true;
    //                selcallInputID.text = "-----";
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (cursorIndex < selcallInputID.text.Length - 1)
    //                {
    //                    char input = NumberPadSetting.KeyPadInput(false, buttonID);
    //                    char[] temp = selcallInputID.text.ToCharArray();
    //                    temp[cursorIndex] = input;
    //                    selcallInputID.text = new string(temp);
    //                    cursorIndex++;
    //                    cursorIndex = cursorIndex >= selcallInputID.text.Length ? selcallInputID.text.Length : cursorIndex;
    //                    radio.qMACDisplayController.Selcall.gameObject.SetActive(false);
    //                    radio.qMACDisplayController.sellcallID.gameObject.SetActive(true);
    //                }
    //            }
    //            break;
    //    }
    //}
    //static IEnumerator WaitBefore(QMAC90M radio, float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    radio.qMACDisplayController.channelInput.gameObject.SetActive(true);
    //    radio.qMACDisplayController.SelcallPanel.gameObject.SetActive(false);
    //    radio.qMACDisplayController.SelcallPanel.transform.Find("Selcall").gameObject.SetActive(false);
    //    radio.qMACDisplayController.SelcallPanel.transform.Find("SelcallInput").gameObject.SetActive(false);
    //    Logger.Log("WaitBeforeSend");
    //}

    //public static void QmacQuickSelcall(QMAC90M radio, ButtonKeyCode buttonID) //Quick Selcall to resent Number
    //{

    //    Text quickCall = radio.qMACDisplayController.incomingCallPanel.transform.Find("CellNumber").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.NUM_STAR:
    //            quickCall.text = Convert.ToString(radio.activeChannel.SelCall);
    //            Logger.Log(quickCall.text);
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.qMACDisplayController.incomingCallPanel.SetActive(false);
    //            radio.qMACDisplayController.channelInput.gameObject.SetActive(true);
    //            radio.Navigator.Pop();
    //            break;
    //    }
    //}
    //public static void QmacCAllReceive(QMAC90M radio, ButtonKeyCode buttonID, bool iscallincoming, int clickCounter) //Selcall Receive
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.PTT:
    //            radio.qMACDisplayController.incomingCallPanel.transform.Find("AlertDisplay").GetComponent<Text>().text = "Connected ";
    //            radio.qMACDisplayController.incomingCallPanel.transform.Find("CellNumber").GetComponent<Text>().text = "1234";
    //            break;
    //        case ButtonKeyCode.NUM_STAR:
    //            if (clickCounter == 2)
    //            {
    //                radio.qMACDisplayController.incomingCallPanel.transform.Find("AlertDisplay").GetComponent<Text>().text = "Connected ";
    //                radio.qMACDisplayController.incomingCallPanel.transform.Find("CellNumber").GetComponent<Text>().text = "1234";
    //            }

    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.qMACDisplayController.incomingCallPanel.SetActive(false);
    //            radio.qMACDisplayController.channelInput.SetActive(true);
    //            radio.Navigator.Pop();
    //            break;
    //    }
    //}

    /*======================
    QMAC90M SELCALL ENTRY END
    ======================*/



    public int resizeTextMaxSize;


    /*======================
    Xv 3088 call Settings
    ======================*/


    //Group call settings start
    //public static void XV3088GroupCall(XV3088 radio, ButtonKeyCode buttonID)
    //{
    //    Logger.Log("call pressed");
    //    radio.xV3088DisplayController.FText.gameObject.SetActive(false);
    //    radio.xV3088DisplayController.FrequencyType.gameObject.SetActive(false);
    //    Text frequencyText = radio.xV3088DisplayController.Frequency_Number.GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            Logger.Log("Group Call Started..,.");
    //            radio.xV3088DisplayController.AddrCodeText.gameObject.SetActive(false);
    //            radio.xV3088DisplayController.FText.gameObject.SetActive(true);
    //            radio.xV3088DisplayController.FrequencyType.gameObject.SetActive(true);
    //            radio.Navigator.Pop();
    //            radio.RfreshHomeScreen();
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                frequencyText.text = "";
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (frequencyText.text.Length == 2)
    //                {
    //                    frequencyText.text += '.';
    //                }
    //                if (frequencyText.text.Length > 5)
    //                {
    //                    frequencyText.text = frequencyText.text.Remove(frequencyText.text.Length - 1, 1);
    //                }
    //                radio.xV3088DisplayController.Frequency_Number.SetActive(true);
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                frequencyText.text += input;
    //            }
    //            break;
    //    }

    //}
    //Group call settings End



    /*======================
    Rf - 1350 call Settings
  ======================*/


    //Group call settings start
    //public static void rf1350GroupCall(RF1350 radio, ButtonKeyCode buttonID)
    //{
    //    Logger.Log("call pressed");
    //    radio.rf1350DisplayController.FText.gameObject.SetActive(false);
    //    radio.rf1350DisplayController.FrequencyType.gameObject.SetActive(false);
    //    Text frequencyText = radio.rf1350DisplayController.Frequency_Number.GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.ENTER:
    //            Logger.Log("Group Call Started..,.");
    //            radio.rf1350DisplayController.AddrCodeText.gameObject.SetActive(false);
    //            radio.rf1350DisplayController.FText.gameObject.SetActive(true);
    //            radio.rf1350DisplayController.FrequencyType.gameObject.SetActive(true);
    //            radio.Navigator.Pop();
    //            radio.RfreshHomeScreen();
    //            break;
    //        default:
    //            if (!isNumberPadActive)
    //            {
    //                isNumberPadActive = true;
    //                frequencyText.text = "";
    //            }
    //            if (isNumberPadActive)
    //            {
    //                if (frequencyText.text.Length == 2)
    //                {
    //                    frequencyText.text += '.';
    //                }
    //                if (frequencyText.text.Length > 5)
    //                {
    //                    frequencyText.text = frequencyText.text.Remove(frequencyText.text.Length - 1, 1);
    //                }
    //                radio.rf1350DisplayController.Frequency_Number.SetActive(true);
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                frequencyText.text += input;
    //            }
    //            break;
    //    }
    //}
    //Group call settings End
    /*======================
    CD116 CALL ENTRY
    ======================*/
    //public static void CallingTele201(CD116 fieldExchange, ButtonKeyCode buttonID)
    //{
    //    Text CallStatus1 = fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_1").GetComponent<Text>();
    //    Text CallStatus2 = fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            if (fieldExchange.numberOfCalls == 0)
    //            {
    //                CallStatus2.text = "RING";
    //            }
    //            else
    //            {
    //                CallStatus1.text = "RING";
    //                CallStatus2.text = "HOLD";
    //            }
    //            call();
    //            break;
    //    }
    //    void call()
    //    {
    //        fieldExchange.handsetTeleController.tele201.transform.Find("Splash_Indicator").gameObject.SetActive(true);
    //        CallMenuStatusOnCall(fieldExchange);
    //        fieldExchange.Navigator.Push("CallPickUP201");
    //    }
    //}
    //public static void CallingTele202(CD116 fieldExchange, ButtonKeyCode buttonID)
    //{
    //    Text CallStatus1 = fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_1").GetComponent<Text>();
    //    Text CallStatus2 = fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            if (fieldExchange.numberOfCalls == 0)
    //            {
    //                CallStatus2.text = "RING";
    //            }
    //            else
    //            {
    //                CallStatus1.text = "RING";
    //                CallStatus2.text = "HOLD";
    //            }
    //            call();
    //            break;
    //    }
    //    void call()
    //    {
    //        fieldExchange.handsetTeleController.tele202.transform.Find("Splash_Indicator").gameObject.SetActive(true);
    //        CallMenuStatusOnCall(fieldExchange);
    //        fieldExchange.Navigator.Push("CallPickUP202");
    //    }
    //}
    //public static void CallMenuStatusOnCall(CD116 fieldExchange) // Menu When call in progress
    //{
    //    fieldExchange.cd116DisplayController.menuPanel.transform.Find("3").GetComponent<Text>().text = "RELEASE";
    //    fieldExchange.cd116DisplayController.menuPanel.transform.Find("1").GetComponent<Text>().text = "";
    //    fieldExchange.cd116DisplayController.menuPanel.transform.Find("2").GetComponent<Text>().text = "";
    //    fieldExchange.cd116DisplayController.menuPanel.transform.Find("4").GetComponent<Text>().text = "";
    //    fieldExchange.isCallIncoming = true;
    //    fieldExchange.isNumberPadActive = false;
    //    fieldExchange.cursorIndex = 0;
    //    fieldExchange.Navigator.Pop();
    //}
    //public static void CallPickUP201(CD116 fieldExchange, ButtonKeyCode buttonID)
    //{
    //    /*instance.StopAllCoroutines();*/
    //    if (fieldExchange.isCallIncoming == true && buttonID == ButtonKeyCode.PickUP && fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>().text == "RING")
    //    {
    //        fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>().text = "TALK"; //reference
    //        fieldExchange.handsetTeleController.tele201.transform.Find("Splash_Indicator").gameObject.SetActive(false);
    //        /*BlinkStatus(fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2"));*/
    //        CallMenuStatusOnPickUP(fieldExchange);
    //        fieldExchange.numberOfCalls++;
    //    }
    //    if (fieldExchange.isCallIncoming == true && buttonID == ButtonKeyCode.PickUP && fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_1").GetComponent<Text>().text == "RING")
    //    {
    //        fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_1").GetComponent<Text>().text = "TALK";
    //        fieldExchange.handsetTeleController.tele201.transform.Find("Splash_Indicator").gameObject.SetActive(false);
    //        CallMenuStatusOnPickUP(fieldExchange);
    //        fieldExchange.numberOfCalls++;
    //    }
    //    if (buttonID == ButtonKeyCode.DOWN_ARROW)
    //    {
    //        fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>().text = "";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("1").GetComponent<Text>().text = "MORE";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("2").GetComponent<Text>().text = "PARK";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("3").GetComponent<Text>().text = "";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("4").GetComponent<Text>().text = "DIAL";
    //        fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("PhoneID_1").GetComponent<Text>().text = "";
    //        fieldExchange.handsetTeleController.tele202.transform.Find("Splash_Indicator").gameObject.SetActive(false);
    //        fieldExchange.handsetTeleController.tele201.transform.Find("Splash_Indicator").gameObject.SetActive(false);
    //        fieldExchange.numberOfCalls--;
    //        fieldExchange.Navigator.Pop();
    //    }
    //}
    //public static void CallPickUP202(CD116 fieldExchange, ButtonKeyCode buttonID)
    //{
    //    if (fieldExchange.isCallIncoming == true && buttonID == ButtonKeyCode.PickUP && fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_1").GetComponent<Text>().text == "RING")
    //    {
    //        fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_1").GetComponent<Text>().text = "TALK";
    //        fieldExchange.handsetTeleController.tele202.transform.Find("Splash_Indicator").gameObject.SetActive(false);
    //        CallMenuStatusOnPickUP(fieldExchange);
    //        fieldExchange.numberOfCalls++;
    //    }
    //    if (fieldExchange.isCallIncoming == true && buttonID == ButtonKeyCode.PickUP && fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>().text == "RING")
    //    {
    //        fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>().text = "TALK";
    //        fieldExchange.handsetTeleController.tele202.transform.Find("Splash_Indicator").gameObject.SetActive(false);
    //        CallMenuStatusOnPickUP(fieldExchange);
    //        fieldExchange.numberOfCalls++;
    //    }
    //    if (buttonID == ButtonKeyCode.DOWN_ARROW)
    //    {
    //        fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>().text = "";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("1").GetComponent<Text>().text = "MORE";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("2").GetComponent<Text>().text = "PARK";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("3").GetComponent<Text>().text = "";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("4").GetComponent<Text>().text = "DIAL";
    //        fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("PhoneID_1").GetComponent<Text>().text = "";
    //        fieldExchange.handsetTeleController.tele202.transform.Find("Splash_Indicator").gameObject.SetActive(false);
    //        fieldExchange.handsetTeleController.tele201.transform.Find("Splash_Indicator").gameObject.SetActive(false);
    //        fieldExchange.numberOfCalls--;
    //        fieldExchange.Navigator.Pop();
    //    }
    //}
    //public static void CallMenuStatusOnPickUP(CD116 fieldExchange) // Menu When call in progress
    //{
    //    fieldExchange.isCallConnected = true;
    //    if (fieldExchange.numberOfCalls == 0)
    //    {
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("1").GetComponent<Text>().text = "MONIT";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("2").GetComponent<Text>().text = "PARK";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("4").GetComponent<Text>().text = "DIAL";
    //    }
    //    else
    //    {
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("1").GetComponent<Text>().text = "CANCEL";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("2").GetComponent<Text>().text = "3PARTY";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("3").GetComponent<Text>().text = "TRANSF";
    //        fieldExchange.cd116DisplayController.menuPanel.transform.Find("4").GetComponent<Text>().text = "SWAP";
    //    }
    //    fieldExchange.Navigator.Pop();
    //}
    /*static void BlinkStatus(Transform blinkStatus)
    {
        *//*fieldExchange.cd116DisplayController.inoutBoundCall.transform.Find("CallStatus_2").GetComponent<Text>()*//*
        if (blinkStatus.gameObject.activeSelf)
        {

            instance.StartCoroutine(blink_Status(blinkStatus));
        }

    }
    public static CallSettings instance;
    private void Awake()
    {
        instance = this;
    }
    static IEnumerator blink_Status(Transform blink)
    {
        while (true)
        {
            string blinkStatusText = blink.GetComponent<Text>().text;
            blink.GetComponent<Text>().text = blinkStatusText;
            yield return new WaitForSeconds(1f);
            blink.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1f);
        }
    }*/
    /*======================
    CD116 CALL ENTRY END
    ======================*/
}