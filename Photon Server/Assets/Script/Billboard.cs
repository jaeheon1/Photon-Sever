using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Billboard : MonoBehaviourPun
{
   
    [SerializeField] Text NickName;
   

    void Start()
    {
        //�ڱ� �ڽ��� �г������� �����ϴ� �����Դϴ�.
        NickName.text = photonView.Owner.NickName;
    }

   
    void Update()
    {
        //�ڱ� ���� ���⿡ ī�޶��� �� ������ �����Ͽ� �ٶ� �� �ֵ��� �����մϴ�.
        transform.forward = Camera.main.transform.forward;

      
    }
}
