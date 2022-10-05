using Photon.Pun;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;

public class PhotonControl : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;

    private Animator animator;
    public int score;

    //isMine : 나 자신만 플레이 하고 싶을때
    void Start()
    {

        animator = GetComponent<Animator>();
       


        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }


    void Update()
    {
        //현재 플레이어가 나 자신이 아니라면 
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }




        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(dir.normalized * speed * Time.deltaTime);
        transform.eulerAngles += new Vector3
            (

            0,
            Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime,
            0

            );

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Crystal(Clone)")
        {
            if (photonView.IsMine)
            {
                score++;
            }

            PlayFabDataSvae();
            PhotonView view = other.gameObject.GetComponent<PhotonView>();
            if(view.IsMine)
            {
                //충돌한 물체가 네트워크 객체라면
                //네트워크 객체를 파괴합니다.
                PhotonNetwork.Destroy(other.gameObject);
            }

        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //로컬 오브젝트라면 쓰기 부분이 실행 됩ㄴ디ㅏ. 
        if (stream.IsWriting)
        {
            //네트워크를 통해 score 값을 보냅니다.
            stream.SendNext(score);
        }
        else
        {//원격 오브젝트 라면 읽기 부분이 실행됩니다.
            score = (int)stream.ReceiveNext();
        }
    }

    public void PlayFabDataSvae()
    {
        PlayFabClientAPI.UpdatePlayerStatistics
            (
              new UpdatePlayerStatisticsRequest
              {
                  Statistics = new List<StatisticUpdate>
                  {
                      new StatisticUpdate
                      {
                          StatisticName="Score",Value=score
                      },
                  }
              },

              (result) => { Debug.Log("값 저장 성공"); },
              (error) => { Debug.Log("값 저장 실패"); }

            ) ;


    }
}
