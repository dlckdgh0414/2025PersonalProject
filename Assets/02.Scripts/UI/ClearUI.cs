using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI deliveryTimeText;
    [SerializeField] private int nextSceneIdx;
    [SerializeField] private float threestarsMin,threestarsSec;
    [SerializeField] private float twostarsMin, twostarsSec;


    public void SetDeliveryTime(string deliveryTime , float minTime,float secTime)
    {
        deliveryTimeText.text = "��� �ð� : " + deliveryTime;
        if(minTime <= threestarsMin && secTime < threestarsSec)
        {
            Debug.Log("��3��");
        }
        else if(minTime  < twostarsMin && secTime < twostarsSec)
        {
            Debug.Log("��2��");

        }
        else
        {
            Debug.Log("��1��");

        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneIdx);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
