using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject menuBack;
    public GameObject modeBackground;

    public void menuEnable() 
    {
        menuBack.SetActive(true);
        modeBackground.SetActive(false);
    }
    public void modeEnable()
    {
        modeBackground.SetActive(true);
        menuBack.SetActive(false);
    }
}
