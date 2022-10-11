using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectSever : MonoBehaviourPunCallbacks
{
    private string serverName;
    [SerializeField] GameObject[] character;

    private void Start()
    {
        character[DataManager.characterCount].SetActive(true); 
        

    }
  public void RightCharacterSelect()
    {
        DataManager.characterCount++;
        //for 문을 돌면서 게임 오브젝트 (캐릭터 )전체를 비활성화합니다
        for(int i=0;i<character.Length;i++)
        {
            
            character[i].SetActive(false);
        }

        
        character[DataManager.characterCount].SetActive(true);
        if(DataManager.characterCount>=2)
        {
            DataManager.characterCount = -1;
        }
    }
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
