using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonControl : MonoBehaviour
{

    public GameObject HfArrow;
    public GameObject VhfArrow;
    public GameObject UhfArrow;
    public GameObject RrfArrow;
    public GameObject MwfArrow;

    public void HfArrowControl()
    {
        HfArrow.SetActive(true);
        VhfArrow.SetActive(false);
        UhfArrow.SetActive(false);
        RrfArrow.SetActive(false);
        MwfArrow.SetActive(false);
    }
    public void VhfArrowControl()
    {
        HfArrow.SetActive(false);
        VhfArrow.SetActive(true);
        UhfArrow.SetActive(false);
        RrfArrow.SetActive(false);
        MwfArrow.SetActive(false);
    }
    public void UhfArrowControl()
    {
        HfArrow.SetActive(false);
        VhfArrow.SetActive(false);
        UhfArrow.SetActive(true);
        RrfArrow.SetActive(false);
        MwfArrow.SetActive(false);
    }
    public void RrArrowControl()
    {
        HfArrow.SetActive(false);
        VhfArrow.SetActive(false);
        UhfArrow.SetActive(false);
        RrfArrow.SetActive(true);
        MwfArrow.SetActive(false);
    }
    public void MwfArrowControl()
    {
        HfArrow.SetActive(false);
        VhfArrow.SetActive(false);
        UhfArrow.SetActive(false);
        RrfArrow.SetActive(false);
        MwfArrow.SetActive(true);
    }
}
