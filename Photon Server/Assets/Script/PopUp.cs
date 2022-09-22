using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PopUp : MonoBehaviour
{
    // Text 2 3 �� �Ŀ� ������ ���� �ֽ��ϴ�.
    //TextMeshPro
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel;

    //PopUp ��ũ��Ʈ�� �������� ������ �� �ִ� �Լ�
    public static PopUp Show(string title,string message)
    {
        if (gamePanel == null)
        { 

            Instantiate(Resources.Load<GameObject>("Game Panel"));


        }

        PopUp window = gamePanel.GetComponent<PopUp>();
        window.UpdateContent(title, message);

        return window;
    }
    // �˾��� ������ ������Ʈ �ϴ� �Լ�

    public void UpdateContent(string titleMessage,string contentMessage)
    {
        title.text = titleMessage;
        content.text = contentMessage;
    }
    // �˾��� �ݴ� �Լ� 
    public void Cancle()
    {
        Destroy(gameObject);
    }

}
