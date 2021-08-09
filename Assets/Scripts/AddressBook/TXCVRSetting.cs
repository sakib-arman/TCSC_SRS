using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TXCVRSetting : MonoBehaviour
{
    public static int cursorIndex = 0;
    public static bool isNumberPadActive = false;
    private static int counter_stage = 0;
    private static int pin_stage = 0;
    public static bool isTransmit = false;
    public static bool isTransmitYes = false;
    /*======================
    Barret2090 Start
    ======================*/
    public static void TxCVRLock(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        Text TxCVR_number = radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>();
        Text PinNumberEntry = radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("PinNumberPanel").Find("PinNumberEntry").GetComponent<Text>();
        Text Transmit = radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("TransmitPanel").Find("Confirmation").GetComponent<Text>();
        switch (buttonID)
        {
            case ButtonKeyCode.UP_ARROW:
                // TxCVR AddressBook SelCall ID UP
                radio.TxCVRIndex++;
                if (radio.TxCVRIndex >= radio.adressBook.Count)
                {
                    radio.TxCVRIndex = 0;
                }
                radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>().text = radio.adressBook[radio.TxCVRIndex].ID.ToString();

                // Confirmation Transmit 
                if (isTransmit && isTransmitYes)
                {
                    Transmit.text = Transmit.text == "No" ? "Yes" : "No";
                }
                break;
            case ButtonKeyCode.DOWN_ARROW:
                // TxCVR AddressBook SelCall ID UP
                radio.TxCVRIndex--;
                if (radio.TxCVRIndex < 0)
                {
                    radio.TxCVRIndex = radio.adressBook.Count - 1;
                }
                radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>().text = radio.adressBook[radio.TxCVRIndex].ID.ToString();

                // Confirmation Transmit 
                if (isTransmit && isTransmitYes)
                {
                    Transmit.text = Transmit.text == "Yes" ? "No" : "Yes";
                }

                break;
            case ButtonKeyCode.OK:
                if (TxCVR_number.text.Equals("------"))  // ignore ------ das
                    return;
                if (PinNumberEntry.text.Equals("--------"))  // ignore ------ das
                    return;
                int countDas = PinNumberEntry.text.Count(c => c == '-');  // count total hyphen using LINQ
                if (countDas > 0)
                {
                    return;
                }

                if (cursorIndex == 4)
                {
                    TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 2);  // ignore last 2 das
                }
                else if (cursorIndex == 5)
                {
                    TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 1);   //ignore last das
                    TxCVR_number.text = TxCVR_number.text.Insert(0, "0");        // insert 0 at first index        
                }
                else if (cursorIndex == 3)
                {
                    TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 3);   // ignore last 3 das
                    TxCVR_number.text = TxCVR_number.text.Insert(0, "0");      // add 0 in first index like 0123
                }
                else if (cursorIndex == 2)
                {
                    TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 4);  // ignore last 4 das
                    TxCVR_number.text = TxCVR_number.text.Insert(0, "00");       // add 00 in first index
                }
                else if (cursorIndex == 1)
                {
                    TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 5);   // ignore last 5 das
                    TxCVR_number.text = TxCVR_number.text.Insert(0, "000");     // add 000 in first index
                }
                print("stage " + counter_stage);
                counter_stage++;

                if (counter_stage == 1)
                {
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>().text = TxCVR_number.text;
                    counter_stage = 0;
                    cursorIndex = 0;
                    isNumberPadActive = false;
                }

                // Confirmation Transmit 
                if (isTransmit && counter_stage == 3)
                {
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("PinNumberPanel").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("TransmitPanel").gameObject.SetActive(true);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("TransmitPanel").Find("Confirmation").GetComponent<Text>().text = "No";
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("TransmitPanel").Find("Header").GetComponent<Text>().text = "Confirm Transmit";
                    isTransmitYes = true;
                }
                if (isTransmitYes && counter_stage == 4)
                {
                    isTransmit = false;
                    isTransmitYes = false;
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("TransmitPanel").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.SetActive(false);
                    radio.Navigator.Pop();
                    counter_stage = 0;
                    cursorIndex = 0;
                    pin_stage = 0;
                    break;

                }
                break;
            case ButtonKeyCode.CALL: // TXCVR Lock Pin
                if (pin_stage == 0)
                {
                    counter_stage = 2;
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("Header").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("Footer").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("AddressBookEntry").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("IDTag").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("IDEntry").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("PinNumberPanel").gameObject.SetActive(true);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("PinNumberPanel").Find("Header").GetComponent<Text>().text = "TXCVR Lock Pin";
                    PinNumberEntry.text = "--------";
                    pin_stage = 1;
                }
                break;

            case ButtonKeyCode.CANCEL:
                if (isNumberPadActive && counter_stage == 0 && cursorIndex > 0)
                {
                    char[] id = TxCVR_number.text.ToCharArray();
                    id[cursorIndex - 1] = '-';                 // replace digit with das
                    TxCVR_number.text = new string(id);  // set text
                    cursorIndex--;                         // increment cursor index
                    cursorIndex = cursorIndex < 0 ? counter_stage++ : cursorIndex;  // check cursor index less than 0
                }
                else if (counter_stage == 2 && cursorIndex > 0)
                {
                    char[] id = PinNumberEntry.text.ToCharArray();
                    id[cursorIndex - 1] = '-';                 // replace digit with das
                    PinNumberEntry.text = new string(id);  // set text
                    cursorIndex--;                         // increment cursor index
                    cursorIndex = cursorIndex < 0 ? 0 : cursorIndex;  // check cursor index less than 0
                    print(counter_stage);
                }
                else if (isTransmit && counter_stage == 3)
                {
                    counter_stage = 2;
                    pin_stage = 0;
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("TransmitPanel").gameObject.SetActive(false);
                    radio.displayControllerBarrett2090.TXCVRPanel.transform.Find("PinNumberPanel").gameObject.SetActive(true);
                }
                else
                {
                    radio.displayControllerBarrett2090.TXCVRPanel.SetActive(false);
                    radio.Navigator.Pop();
                    counter_stage = 0;
                    cursorIndex = 0;
                    pin_stage = 0;
                }
                //radio.displayControllerBarrett2090.TXCVRPanel.SetActive(false);
                //radio.Navigator.Pop();
                break;

            default:
                if (!isNumberPadActive && counter_stage == 0)
                {
                    isNumberPadActive = true;
                    TxCVR_number.text = "------";
                }
                if (counter_stage == 0 && cursorIndex < 6)
                {
                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                    char[] id = TxCVR_number.text.ToCharArray();
                    id[cursorIndex] = input;
                    TxCVR_number.text = new string(id);
                    cursorIndex++;
                    cursorIndex = cursorIndex >= 6 ? 6 : cursorIndex;
                }
                // TXCVR Lock Pin
                if (counter_stage == 2 && cursorIndex < 8)
                {
                    char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
                    char[] id = PinNumberEntry.text.ToCharArray();
                    id[cursorIndex] = input;
                    PinNumberEntry.text = new string(id);
                    cursorIndex++;
                    cursorIndex = cursorIndex >= 8 ? 8 : cursorIndex;
                    isTransmit = true;
                }

                break;
        }
    }
    /*======================
    Barret2090 End
    ======================*/
    /*======================
    Barret2050 Start
    ======================*/
    //public static void TxCVRLock(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    Text TxCVR_number = radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>();
    //    Text PinNumberEntry = radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("PinNumberPanel").Find("PinNumberEntry").GetComponent<Text>();
    //    Text Transmit = radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("TransmitPanel").Find("Confirmation").GetComponent<Text>();
    //    switch (buttonID)
    //    {
    //        case ButtonKeyCode.UP_ARROW:
    //            // TxCVR AddressBook SelCall ID UP
    //            radio.TxCVRIndex++;
    //            if (radio.TxCVRIndex >= radio.adressBook.Count)
    //            {
    //                radio.TxCVRIndex = 0;
    //            }
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>().text = radio.adressBook[radio.TxCVRIndex].ID.ToString();

    //            // Confirmation Transmit 
    //            if (isTransmit && isTransmitYes)
    //            {
    //                Transmit.text = Transmit.text == "No" ? "Yes" : "No";
    //            }
    //            break;
    //        case ButtonKeyCode.DOWN_ARROW:
    //            // TxCVR AddressBook SelCall ID UP
    //            radio.TxCVRIndex--;
    //            if (radio.TxCVRIndex < 0)
    //            {
    //                radio.TxCVRIndex = radio.adressBook.Count - 1;
    //            }
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>().text = radio.adressBook[radio.TxCVRIndex].ID.ToString();

    //            // Confirmation Transmit 
    //            if (isTransmit && isTransmitYes)
    //            {
    //                Transmit.text = Transmit.text == "Yes" ? "No" : "Yes";
    //            }

    //            break;
    //        case ButtonKeyCode.OK:
    //            if (TxCVR_number.text.Equals("------"))  // ignore ------ das
    //                return;
    //            if (PinNumberEntry.text.Equals("--------"))  // ignore ------ das
    //                return;
    //            int countDas = PinNumberEntry.text.Count(c => c == '-');  // count total hyphen using LINQ
    //            if (countDas > 0)
    //            {
    //                return;
    //            }

    //            if (cursorIndex == 4)
    //            {
    //                TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 2);  // ignore last 2 das
    //            }
    //            else if (cursorIndex == 5)
    //            {
    //                TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 1);   //ignore last das
    //                TxCVR_number.text = TxCVR_number.text.Insert(0, "0");        // insert 0 at first index        
    //            }
    //            else if (cursorIndex == 3)
    //            {
    //                TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 3);   // ignore last 3 das
    //                TxCVR_number.text = TxCVR_number.text.Insert(0, "0");      // add 0 in first index like 0123
    //            }
    //            else if (cursorIndex == 2)
    //            {
    //                TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 4);  // ignore last 4 das
    //                TxCVR_number.text = TxCVR_number.text.Insert(0, "00");       // add 00 in first index
    //            }
    //            else if (cursorIndex == 1)
    //            {
    //                TxCVR_number.text = TxCVR_number.text.Remove(TxCVR_number.text.Length - 5);   // ignore last 5 das
    //                TxCVR_number.text = TxCVR_number.text.Insert(0, "000");     // add 000 in first index
    //            }

    //            counter_stage++;

    //            if (counter_stage == 1)
    //            {
    //                radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("IDEntry").GetComponent<Text>().text = TxCVR_number.text;
    //                counter_stage = 0;
    //                cursorIndex = 0;
    //                isNumberPadActive = false;
    //            }

    //            // Confirmation Transmit 
    //            if (isTransmit && counter_stage == 3)
    //            {
    //                radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("PinNumberPanel").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("TransmitPanel").gameObject.SetActive(true);
    //                radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("TransmitPanel").Find("Confirmation").GetComponent<Text>().text = "No";
    //                radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("TransmitPanel").Find("Header").GetComponent<Text>().text = "Confirm Transmit";
    //                isTransmitYes = true;
    //            }
    //            if (isTransmitYes && counter_stage == 4)
    //            {
    //                isTransmit = false;
    //                isTransmitYes = false;
    //                radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("TransmitPanel").gameObject.SetActive(false);
    //                radio.displayControllerBarrett2050.TXCVRPanel.SetActive(false);
    //                radio.Navigator.Pop();
    //                counter_stage = 0;
    //                cursorIndex = 0;
    //                break;

    //            }
    //            break;
    //        case ButtonKeyCode.CALL: // TXCVR Lock Pin
    //            counter_stage = 2;
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("Header").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("Footer").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("AddressBookEntry").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("IDTag").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("IDEntry").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("InLineArrows").gameObject.SetActive(false);
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("PinNumberPanel").gameObject.SetActive(true);
    //            radio.displayControllerBarrett2050.TXCVRPanel.transform.Find("PinNumberPanel").Find("Header").GetComponent<Text>().text = "TXCVR Lock Pin";
    //            PinNumberEntry.text = "--------";
    //            break;

    //        case ButtonKeyCode.CANCEL:
    //            if (isNumberPadActive && counter_stage == 0)
    //            {
    //                char[] id = TxCVR_number.text.ToCharArray();
    //                id[cursorIndex - 1] = '-';                 // replace digit with das
    //                TxCVR_number.text = new string(id);  // set text
    //                cursorIndex--;                         // increment cursor index
    //                cursorIndex = cursorIndex < 0 ? counter_stage++ : cursorIndex;  // check cursor index less than 0
    //            }
    //            //radio.displayControllerBarrett2050.TXCVRPanel.SetActive(false);
    //            //radio.Navigator.Pop();
    //            break;

    //        default:
    //            if (!isNumberPadActive && counter_stage == 0)
    //            {
    //                isNumberPadActive = true;
    //                TxCVR_number.text = "------";
    //            }
    //            if (counter_stage == 0 && cursorIndex < 6)
    //            {
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                char[] id = TxCVR_number.text.ToCharArray();
    //                id[cursorIndex] = input;
    //                TxCVR_number.text = new string(id);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= 6 ? 6 : cursorIndex;
    //            }
    //            // TXCVR Lock Pin
    //            if (counter_stage == 2 && cursorIndex < 8)
    //            {
    //                char input = NumberPadSetting.KeyPadInput(radio.isCapsLockOn, buttonID);
    //                char[] id = PinNumberEntry.text.ToCharArray();
    //                id[cursorIndex] = input;
    //                PinNumberEntry.text = new string(id);
    //                cursorIndex++;
    //                cursorIndex = cursorIndex >= 8 ? 8 : cursorIndex;
    //                isTransmit = true;
    //            }

    //            break;
    //    }
    //}
    /*======================
    Barret2090 End
    ======================*/
}
