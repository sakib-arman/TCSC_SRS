//using UnityEngine;

//using System;
//using System.Linq;
//using Photon.Voice.PUN;
//using Photon.Voice.Unity;

//public class VoiceManeger : MonoBehaviour
//{

//    public Recorder recorder;
//    public byte chanelname;
//    byte[] allByteValues = Enumerable.Range(1, 255).SelectMany(BitConverter.GetBytes).ToArray();
//    private byte[] groupsToAdd = new byte[] { 0, 3, 2 };

//    public void ChangeChannel()
//    {
//        PhotonVoiceNetwork.Instance.Client.ChangeAudioGroups(null, groupsToAdd);
//        recorder.AudioGroup = chanelname;

//    }
//}
