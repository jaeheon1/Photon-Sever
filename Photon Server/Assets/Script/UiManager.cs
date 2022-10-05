using UnityEngine;
using PlayFab;
using UnityEngine.UI;
using PlayFab.ClientModels;
using Photon.Pun;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;


    public Text scoreText;
    public Text leaderBorderText;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
}