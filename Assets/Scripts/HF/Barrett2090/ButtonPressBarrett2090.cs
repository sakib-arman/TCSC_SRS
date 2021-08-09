using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(MeshCollider))]
public class ButtonPressBarrett2090 : MonoBehaviour
{
    private Barrett2090 barrett;

    // public string ButtonID;
    public ButtonKeyCode ButtonID = ButtonKeyCode.NUM_0;
    Button button;
    public float delay = 3;
    //Multiclick Parameter
    public bool isMultiClickable = true;
    public float DoubleClickTimeout = 0.1f;
    private bool clickEnable = true;
    private int clickCounter = 1;
    private bool isProtectedMode = false;
    private float endTime = 0;
    private bool isButtonDown = false;
    void Start()
    {
        // barrett = gameObject.GetComponent(typeof(Barrett2090)) as Barrett2090;
        barrett = this.GetComponentInParent<HandsetInfo>().barrett;
        button = this.GetComponent<Button>();
        button?.onClick.AddListener(UiButtonClick);

    }
    private void OnMouseDown()
    {
        delay = Time.time;
        endTime = Time.time + 2f;
        isButtonDown = true;
    }
    private void Update()
    {

        if (endTime < Time.time && isButtonDown)
        {
            ButtonKeyCode secondButton = ButtonID;
            isProtectedMode = true;
            isButtonDown = false;
            delay = Time.time - delay;
            switch (ButtonID)
            {
                case ButtonKeyCode.MENU:
                    secondButton = ButtonKeyCode.MENU;
                    break;
                case ButtonKeyCode.NUM_9:
                    secondButton = ButtonKeyCode.SCAN;
                    break;
                case ButtonKeyCode.NUM_8:
                    secondButton = ButtonKeyCode.SCRAM;
                    break;
            }
            barrett?.onPressed(secondButton, delay);

        }
    }
    private void OnMouseUp()
    {
        isButtonDown = false;
        delay = Time.time - delay;
        if (isProtectedMode)
        {
            isProtectedMode = false;
            return;
        }

        if (barrett.keyboardType == KeyboardType.CHARACTER)
        {
            if (clickEnable)
            {
                clickEnable = false;
                StartCoroutine(Multiclick(DoubleClickTimeout));
            }
        }
        else
        {
            //if(ButtonID == ButtonKeyCode.TUNE)
            //{
            //    if (delay > 1)
            //    {
            //        ButtonID = ButtonKeyCode.CAPS_LOCK;
            //    }
            //}
            barrett?.onPressed(ButtonID, delay);
        }


    }
    private void UiButtonClick()
    {
        // Debug.Log(ButtonID);
        barrett?.onPressed(ButtonID, delay);

    }
    IEnumerator Multiclick(float timer)
    {


        float endTime = Time.time + timer;
        while (Time.time < endTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickCounter++;
                endTime += timer;

                //yield return new WaitForSeconds(0);
                //clickEnable = true;
                //doubleClick = true;

            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.1f);
        ButtonKeyCode tempId = ButtonID;
        if (clickCounter == 1)
        {
            //Singel Click

            switch (ButtonID)
            {
                case ButtonKeyCode.NUM_2:
                    tempId = ButtonKeyCode.CHAR_A;
                    break;
                case ButtonKeyCode.NUM_3:
                    tempId = ButtonKeyCode.CHAR_D;

                    break;
                case ButtonKeyCode.NUM_4:
                    tempId = ButtonKeyCode.CHAR_G;

                    break;
                case ButtonKeyCode.NUM_5:
                    tempId = ButtonKeyCode.CHAR_J;

                    break;
                case ButtonKeyCode.NUM_6:
                    tempId = ButtonKeyCode.CHAR_M;

                    break;
                case ButtonKeyCode.NUM_7:
                    tempId = ButtonKeyCode.CHAR_P;

                    break;
                case ButtonKeyCode.NUM_8:
                    tempId = ButtonKeyCode.CHAR_T;

                    break;
                case ButtonKeyCode.NUM_9:
                    tempId = ButtonKeyCode.CHAR_W;
                    break;
                case ButtonKeyCode.NUM_0:
                    tempId = ButtonKeyCode.WHITE_SPACE;
                    break;
                case ButtonKeyCode.TUNE:
                    tempId = ButtonKeyCode.CAPS_LOCK;
                    break;

            }



        }
        else if (clickCounter == 2)
        {
            //Double Click


            switch (ButtonID)
            {
                case ButtonKeyCode.NUM_2:
                    tempId = ButtonKeyCode.CHAR_B;
                    break;
                case ButtonKeyCode.NUM_3:
                    tempId = ButtonKeyCode.CHAR_E;

                    break;
                case ButtonKeyCode.NUM_4:
                    tempId = ButtonKeyCode.CHAR_H;

                    break;
                case ButtonKeyCode.NUM_5:
                    tempId = ButtonKeyCode.CHAR_K;

                    break;
                case ButtonKeyCode.NUM_6:
                    tempId = ButtonKeyCode.CHAR_N;

                    break;
                case ButtonKeyCode.NUM_7:
                    tempId = ButtonKeyCode.CHAR_Q;

                    break;
                case ButtonKeyCode.NUM_8:
                    tempId = ButtonKeyCode.CHAR_U;

                    break;
                case ButtonKeyCode.NUM_9:
                    tempId = ButtonKeyCode.CHAR_X;
                    break;
            }


        }
        else if (clickCounter == 3)
        {


            switch (ButtonID)
            {
                case ButtonKeyCode.NUM_2:
                    tempId = ButtonKeyCode.CHAR_C;
                    break;
                case ButtonKeyCode.NUM_3:
                    tempId = ButtonKeyCode.CHAR_F;

                    break;
                case ButtonKeyCode.NUM_4:
                    tempId = ButtonKeyCode.CHAR_I;

                    break;
                case ButtonKeyCode.NUM_5:
                    tempId = ButtonKeyCode.CHAR_L;

                    break;
                case ButtonKeyCode.NUM_6:
                    tempId = ButtonKeyCode.CHAR_O;

                    break;
                case ButtonKeyCode.NUM_7:
                    tempId = ButtonKeyCode.CHAR_R;

                    break;
                case ButtonKeyCode.NUM_8:
                    tempId = ButtonKeyCode.CHAR_V;

                    break;
                case ButtonKeyCode.NUM_9:
                    tempId = ButtonKeyCode.CHAR_Y;
                    break;
            }



        }
        else
        {

            switch (ButtonID)
            {
                case ButtonKeyCode.NUM_7:
                    tempId = ButtonKeyCode.CHAR_S;
                    break;
                case ButtonKeyCode.NUM_9:
                    tempId = ButtonKeyCode.CHAR_Z;
                    break;
            }


        }
        barrett?.onPressed(tempId, delay);
        clickEnable = true;
        clickCounter = 1;

    }

}
