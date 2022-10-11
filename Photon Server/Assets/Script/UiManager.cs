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
    //L value = ǥ���� ���Ŀ��� ������� ������ (�޸� ������ �������ִ� ����)
    //R value : ǥ���� ���Ŀ��� ������� �� (�Ӽ� ����)
  
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

    public void GetVirtualCurrency()
    {
        var request = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "RP",   //Playfab�� ������ ��ȭ�ڵ�
            Amount = 100               // ���� ȭ�� �߰��� ��
        };
        PlayFabClientAPI.AddUserVirtualCurrency
            (
            request, (result) => Debug.Log("����ȭ�� �߰��Ǿ����ϴ�"+result.Balance),
            (error) => Debug.Log("����ȭ�� ŉ������ ���߽��ϴ�")
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
            (result) => print("������ ���� ����"),
            (error) => print("������ ���� ����")
        );
    }
}