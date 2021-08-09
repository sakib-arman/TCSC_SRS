
using System;
using System.IO;
using UnityEngine;
// Author Arnob Monir
public class GLOBALTAG : MonoBehaviour
{
    public static float TIMESCALE = 1;
    public static string RADIO_HF = "HF";
    public static string RADIO_VHF = "VHF";
    public static string RADIO_UHF = "UHF";
    // Debugger
    public static bool isDebug = true;
    public static string LogFileName = Application.persistentDataPath + "/" + (System.DateTime.Now.ToString("dd_MMMM_yyyy_HH_mm")) + ".log";
}
