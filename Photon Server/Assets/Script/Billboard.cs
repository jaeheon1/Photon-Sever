using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Billboard : MonoBehaviourPun
{
   
    [SerializeField] Text NickName;
   

    void Start()
    {
        //자기 자신의 닉네임으로 설정하는 변수입니다.
        NickName.text = photonView.Owner.NickName;
    }

   
    void Update()
    {
        //자기 앞의 방향에 카메라의 앞 방향을 지정하여 바라볼 수 있도록 설정합니다.
        transform.forward = Camera.main.transform.forward;

      
    }
}
