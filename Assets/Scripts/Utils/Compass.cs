using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    private Transform compass;
    private Transform camTrans;
    void Start()
    {
        compass = this.transform;
        camTrans = Camera.main.transform;

    }

    void Update()
    {


    }
}
