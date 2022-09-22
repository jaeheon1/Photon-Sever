using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    public Button connect;
    public Text currentRegion;
    public Text currentLobby;

    public void Connect()
    {
        //서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Update()
    {
        //현재 국가 정보가 등록 됩니다.
        currentRegion.text = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;

        switch(Data.count)
        {
            case 0:currentLobby.text="First Lobby";
                break;
            case 1:currentLobby.text = "Second Lobby";
                break;
            case 2: currentLobby.text = "Third Lobby";
                break;

        }
    }
}
