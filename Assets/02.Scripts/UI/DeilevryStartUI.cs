using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeilevryStartUI : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO foodDeilevryEvent;
    [SerializeField] private Image foodImage;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI deilevryTimeText;
    private bool _isDeliveryStart = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foodDeilevryEvent.AddListener<FoodPickUPEvent>(HandleFoodPickUp);
    }

    private void OnDisable()
    {
        foodDeilevryEvent.RemoveListener<FoodPickUPEvent>(HandleFoodPickUp);

    }

    private void Update()
    {
        if (_isDeliveryStart)
        {
            deilevryTimeText.text = "배달 시간 : " + Time.time;
        }
    }

    private void HandleFoodPickUp(FoodPickUPEvent evt)
    {
        foodImage.sprite = evt.foodSO.foodSprite;
        foodText.text = evt.foodSO.foodName;
        _isDeliveryStart = evt.deliveryStart;
    }
}
