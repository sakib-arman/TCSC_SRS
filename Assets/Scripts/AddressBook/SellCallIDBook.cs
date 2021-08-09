using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SellCallIDBook : MonoBehaviour
{
    /*==========================
    Barret2090
    ==========================*/
    public static int cursorIndex = 0;
    private static int counter_stage = 0;
    private static string tempName = "";
    public static void SellCallAddEntrySetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text SelCallNameText = radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>();
        Text SelCalllabelID = radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SellCallID").GetComponent<Text>();
        Text footer = radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("Footer").GetComponent<Text>();
        Text linkID = radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>();

        switch (buttonID)
        {
            case ButtonKeyCode.OK:

                if (SelCallNameText.text.Equals("")) // empty string check
                    return;

                if (SelCalllabelID.text.Equals("------"))  // ignore ------ das
                    return;

                counter_stage++;     // stage increment for one panel to another panel
                if (counter_stage == 1)
                {
                    // selcall Id panel
                    cursorIndex = 0;
                    SelCalllabelID.text = "------";   // 
                }
                else if (counter_stage == 2)
                {
                    // linked Id status
                    radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>().text = "No";
                    footer.text = "Input Link Status";

                    // set sellcall ID value without das
                    if (cursorIndex == 4)
                    {
                        SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 2);  // ignore last 2 das
                    }
                    else if (cursorIndex == 5)
                    {
                        SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 1);   //ignore last das
                        SelCalllabelID.text = SelCalllabelID.text.Insert(0, "0");        // insert 0 at first index        
                    }
                    else if (cursorIndex == 3)
                    {
                        SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 3);   // ignore last 3 das
                        SelCalllabelID.text = SelCalllabelID.text.Insert(0, "0");      // add 0 in first index like 0123
                    }
                    else if (cursorIndex == 2)
                    {
                        SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 4);  // ignore last 4 das
                        SelCalllabelID.text = SelCalllabelID.text.Insert(0, "00");       // add 00 in first index
                    }
                    else if (cursorIndex == 1)
                    {
                        SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 5);   // ignore last 5 das
                        SelCalllabelID.text = SelCalllabelID.text.Insert(0, "000");     // add 000 in first index
                    }


                }
                else
                {
                    // set all value in contact list
                    radio.adressBook.Add(new Contact(SelCallNameText.text, int.Parse(SelCalllabelID.text), linkID.text == "Yes" ? true : false));
                    radio.displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = radio.adressBook.Count == 0 ? "Empty" : radio.adressBook.Count + " Entries";
                    radio.displayControllerBarrett2090.SellCallIdBook.SetActive(false);
                    radio.Navigator.Pop();
                    counter_stage = 0;
                }


                break;
            case ButtonKeyCode.CANCEL:
                if (counter_stage == 0 && SelCallNameText.text.Length > 0)  // selcall name entry stage
                {

                    SelCallNameText.text = SelCallNameText.text.Remove(SelCallNameText.text.Length - 1);  // decrement last digit

                }
                else if (counter_stage == 1 && cursorIndex > 0)    // selcall ID stage
                {

                    char[] id = SelCalllabelID.text.ToCharArray();
                    id[cursorIndex - 1] = '-';                 // replace digit with das
                    SelCalllabelID.text = new string(id);  // set text
                    cursorIndex--;                         // increment cursor index
                    cursorIndex = cursorIndex < 0 ? counter_stage++ : cursorIndex;  // check cursor index less than 0

                }
                else
                {
                    // return back in address book panel
                    radio.displayControllerBarrett2090.SellCallIdBook.SetActive(false);
                    radio.Navigator.Pop();
                    counter_stage = 0;
                }
                break;

            case ButtonKeyCode.UP_ARROW:
                if (counter_stage == 2) // link id status
                {
                    linkID.text = linkID.text == "Yes" ? "No" : "Yes"; // change yes and no
                }

                break;
            case ButtonKeyCode.DOWN_ARROW:
                if (counter_stage == 2) // link id status
                {
                    linkID.text = linkID.text == "Yes" ? "No" : "Yes";   // change yes and no
                }
                break;
            default:
                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);  // keypad input
                if (counter_stage == 0 && SelCallNameText.text.Length < 16)
                {
                    //name entry
                    SelCallNameText.text += input.ToString();
                }
                else if (counter_stage == 1 && cursorIndex < 6)
                {
                    // selcall id
                    footer.text = "Input Selcall ID";
                    char[] id = SelCalllabelID.text.ToCharArray();
                    id[cursorIndex] = input;
                    SelCalllabelID.text = new string(id);
                    cursorIndex++;
                    cursorIndex = cursorIndex >= 6 ? 6 : cursorIndex;

                }

                break;
        }

    }
    public static void SellCallSearchEntrySetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        switch (buttonID)
        {
            case ButtonKeyCode.UP_ARROW:
                radio.addressBookIndex++;
                radio.addressBookIndex = radio.addressBookIndex < radio.adressBook.Count() ? radio.addressBookIndex : 0;
                radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label;
                radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SellCallID").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].isLinked == true ? "Yes" : "NO";
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.addressBookIndex--;
                radio.addressBookIndex = radio.addressBookIndex < 0 ? radio.adressBook.Count() - 1 : radio.addressBookIndex;
                radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label;
                radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SellCallID").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
                radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].isLinked == true ? "Yes" : "NO";
                break;
            case ButtonKeyCode.CANCEL:
                radio.displayControllerBarrett2090.SellCallIdBook.transform.Find("NamePanel").Find("InLineArrows").gameObject.SetActive(false);
                radio.displayControllerBarrett2090.SellCallIdBook.SetActive(false);
                radio.Navigator.Pop();
                break;
        }
    }
    /*==========================
    Barret2090 End
    ==========================*/
    /*==========================
    Barret2050
    ==========================*/
    //public static void SellCallAddEntrySetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text SelCallNameText = radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>();
    //    Text SelCalllabelID = radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SellCallID").GetComponent<Text>();
    //    Text footer = radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("Footer").GetComponent<Text>();
    //    Text linkID = radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>();

    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.OK:

    //            if (SelCallNameText.text.Equals("")) // empty string check
    //                return;

    //            if (SelCalllabelID.text.Equals("------"))  // ignore ------ das
    //                return;

    //            counter_stage++;     // stage increment for one panel to another panel
    //            if (counter_stage == 1)
    //            {
    //                // selcall Id panel
    //                cursorIndex = 0;
    //                SelCalllabelID.text = "------";   // 
    //            }
    //            else if (counter_stage == 2)
    //            {
    //                // linked Id status
    //                radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>().text = "No";
    //                footer.text = "Input Link Status";

    //                // set sellcall ID value without das
    //                if (cursorIndex == 4)
    //                {
    //                    SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 2);  // ignore last 2 das
    //                }
    //                else if (cursorIndex == 5)
    //                {
    //                    SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 1);   //ignore last das
    //                    SelCalllabelID.text = SelCalllabelID.text.Insert(0, "0");        // insert 0 at first index        
    //                }
    //                else if (cursorIndex == 3)
    //                {
    //                    SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 3);   // ignore last 3 das
    //                    SelCalllabelID.text = SelCalllabelID.text.Insert(0, "0");      // add 0 in first index like 0123
    //                }
    //                else if (cursorIndex == 2)
    //                {
    //                    SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 4);  // ignore last 4 das
    //                    SelCalllabelID.text = SelCalllabelID.text.Insert(0, "00");       // add 00 in first index
    //                }
    //                else if (cursorIndex == 1)
    //                {
    //                    SelCalllabelID.text = SelCalllabelID.text.Remove(SelCalllabelID.text.Length - 5);   // ignore last 5 das
    //                    SelCalllabelID.text = SelCalllabelID.text.Insert(0, "000");     // add 000 in first index
    //                }


    //            }
    //            else
    //            {
    //                // set all value in contact list
    //                radio.adressBook.Add(new Contact(SelCallNameText.text, int.Parse(SelCalllabelID.text), linkID.text == "Yes" ? true : false));
    //                radio.displayControllerBarrett2050.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = radio.adressBook.Count == 0 ? "Empty" : radio.adressBook.Count + " Entries";
    //                radio.displayControllerBarrett2050.SellCallIdBook.SetActive(false);
    //                radio.Navigator.Pop();
    //                counter_stage = 0;
    //            }


    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (counter_stage == 0 && SelCallNameText.text.Length > 0)  // selcall name entry stage
    //            {

    //                SelCallNameText.text = SelCallNameText.text.Remove(SelCallNameText.text.Length - 1);  // decrement last digit

    //            }
    //            else if (counter_stage == 1 && cursorIndex > 0)    // selcall ID stage
    //            {

    //                char[] id = SelCalllabelID.text.ToCharArray();
    //                id[cursorIndex - 1] = '-';                 // replace digit with das
    //                SelCalllabelID.text = new string(id);  // set text
    //                cursorIndex--;                         // increment cursor index
    //                cursorIndex = cursorIndex < 0 ? counter_stage++ : cursorIndex;  // check cursor index less than 0

    //            }
    //            else
    //            {
    //                // return back in address book panel
    //                radio.displayControllerBarrett2050.SellCallIdBook.SetActive(false);
    //                radio.Navigator.Pop();
    //                counter_stage = 0;
    //            }
    //            break;

    //        case ButtonKeyCode.UP_ARROW:
    //            if (counter_stage == 2) // link id status
    //            {
    //                linkID.text = linkID.text == "Yes" ? "No" : "Yes"; // change yes and no
    //            }

    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            if (counter_stage == 2) // link id status
    //            {
    //                linkID.text = linkID.text == "Yes" ? "No" : "Yes";   // change yes and no
    //            }
    //            break;
    //        default:
    //            char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);  // keypad input
    //            if (counter_stage == 0 && SelCallNameText.text.Length < 16)
    //            {
    //                //name entry
    //                SelCallNameText.text += input.ToString();
    //            }
    //            else if (counter_stage == 1 && cursorIndex < 6)
    //            {
    //                // selcall id
    //                footer.text = "Input Selcall ID";
    //                char[] id = SelCalllabelID.text.ToCharArray();
    //                id[cursorIndex] = input;
    //                SelCalllabelID.text = new string(id);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= 6 ? 6 : cursorIndex;

    //            }

    //            break;
    //    }

    //}
    //public static void SellCallSearchEntrySetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            radio.addressBookIndex++;
    //            radio.addressBookIndex = radio.addressBookIndex < radio.adressBook.Count() ? radio.addressBookIndex : 0;
    //            radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label;
    //            radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SellCallID").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].isLinked == true ? "Yes" : "NO";
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.addressBookIndex--;
    //            radio.addressBookIndex = radio.addressBookIndex < 0 ? radio.adressBook.Count() - 1 : radio.addressBookIndex;
    //            radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].Label;
    //            radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("SellCallIDPanel").Find("SellCallID").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].ID.ToString();
    //            radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("LinkedID").Find("ID").GetComponent<Text>().text = radio.adressBook[radio.addressBookIndex].isLinked == true ? "Yes" : "NO";
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.displayControllerBarrett2050.SellCallIdBook.transform.Find("NamePanel").Find("InLineArrows").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.SellCallIdBook.SetActive(false);
    //            radio.Navigator.Pop();
    //            break;
    //    }
    //}
    /*==========================
    Barret2050 End
    ==========================*/
}
