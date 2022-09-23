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
        //���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Update()
    {
        //���� ���� ������ ��� �˴ϴ�.
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
    //���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ� 
    //�κ� �����ߴ��� Ȯ���Ҽ� �ִ� �Լ� �Դϴ�.
    public override void OnConnectedToMaster()
    {
        //Ư�� �κ� �����Ͽ� �����ϴ� ���

        switch(Data.count)
        {
            case 0:PhotonNetwork.JoinLobby(new TypedLobby("Lobby 1", LobbyType.Default));
                break;
            case 1:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 2", LobbyType.Default));
                break;
            case 2:
                PhotonNetwork.JoinLobby(new TypedLobby("Lobby 3", LobbyType.Default));
                break;

        }
    }
    //�κ� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedLobby()
    {
        //PhotonNetwork.LoadLevel�� ����ϴ� ������ ���� ��Ʈ��ũ���� ���� ����ȭ�� ���߱� ���ؼ� ����մϴ�. 
        PhotonNetwork.LoadLevel("Photon Room");
    }


}
