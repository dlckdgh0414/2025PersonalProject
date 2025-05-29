using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI deliveryTimeText;
    [SerializeField] private int nextSceneIdx;

    public void SetDeliveryTime(string deliveryTime)
    {
        deliveryTimeText.text = "배달 시간 : " + deliveryTime;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneIdx);
    }
}
