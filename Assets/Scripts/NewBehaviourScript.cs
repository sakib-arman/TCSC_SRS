using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    int cursorIndex = 0;
    string number = "00002534";
    public void Bla()
    {
        //up 
        if (cursorIndex == 0)
        {
            // 00002534
            int num = int.Parse(number) + 1;
            // 2535
            number = num.ToString().PadLeft(8, '0');
            // 00002535
        }

        //down

        //left 
        cursorIndex--;
        //right 
        cursorIndex++;


    }
}
