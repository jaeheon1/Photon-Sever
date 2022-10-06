using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectSever : MonoBehaviourPunCallbacks
{
  
    void Start()
    {
        
    }

  
   public void SelectLobby()
    {
        PhotonNetwork.ConnectUsingSettings();


    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(new TypedLobby("Count", LobbyType.Default));
    }
}
