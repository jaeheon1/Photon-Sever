using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField input;
    [SerializeField] GameObject chatPrefab;
    [SerializeField] Transform chatContent;
    void Start()
    {
        
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (input.text.Length == 0) return;

            //inputField에 있는 텍스트를 가져옵니다.
            string chat = PhotonNetwork.NickName + ":" + input.text;

            //RpcTarget.All 현재 룸에 있는 모든 클라이언트에게 RpcAddChat 함수를 실행하라는 명령입니다.
            photonView.RPC("RpcAddChat", RpcTarget.All, chat);


        }



    }
    [PunRPC]
    void RpcAddChat(string message)
    {
        //ChatPrefab 을 하나 만들어서 텍스트에 값을 설정합니다. 
        GameObject chat = Instantiate(chatPrefab);
        chat.GetComponent<Text>().text = message;

        //스크롤 뷰= content 자식을 등록합니다. 
        chat.transform.SetParent(chatContent);

        //채팅을 입력한 후에도 이어서 입력할 수 있도록 설정합니다.
        input.ActivateInputField();

        //input 텍스트를 초기화합니다.
        input.text = "";
    }

}
