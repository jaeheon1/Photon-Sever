using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectSever : MonoBehaviourPunCallbacks
{
    private string serverName;
   
  
   public void SelectLobby(string text)
    {
        //Challeger Server
        serverName = text;
        PhotonNetwork.ConnectUsingSettings();


    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(new TypedLobby(serverName, LobbyType.Default));
    }
}
