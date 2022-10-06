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

    [SerializeField] GameObject leaderBorder;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

  
    public void GetLeaderBorder()
    {

        leaderBorder.SetActive(true);
        //var : �ڵ����� �ڷ����� �߷��ؼ� �ڷ����� �����ݴϴ�.
        var request = new GetLeaderboardRequest
        {
            StartPosition = 0, //�⺻ ��ġ ��
            StatisticName = "Score", // playfab���� �ҷ��� ����ǥ �̸�
            MaxResultsCount = 20, //����ǥ�� �ִ�� ��Ÿ���� �� 
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowLocations = true,
                ShowDisplayName = true,
            }
        };

        PlayFabClientAPI.GetLeaderboard(request, (result) =>
         {
             for (int i = 0; i < result.Leaderboard.Count; i++)
             {
                 var currentBoader = result.Leaderboard[i];
                 leaderBorderText.text += currentBoader.Profile.Locations[0].ContinentCode.Value +
                 "-" + currentBoader.DisplayName + "-" + currentBoader.StatValue + "\n";
             }

         },
         (error) => Debug.Log("�������带 �ҷ����� ���߽��ϴ�"));
        

    }

}