using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameManager : MonoBehaviourPunCallbacks
{
   
    void Start()
    {
        PhotonNetwork.Instantiate
            ("Character",
            new Vector3(Random.Range(0, 5),
            1,
            Random.Range(0, 5)),
            
            Quaternion.identity);
    }
    public void ExitRoom()
    {
        //현재 룸에서 나가는 함수입니다.
        PhotonNetwork.LeaveRoom();
    }
    //현재 플레이어가 룸에서 나갔다면 호출되는 함수
  public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }
}
