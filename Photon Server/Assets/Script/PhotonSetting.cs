using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;

public class PhotonSetting : MonoBehaviour
{
    public InputField email;
    public InputField password;
    public InputField username;
    public Dropdown region;
    //�α����� �������θ� ��ȯ�մϴ�. -LoginResult

    private void Awake()
    {
        PlayFabSettings.TitleId="2B5BE";
    }


    public void LoginSuccess(LoginResult result)
    {

        //AutomaticallySyncScene ������ Ŭ���̾�Ʈ�� �������� ���� ����ȭ ���� ������ �����ϴ� ����Դϴ�. 
        //false= ����ȭ�� ���� �ʰڴ�.
        //true = ������ Ŭ���̾�Ʈ�� �������� ����ȭ�� �ϰڴ�. 
        PhotonNetwork.AutomaticallySyncScene = false;

        //���� ������ �������� ������ ����մϴ�. 
        //���� ������ ������ �� �ֵ��� ����� �Ǿ��ִ� ���ڿ��� �����մϴ�. 
        PhotonNetwork.GameVersion = "1.0f";

        //���� ���̵� ����
        PhotonNetwork.NickName = username.text;

        //�Է��� ������ �����մϴ�. 
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;

        //���� ����

        PhotonNetwork.LoadLevel("Photon Lobby");

    }

    public void LoginFailure(PlayFabError error)
    {
        Debug.Log("�α��� ����");

    }
    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("ȸ������ ����");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("ȸ������ ���� ");

    }

    public void SignUp()
    {
        //registerPlayfabUserRequest : ���� ������ ����ϱ� ���� Ŭ���� 

        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,     //�Է��� email
            Password = password.text,    //�Է��� ��й�ȣ
            Username = username.text       // �Է��� �����̸� 
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (

            request,   // ȸ�� ���Կ� ���� ���� ���� 
            SignUpSuccess,   // ȸ�� ������ ���� ������ ȸ�� ���� ���� �Լ� ȣ�� 
            SignUpFailure      // ȸ�� ������ ���� ���� �� ȸ�� ���� ���� �Լ� ȣ�� 

        );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text
        };

        PlayFabClientAPI.LoginWithEmailAddress
            (
               request,
               LoginSuccess,
               LoginFailure

            );


    }
}
    




