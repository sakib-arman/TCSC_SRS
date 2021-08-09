using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhoneBookAddEntry : MonoBehaviour
{
    public static int cursorIndex = 0;
    private static int counter_stage = 0;
    private static string tempName = "";
    /*==========================
    Barret2090 
    ==========================*/
    public static void PhoneBookAddEntrySetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text PhoneNameText = radio.displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>();
        Text PhonelebelID = radio.displayControllerBarrett2090.PhoneBook.transform.Find("PhonePanel").Find("PhoneID").GetComponent<Text>();
        Text footer = radio.displayControllerBarrett2090.PhoneBook.transform.Find("Footer").GetComponent<Text>();

        switch (buttonID)
        {
            case ButtonKeyCode.OK:

                if (PhoneNameText.text.Equals("")) // empty string check
                    return;

                if (PhonelebelID.text.Equals("-------------------"))  // ignore ------ das
                    return;

                counter_stage++;     // stage increment for one panel to another panel
                if (counter_stage == 1)
                {
                    // PhoneBook Id panel
                    cursorIndex = 0;
                    PhonelebelID.text = "-------------------";   // 
                }
                else
                {
                    int countDas = PhonelebelID.text.Count(c => c == '-');  // count total hyphen using LINQ
                    PhonelebelID.text = PhonelebelID.text.Remove(PhonelebelID.text.Length - countDas);
                    // set all value in contact list
                    radio.phoneBook.Add(new Contact(PhoneNameText.text, int.Parse(PhonelebelID.text), true));
                    radio.displayControllerBarrett2090.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = radio.phoneBook.Count == 0 ? "Empty" : radio.phoneBook.Count + " Entries";
                    radio.displayControllerBarrett2090.PhoneBook.SetActive(false);
                    radio.Navigator.Pop();
                    counter_stage = 0;
                }


                break;
            case ButtonKeyCode.CANCEL:
                if (counter_stage == 0 && PhoneNameText.text.Length > 0)  // PhoneBook name entry stage
                {

                    PhoneNameText.text = PhoneNameText.text.Remove(PhoneNameText.text.Length - 1);  // decrement last digit

                }
                else if (counter_stage == 1 && cursorIndex > 0)    // PhoneBook ID stage
                {
                    char[] id = PhonelebelID.text.ToCharArray();
                    id[cursorIndex-1] = '-';                 // replace digit with das
                    PhonelebelID.text = new string(id);  // set text
                    cursorIndex--;                         // increment cursor index
                    cursorIndex = cursorIndex < 0 ? counter_stage++ : cursorIndex;  // check cursor index less than 0
                }

                else
                {
                    // return back in address book panel
                    radio.displayControllerBarrett2090.PhoneBook.SetActive(false);
                    radio.Navigator.Pop();
                    counter_stage = 0;
                }
                break;
            default:
                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);  // keypad input
                if (counter_stage == 0 && PhoneNameText.text.Length < 17)
                {
                    //name entry
                    PhoneNameText.text += input.ToString();
                }
                else if (counter_stage == 1 && cursorIndex < 19)
                {
                    // PhoneBook id
                    footer.text = "Input Phone ID";
                    char[] id = PhonelebelID.text.ToCharArray();
                    id[cursorIndex] = input;
                    PhonelebelID.text = new string(id);
                    cursorIndex++;
                    cursorIndex = cursorIndex >= 19 ? 19 : cursorIndex;

                }

                break;
        }
    }
    public static void PhoneBookSearchEntrySetting(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        switch (buttonID)
        {
            case ButtonKeyCode.UP_ARROW:
                radio.phoneBookIndex++;
                radio.phoneBookIndex = radio.phoneBookIndex < radio.adressBook.Count() ? radio.phoneBookIndex : 0;
                radio.displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = radio.adressBook[radio.phoneBookIndex].Label;
                radio.displayControllerBarrett2090.PhoneBook.transform.Find("PhonePanel").Find("PhoneID").GetComponent<Text>().text = radio.adressBook[radio.phoneBookIndex].ID.ToString();
                break;
            case ButtonKeyCode.DOWN_ARROW:
                radio.phoneBookIndex--;
                radio.phoneBookIndex = radio.phoneBookIndex < 0 ? radio.adressBook.Count() - 1 : radio.phoneBookIndex;
                radio.displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = radio.adressBook[radio.phoneBookIndex].Label;
                radio.displayControllerBarrett2090.PhoneBook.transform.Find("PhonePanel").Find("PhoneID").GetComponent<Text>().text = radio.adressBook[radio.phoneBookIndex].ID.ToString();
                //Logger.Log(radio.adressBook[radio.addressBookIndex].Label);
                break;
            case ButtonKeyCode.CANCEL:
                radio.displayControllerBarrett2090.PhoneBook.transform.Find("NamePanel").Find("InLineArrows").gameObject.SetActive(false);
                radio.displayControllerBarrett2090.PhoneBook.SetActive(false);
                radio.Navigator.Pop();
                break;
        }
    }
    /*==========================
    Barret2090 End
    ==========================*/
    /*==========================
    Barrett2050 
    ==========================*/
    //public static void PhoneBookAddEntrySetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text PhoneNameText = radio.displayControllerBarrett2050.PhoneBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>();
    //    Text PhonelebelID = radio.displayControllerBarrett2050.PhoneBook.transform.Find("PhonePanel").Find("PhoneID").GetComponent<Text>();
    //    Text footer = radio.displayControllerBarrett2050.PhoneBook.transform.Find("Footer").GetComponent<Text>();

    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.OK:

    //            if (PhoneNameText.text.Equals("")) // empty string check
    //                return;

    //            if (PhonelebelID.text.Equals("-------------------"))  // ignore ------ das
    //                return;

    //            counter_stage++;     // stage increment for one panel to another panel
    //            if (counter_stage == 1)
    //            {
    //                // PhoneBook Id panel
    //                cursorIndex = 0;
    //                PhonelebelID.text = "-------------------";   // 
    //            }
    //            else
    //            {
    //                int countDas = PhonelebelID.text.Count(c => c == '-');  // count total hyphen using LINQ
    //                PhonelebelID.text = PhonelebelID.text.Remove(PhonelebelID.text.Length - countDas);
    //                // set all value in contact list
    //                radio.phoneBook.Add(new Contact(PhoneNameText.text, int.Parse(PhonelebelID.text), true));
    //                radio.displayControllerBarrett2050.MenuPanel.transform.Find("SideArrow").Find("Footer").GetComponent<Text>().text = radio.phoneBook.Count == 0 ? "Empty" : radio.phoneBook.Count + " Entries";
    //                radio.displayControllerBarrett2050.PhoneBook.SetActive(false);
    //                radio.Navigator.Pop();
    //                counter_stage = 0;
    //            }


    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            if (counter_stage == 0 && PhoneNameText.text.Length > 0)  // PhoneBook name entry stage
    //            {

    //                PhoneNameText.text = PhoneNameText.text.Remove(PhoneNameText.text.Length - 1);  // decrement last digit

    //            }
    //            else if (counter_stage == 1 && cursorIndex > 0)    // PhoneBook ID stage
    //            {
    //                char[] id = PhonelebelID.text.ToCharArray();
    //                id[cursorIndex-1] = '-';                 // replace digit with das
    //                PhonelebelID.text = new string(id);  // set text
    //                cursorIndex--;                         // increment cursor index
    //                cursorIndex = cursorIndex < 0 ? counter_stage++ : cursorIndex;  // check cursor index less than 0
    //            }

    //            else
    //            {
    //                // return back in address book panel
    //                radio.displayControllerBarrett2050.PhoneBook.SetActive(false);
    //                radio.Navigator.Pop();
    //                counter_stage = 0;
    //            }
    //            break;
    //        default:
    //            char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);  // keypad input
    //            if (counter_stage == 0 && PhoneNameText.text.Length < 17)
    //            {
    //                //name entry
    //                PhoneNameText.text += input.ToString();
    //            }
    //            else if (counter_stage == 1 && cursorIndex < 19)
    //            {
    //                // PhoneBook id
    //                footer.text = "Input Phone ID";
    //                char[] id = PhonelebelID.text.ToCharArray();
    //                id[cursorIndex] = input;
    //                PhonelebelID.text = new string(id);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= 19 ? 19 : cursorIndex;

    //            }

    //            break;
    //    }
    //}
    //public static void PhoneBookSearchEntrySetting(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            radio.phoneBookIndex++;
    //            radio.phoneBookIndex = radio.phoneBookIndex < radio.adressBook.Count() ? radio.phoneBookIndex : 0;
    //            radio.displayControllerBarrett2050.PhoneBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = radio.adressBook[radio.phoneBookIndex].Label;
    //            radio.displayControllerBarrett2050.PhoneBook.transform.Find("PhonePanel").Find("PhoneID").GetComponent<Text>().text = radio.adressBook[radio.phoneBookIndex].ID.ToString();
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            radio.phoneBookIndex--;
    //            radio.phoneBookIndex = radio.phoneBookIndex < 0 ? radio.adressBook.Count() - 1 : radio.phoneBookIndex;
    //            radio.displayControllerBarrett2050.PhoneBook.transform.Find("NamePanel").Find("NameEntry").GetComponent<Text>().text = radio.adressBook[radio.phoneBookIndex].Label;
    //            radio.displayControllerBarrett2050.PhoneBook.transform.Find("PhonePanel").Find("PhoneID").GetComponent<Text>().text = radio.adressBook[radio.phoneBookIndex].ID.ToString();
    //            //Logger.Log(radio.adressBook[radio.addressBookIndex].Label);
    //            break;
    //        case ButtonKeyCode.CANCEL:
    //            radio.displayControllerBarrett2050.PhoneBook.transform.Find("NamePanel").Find("InLineArrows").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.PhoneBook.SetActive(false);
    //            radio.Navigator.Pop();
    //            break;
    //    }
    //}
    /*==========================
    Barret2050 End
    ==========================*/
}
