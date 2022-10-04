using Photon.Pun;
using UnityEngine;
public class PhotonControl : MonoBehaviourPun
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;

    private Animator animator;


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
            PhotonView view = other.gameObject.GetComponent<PhotonView>();
            if(view.IsMine)
            {
                //충돌한 물체가 네트워크 객체라면
                //네트워크 객체를 파괴합니다.
                PhotonNetwork.Destroy(other.gameObject);
            }

        }

    }
}
