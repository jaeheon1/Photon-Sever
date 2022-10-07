using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class WebLoad : MonoBehaviour
{
    //������ �̹����� �ҷ��� ���� Raw Image�� ����ؾ� �մϴ�.

    [SerializeField] RawImage webImage;
    void Awake()
    {
        string a= "https://raw.githubusercontent.com/Unity2033/Unity-3D-Example/main/Assets/Class/Photon%20Server/Texture/Ice%20Kingdom.jpg";
        //�����͸� �ε� �� �� 
        StartCoroutine(WebImageLoad(a));
    }

    private void Start()
    {
        //ĳ���� ��ġ ��
    }

    private IEnumerator WebImageLoad(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log("www.error");
        }
        else
        {
            webImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }
}
