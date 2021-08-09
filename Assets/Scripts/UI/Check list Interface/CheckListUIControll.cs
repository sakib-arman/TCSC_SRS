using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckListUIControll : MonoBehaviour
{
    bool isExpanded = true;
    public void SidebarControll(Animator button) 
    {
       
        if (isExpanded)
        {
            button.SetTrigger("collapse");
            isExpanded = false;

        }
        else
        {
            button.SetTrigger("expand");
            isExpanded = true;
        }
    }
}
