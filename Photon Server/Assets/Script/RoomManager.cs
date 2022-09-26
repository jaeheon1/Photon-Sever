using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;//��� ������ �������� �� �̺�Ʈ�� ȣ���ϴ� ���̺귯��
public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField roomName, roomPerson;
    [SerializeField] Button roomCreate, roomJoin;

    public GameObject RoomPrefab;
    public Transform RoomContent;

    // �ð� ���⵵�� 0�� �ð� ���⵵�� �����ϴ�.
    //Dictionary : Key Value ������ ���� ������ �� �ִ� �ڷᱸ���Դϴ�.
    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();



    
    void Update()
    {
        //�� �̸��� �ϳ��� �Է��ߴٸ� 
        if (roomName.text.Length > 0)
            //�� ���� ��ư�� Ȱ��ȭ �մϴ�.
            roomJoin.interactable = true;
        else
            
            roomJoin.interactable = false;
        if (roomName.text.Length > 0 && roomPerson.text.Length > 0)
            roomCreate.interactable = true;
        else
            roomCreate.interactable = false;
        //�� �̸��� �� �ο��� �Է����� ������ 
       if(roomName.text.Length>0 && roomPerson.text.Length>0)
        {
            //�� ���� ��ư Ȱ��ȭ 
            roomCreate.interactable = true;

        }
        else
        {    // �� ���� ��ư �� Ȱ��ȭ
            roomCreate.interactable = false;
        }

    }

    //OnJoinRoomFailed: �� ���ӿ� ���������� ȣ��Ǵ��Լ�
    //�� ���ӿ� ���� �ϰ� �Ǹ� �ڵ����� �ϳ� �����ؼ� ���� ����� �ֽ��ϴ�. 
    

    //���� ���� �ִ� 10�� ������ ���� �Ҽ��ֵ��� ������
    public void OnClickCreateRoom()
    {
        //�� �ɼ��� �����մϴ�.
        RoomOptions Room = new RoomOptions();

        //�ִ� �������� ���� �����մϴ�
        Room.MaxPlayers = byte.Parse(roomPerson.text);

        //���� ���� ���θ� �����մϴ�
        Room.IsOpen = true;

        //�κ񿡼� �� ����� ���� ��ų�� �����մϴ�
        Room.IsVisible = true;

        //���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomName.text, Room);
    }    
    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    //���� ���� �Ǿ����� �����Ǵ� �ݹ��Լ�.

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    public void AllDeleteRoom()
    {
        //transform ������Ʈ��  �ִ� ���� ������Ʈ�� �����Ͽ� ��ü ���� �����մϴ�.
        foreach(Transform element in RoomContent)
        {
            //transform �� ������ �ִ� ���� ������Ʈ�� �����մϴ�.
            Destroy(element.gameObject);
        }

    }

    //�뿡 ���������� ȣ��Ǵ� �ݹ��Լ�
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }
    public void CreateRoomObject()
    {
        //roomCatalog �� �������� value ���� �� �ִٸ� roomInfo �� �־��ݴϴ�.

        foreach(RoomInfo info in roomCatalog.Values)
        {
            //���� �����մϴ�.
            GameObject room = Instantiate(RoomPrefab);

            //roomContent �� ���� ������Ʈ�� �־��ݴϴ�. 
            room.transform.SetParent(RoomContent);

            //�� ������ �Է��մϴ�. 
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    // �� ����� ������Ʈ�ϴ� �Լ� 
    public void UpdateRoom(List<RoomInfo>roomList)
    {
        for(int i=0;i<roomList.Count;i++)
        {
          // �ش� �̸��� roomcatalog�� Ű�� ������ ������ �Ǿ��ִٸ� 
            if(roomCatalog.ContainsKey(roomList[i].Name))
            {
                //removeFromlist : �뿡�� ������ �Ǿ�����
                if(roomList[i].RemovedFromList)
                {
                    //��ųʸ��� �ִ� �����͸� �����մϴ�.
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            //���� ���ٸ� Roominfor�� roomcatalog�� �߰����ݴϴ�.
            roomCatalog[roomList[i].Name] = roomList[i];

        }

    }
    //�ش� �κ� �� ����� ��������� ������ ȣ��(�߰� ���� ����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }
}
