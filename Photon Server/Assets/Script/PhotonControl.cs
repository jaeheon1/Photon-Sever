using Photon.Pun;
using UnityEngine;
public class PhotonControl : MonoBehaviourPun
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;

    private Animator animator;


    //isMine : �� �ڽŸ� �÷��� �ϰ� ������
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
        //���� �÷��̾ �� �ڽ��� �ƴ϶�� 
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
                //�浹�� ��ü�� ��Ʈ��ũ ��ü���
                //��Ʈ��ũ ��ü�� �ı��մϴ�.
                PhotonNetwork.Destroy(other.gameObject);
            }

        }

    }
}
