using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;//어느 서버에 접속했을 때 이벤트를 호출하는 라이브러리
public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField roomName, roomPerson;
    [SerializeField] Button roomCreate, roomJoin;

    public GameObject RoomPrefab;
    public Transform RoomContent;

    // 시간 복잡도가 0의 시간 복잡도를 가집니다.
    //Dictionary : Key Value 형태의 값을 저장할 수 있는 자료구조입니다.
    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();



    
    void Update()
    {
        //방 이름을 하나라도 입력했다면 
        if (roomName.text.Length > 0)
            //방 접속 버튼을 활성화 합니다.
            roomJoin.interactable = true;
        else
            
            roomJoin.interactable = false;
        if (roomName.text.Length > 0 && roomPerson.text.Length > 0)
            roomCreate.interactable = true;
        else
            roomCreate.interactable = false;
        //방 이름과 방 인원을 입력하지 않으면 
       if(roomName.text.Length>0 && roomPerson.text.Length>0)
        {
            //방 생성 버튼 활성화 
            roomCreate.interactable = true;

        }
        else
        {    // 방 생성 버튼 비 활성화
            roomCreate.interactable = false;
        }

    }

    //OnJoinRoomFailed: 방 접속에 실패했을때 호출되는함수
    //방 접속에 실패 하게 되면 자동으로 하나 생성해서 들어가는 방법이 있습니다. 
    

    //포톤 룸은 최대 10명 까지만 접속 할수있도록 설정됨
    public void OnClickCreateRoom()
    {
        //룸 옵션을 설정합니다.
        RoomOptions Room = new RoomOptions();

        //최대 접속자의 수를 설정합니다
        Room.MaxPlayers = byte.Parse(roomPerson.text);

        //룸의 오픈 여부를 설정합니다
        Room.IsOpen = true;

        //로비에서 룸 목록을 노출 시킬지 설정합니다
        Room.IsVisible = true;

        //룸을 생성하는 함수
        PhotonNetwork.CreateRoom(roomName.text, Room);
    }    
    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    //룸이 생성 되었을때 생성되는 콜백함수.

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    public void AllDeleteRoom()
    {
        //transform 오브젝트에  있는 하위 오브젝트에 접근하여 전체 룸을 삭제합니다.
        foreach(Transform element in RoomContent)
        {
            //transform 이 가지고 있는 게임 오브젝트를 삭제합니다.
            Destroy(element.gameObject);
        }

    }

    //룸에 입장했을때 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }
    public void CreateRoomObject()
    {
        //roomCatalog 에 여러개의 value 값이 들어가 있다면 roomInfo 에 넣어줍니다.

        foreach(RoomInfo info in roomCatalog.Values)
        {
            //룸을 생성합니다.
            GameObject room = Instantiate(RoomPrefab);

            //roomContent 의 하위 오브젝트로 넣어줍니다. 
            room.transform.SetParent(RoomContent);

            //룸 정보를 입력합니다. 
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    // 룸 목록을 업데이트하는 함수 
    public void UpdateRoom(List<RoomInfo>roomList)
    {
        for(int i=0;i<roomList.Count;i++)
        {
          // 해당 이름이 roomcatalog의 키로 값으로 설정이 되어있다면 
            if(roomCatalog.ContainsKey(roomList[i].Name))
            {
                //removeFromlist : 룸에서 삭제가 되었을때
                if(roomList[i].RemovedFromList)
                {
                    //딕셔너리에 있는 데이터를 삭제합니다.
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            //룸이 없다면 Roominfor를 roomcatalog에 추가해줍니다.
            roomCatalog[roomList[i].Name] = roomList[i];

        }

    }
    //해당 로비에 방 목록의 변경사항이 있으면 호출(추가 삭제 참가)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }
}
