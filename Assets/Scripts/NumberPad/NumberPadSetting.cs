using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberPadSetting : MonoBehaviour
{
    /*==========================
    Barret2090
    ==========================*/
    public static void NumberPad(Barrett2090 radio, ButtonKeyCode buttonID, Text input, int keypadLength)
    {
        if (input.text.Length >= keypadLength)
        {
            return;
        }
        else
        {
            switch (buttonID)
            {
                //Number pad Button setup 
                case ButtonKeyCode.NUM_0:
                    input.text += "0";
                    break;
                case ButtonKeyCode.NUM_1:
                    input.text += "1";
                    break;
                case ButtonKeyCode.NUM_2:
                    input.text += "2";
                    break;
                case ButtonKeyCode.NUM_3:
                    input.text += "3";
                    break;
                case ButtonKeyCode.NUM_4:
                    input.text += "4";
                    break;
                case ButtonKeyCode.NUM_5:
                    input.text += "5";
                    break;
                case ButtonKeyCode.NUM_6:
                    input.text += "6";
                    break;
                case ButtonKeyCode.NUM_7:
                    input.text += "7";
                    break;
                case ButtonKeyCode.NUM_8:
                    input.text += "8";
                    break;
                case ButtonKeyCode.NUM_9:
                    input.text += "9";
                    break;
                    //Number Pad button setting End 
            }
        }


    }
    public static int cursorIndex = 0;
    public static void NumberPader(Barrett2090 radio, ButtonKeyCode buttonID)
    {
        if (cursorIndex == 5)
            cursorIndex++;

        switch (buttonID)
        {
            //Number pad Button setup 

            case ButtonKeyCode.NUM_0:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('0', cursorIndex);
                cursorIndex++;
                //return 0;
                break;
            case ButtonKeyCode.NUM_1:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('1', cursorIndex);
                cursorIndex++;
                //return 1;
                break;
            case ButtonKeyCode.NUM_2:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('2', cursorIndex);
                cursorIndex++;
                //return 2;
                break;
            case ButtonKeyCode.NUM_3:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('3', cursorIndex);
                cursorIndex++;
                //return 3;
                break;
            case ButtonKeyCode.NUM_4:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('4', cursorIndex);
                cursorIndex++;
                //return 4;
                break;
            case ButtonKeyCode.NUM_5:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('5', cursorIndex);
                cursorIndex++;
                //return 5;
                break;
            case ButtonKeyCode.NUM_6:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('6', cursorIndex);
                cursorIndex++;
                //return 6;
                break;
            case ButtonKeyCode.NUM_7:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('7', cursorIndex);
                cursorIndex++;
                //return 7;
                break;
            case ButtonKeyCode.NUM_8:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('8', cursorIndex);
                cursorIndex++;
                //return 8;
                break;
            case ButtonKeyCode.NUM_9:
                if (cursorIndex >= 9)
                    return;
                radio.displayControllerBarrett2090.SetRxNumber('9', cursorIndex);
                cursorIndex++;
                //return 9;
                break;
                //default:
                //    return 0;
                //    //Number Pad button setting End 
        }

    }
    /*==========================
    Barret2090 End
    ==========================*/
    /*==========================
    Barret2050
    ==========================*/
    //public static void NumberPad(Barrett2050 radio, ButtonKeyCode buttonID, Text input, int keypadLength)
    //{
    //    if (input.text.Length >= keypadLength)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        switch (buttonID)
    //        {
    //            //Number pad Button setup 
    //            case ButtonKeyCode.NUM_0:
    //                input.text += "0";
    //                break;
    //            case ButtonKeyCode.NUM_1:
    //                input.text += "1";
    //                break;
    //            case ButtonKeyCode.NUM_2:
    //                input.text += "2";
    //                break;
    //            case ButtonKeyCode.NUM_3:
    //                input.text += "3";
    //                break;
    //            case ButtonKeyCode.NUM_4:
    //                input.text += "4";
    //                break;
    //            case ButtonKeyCode.NUM_5:
    //                input.text += "5";
    //                break;
    //            case ButtonKeyCode.NUM_6:
    //                input.text += "6";
    //                break;
    //            case ButtonKeyCode.NUM_7:
    //                input.text += "7";
    //                break;
    //            case ButtonKeyCode.NUM_8:
    //                input.text += "8";
    //                break;
    //            case ButtonKeyCode.NUM_9:
    //                input.text += "9";
    //                break;
    //                //Number Pad button setting End 
    //        }
    //    }
    //}
    //public static void NumberPader(Barrett2050 radio, ButtonKeyCode buttonID)
    //{
    //    if (cursorIndex == 5)
    //        cursorIndex++;
    //    switch (buttonID)
    //    {
    //        //Number pad Button setup 
    //        case ButtonKeyCode.NUM_0:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('0', cursorIndex);
    //            cursorIndex++;
    //            //return 0;
    //            break;
    //        case ButtonKeyCode.NUM_1:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('1', cursorIndex);
    //            cursorIndex++;
    //            //return 1;
    //            break;
    //        case ButtonKeyCode.NUM_2:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('2', cursorIndex);
    //            cursorIndex++;
    //            //return 2;
    //            break;
    //        case ButtonKeyCode.NUM_3:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('3', cursorIndex);
    //            cursorIndex++;
    //            //return 3;
    //            break;
    //        case ButtonKeyCode.NUM_4:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('4', cursorIndex);
    //            cursorIndex++;
    //            //return 4;
    //            break;
    //        case ButtonKeyCode.NUM_5:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('5', cursorIndex);
    //            cursorIndex++;
    //            //return 5;
    //            break;
    //        case ButtonKeyCode.NUM_6:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('6', cursorIndex);
    //            cursorIndex++;
    //            //return 6;
    //            break;
    //        case ButtonKeyCode.NUM_7:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('7', cursorIndex);
    //            cursorIndex++;
    //            //return 7;
    //            break;
    //        case ButtonKeyCode.NUM_8:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('8', cursorIndex);
    //            cursorIndex++;
    //            //return 8;
    //            break;
    //        case ButtonKeyCode.NUM_9:
    //            if (cursorIndex >= 9)
    //                return;
    //            radio.displayControllerBarrett2050.SetRxNumber('9', cursorIndex);
    //            cursorIndex++;
    //            //return 9;
    //            break;
    //            //default:
    //            //    return 0;
    //            //    //Number Pad button setting End 
    //    }
    //}
    /*==========================
    Barret2050 End
    ==========================*/
    /*==========================
    CommonNumPad
    ==========================*/
    public static char KeyPadInput(bool isCapsLockOn, ButtonKeyCode buttonID)
    {
        switch (buttonID)
        {
            case ButtonKeyCode.CHAR_A:
                return isCapsLockOn ? 'A' : 'a';
            case ButtonKeyCode.CHAR_B:
                return isCapsLockOn ? 'B' : 'b';
            case ButtonKeyCode.CHAR_C:
                return isCapsLockOn ? 'C' : 'c';
            case ButtonKeyCode.CHAR_D:
                return isCapsLockOn ? 'D' : 'd';
            case ButtonKeyCode.CHAR_E:
                return isCapsLockOn ? 'E' : 'e';
            case ButtonKeyCode.CHAR_F:
                return isCapsLockOn ? 'F' : 'f';
            case ButtonKeyCode.CHAR_G:
                return isCapsLockOn ? 'G' : 'g';
            case ButtonKeyCode.CHAR_H:
                return isCapsLockOn ? 'H' : 'h';
            case ButtonKeyCode.CHAR_I:
                return isCapsLockOn ? 'I' : 'i';
            case ButtonKeyCode.CHAR_J:
                return isCapsLockOn ? 'J' : 'j';
            case ButtonKeyCode.CHAR_K:
                return isCapsLockOn ? 'K' : 'k';
            case ButtonKeyCode.CHAR_L:
                return isCapsLockOn ? 'L' : 'l';
            case ButtonKeyCode.CHAR_M:
                return isCapsLockOn ? 'M' : 'm';
            case ButtonKeyCode.CHAR_N:
                return isCapsLockOn ? 'N' : 'n';
            case ButtonKeyCode.CHAR_O:
                return isCapsLockOn ? 'O' : 'o';
            case ButtonKeyCode.CHAR_P:
                return isCapsLockOn ? 'P' : 'p';
            case ButtonKeyCode.CHAR_Q:
                return isCapsLockOn ? 'Q' : 'q';
            case ButtonKeyCode.CHAR_R:
                return isCapsLockOn ? 'R' : 'r';
            case ButtonKeyCode.CHAR_S:
                return isCapsLockOn ? 'S' : 's';
            case ButtonKeyCode.CHAR_T:
                return isCapsLockOn ? 'T' : 't';
            case ButtonKeyCode.CHAR_U:
                return isCapsLockOn ? 'U' : 'u';
            case ButtonKeyCode.CHAR_V:
                return isCapsLockOn ? 'V' : 'v';
            case ButtonKeyCode.CHAR_W:
                return isCapsLockOn ? 'W' : 'w';
            case ButtonKeyCode.CHAR_X:
                return isCapsLockOn ? 'X' : 'x';
            case ButtonKeyCode.CHAR_Y:
                return isCapsLockOn ? 'Y' : 'y';
            case ButtonKeyCode.CHAR_Z:
                return isCapsLockOn ? 'Z' : 'z';
            case ButtonKeyCode.WHITE_SPACE:
                return ' ';
            case ButtonKeyCode.DECIMAL_POINT:
                return '.';
            case ButtonKeyCode.NUM_1:
                return '1';
            case ButtonKeyCode.NUM_2:
                return '2';
            case ButtonKeyCode.NUM_3:
                return '3';
            case ButtonKeyCode.NUM_4:
                return '4';
            case ButtonKeyCode.NUM_5:
                return '5';
            case ButtonKeyCode.NUM_6:
                return '6';
            case ButtonKeyCode.NUM_7:
                return '7';
            case ButtonKeyCode.NUM_8:
                return '8';
            case ButtonKeyCode.NUM_9:
                return '9';
            default:
                return '0';
        }
    }
    /*==========================
    CommonNumPad END
    ==========================*/
}
