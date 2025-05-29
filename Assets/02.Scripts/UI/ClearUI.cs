using TMPro;
using UnityEngine;

public class ClearUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI deliveryTimeText;

    public void SetDeliveryTime(string deliveryTime)
    {
        deliveryTimeText.text = "배달 시간 : " + deliveryTime;
    }
}
