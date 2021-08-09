using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProfile : MonoBehaviour
{
    public static void MuteAudioSelection(Barrett2090 radio)
    {
        if (!radio.isAudioMuteOn)
        {
            radio.isAudioMuteOn = true;
            Logger.Log(radio, "UnMute");
        }
        else
        {
            radio.isAudioMuteOn = false;
            Logger.Log(radio, "Mute");

        }
    }
    public static void MuteSelectiveCallSelection(Barrett2090 radio)
    {
        if (!radio.isSelectiveCallMuteOn)
        {
            radio.isSelectiveCallMuteOn = true;
            Logger.Log(radio, "Selective Call " + "UnMute");
        }
        else
        {
            radio.isSelectiveCallMuteOn = false;
            Logger.Log(radio, "Selective Call " + "Mute");
        }
    }
    public static void MuteSslSelection(Barrett2090 radio)
    {
        if (!radio.isSslMuteOn)
        {
            radio.isSslMuteOn = true;
            Logger.Log(radio, "Multi SSL Selection Call" + "UnMute");

        }
        else
        {
            radio.isSslMuteOn = false;
            Logger.Log(radio, "Multi SSL Selection Call" + "Mute");
        }
    }
}
