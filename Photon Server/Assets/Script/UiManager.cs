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

    int Box = 10;
    //L value = 표현식 이후에도 사라지지 않은값 (메모리 공간을 가지고있는 정수)
    //R value : 표현식 이후에는 사라지는 값 (임수 변수)
  
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

    public void GetVirtualCurrency()
    {
        var request = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "RP",   //Playfab에 설정한 통화코드
            Amount = 100               // 가상 화폐에 추가할 값
        };
        PlayFabClientAPI.AddUserVirtualCurrency
            (
            request, (result) => Debug.Log("가상화폐가 추가되었습니다"+result.Balance),
            (error) => Debug.Log("가상화폐를 흭득하지 못했습니다")
            );

    }
    public void PurchaseItem()
    {
        var request = new PurchaseItemRequest()
        {
            CatalogVersion = "Game Shop",
            ItemId = "Dragon Skin",
            VirtualCurrency = "RP",
            Price = 100
        };

        PlayFabClientAPI.PurchaseItem
        (
            request,
            (result) => print("아이템 구입 성공"),
            (error) => print("아이템 구입 실패")
        );
    }
}