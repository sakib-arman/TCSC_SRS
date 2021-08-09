//using Photon.Pun;
//using Photon.Realtime;
//using System.Collections.Generic;
//using System;

//public class NetworkManager : MonoBehaviourPunCallbacks
//{
//    public static NetworkManager Instance { get; private set; }
//    private void Awake()
//    {
//        if (Instance == null && Instance != this)
//        {
//            Instance = this;
//            Logger.Log("Photon Initiated");
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//        PhotonNetwork.AutomaticallySyncScene = true;

//    }
//    private void Start()
//    {
//        PhotonNetwork.GameVersion = "1.0.0";
//        PhotonNetwork.ConnectToMaster("192.168.1.100", 5055, "No Id Needed");
//        Logger.Log("Connecting to...." + PhotonNetwork.ServerAddress);
//    }
//    public override void OnConnectedToMaster()
//    {
//        Logger.Log("Connected to " + PhotonNetwork.ServerAddress);
//        Guid myuuid = Guid.NewGuid();
//        PhotonNetwork.LocalPlayer.NickName = myuuid.ToString();
//        RoomOptions options = new RoomOptions();
//        options.MaxPlayers = 4;
//        PhotonNetwork.JoinOrCreateRoom("TCSC", options, TypedLobby.Default);
//        Logger.Log("Finding Rooms...");
//    }
//    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
//    {
//        foreach (object d in data)
//        {
//            Logger.Log("Auth Response " + d);
//        }

//    }
//    public override void OnCustomAuthenticationFailed(string debugMessage)
//    {
//        Logger.LogError("Custom Authentication Failed for reason " + debugMessage);
//    }

//    public override void OnDisconnected(DisconnectCause cause)
//    {
//        Logger.LogError("Connection Broken for " + cause);
//    }
//    public override void OnCreatedRoom()
//    {
//        Logger.Log(PhotonNetwork.CurrentRoom.Name + " named room created");
//    }
//    public override void OnCreateRoomFailed(short returnCode, string message)
//    {
//        Logger.Log(returnCode + "Room creation failed for reason " + message);
//    }
//    public override void OnJoinedRoom()
//    {

//        Logger.Log("Joined in room named " + PhotonNetwork.CurrentRoom.Name);
//        Logger.Log("Current Player in room " + PhotonNetwork.PlayerList.Length);
//        Logger.Log("My name : " + PhotonNetwork.LocalPlayer.NickName + " My ID " + PhotonNetwork.LocalPlayer.ActorNumber);

//    }
//    public override void OnJoinRoomFailed(short returnCode, string message)
//    {
//        base.OnJoinRoomFailed(returnCode, message);
//        Logger.Log("Joining Failed for " + message);
//    }
//    public override void OnJoinedLobby()
//    {
//        Logger.Log("Now in lobby");
//    }
//    public override void OnPlayerEnteredRoom(Player newPlayer)
//    {
//        Logger.Log(newPlayer.NickName + "  Joined now with actor ID " + newPlayer.ActorNumber);
//    }
//    public override void OnPlayerLeftRoom(Player otherPlayer)
//    {
//        Logger.Log(otherPlayer.NickName + "  Joined now with actor ID " + otherPlayer.ActorNumber);
//    }


//}
