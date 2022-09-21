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
    //로그인의 성공여부를 반환합니다. -LoginResult

    private void Awake()
    {
        PlayFabSettings.TitleId="2B5BE";
    }


    public void LoginSuccess(LoginResult result)
    {

        //AutomaticallySyncScene 마스터 클라이언트를 기준으로 씬을 동기화 할지 안할지 결정하는 기능입니다. 
        //false= 동기화를 하지 않겠다.
        //true = 마스터 클라이언트를 기준으로 동기화를 하겠다. 
        PhotonNetwork.AutomaticallySyncScene = false;

        //같은 버전의 유저끼리 접속을 허용합니다. 
        //같은 버전만 접속할 수 있도록 상수로 되어있는 문자열을 설정합니다. 
        PhotonNetwork.GameVersion = "1.0f";

        //유저 아이디 설정
        PhotonNetwork.NickName = username.text;

        //입력한 지역을 설정합니다. 
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;

        //서버 접속

        PhotonNetwork.LoadLevel("Photon Lobby");

    }

    public void LoginFailure(PlayFabError error)
    {
        Debug.Log("로그인 실패");

    }
    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("회원가입 실패 ");

    }

    public void SignUp()
    {
        //registerPlayfabUserRequest : 서버 유저를 등록하기 위한 클래스 

        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,     //입력한 email
            Password = password.text,    //입력한 비밀번호
            Username = username.text       // 입력한 유저이름 
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (

            request,   // 회원 가입에 대한 유저 정보 
            SignUpSuccess,   // 회원 가입이 성공 했을때 회원 가입 성공 함수 호출 
            SignUpFailure      // 회원 가입이 실패 했을 때 회원 가입 실패 함수 호출 

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
    




