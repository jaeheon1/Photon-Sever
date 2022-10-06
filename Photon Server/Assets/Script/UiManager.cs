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
        //var : 자동으로 자료형을 추론해서 자료형을 정해줍니다.
        var request = new GetLeaderboardRequest
        {
            StartPosition = 0, //기본 위치 값
            StatisticName = "Score", // playfab에서 불러올 순위표 이름
            MaxResultsCount = 20, //순위표에 최대로 나타나는 수 
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
         (error) => Debug.Log("리더보드를 불러오지 못했습니다"));
        

    }

}