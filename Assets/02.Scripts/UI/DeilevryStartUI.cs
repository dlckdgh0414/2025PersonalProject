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
    [SerializeField] private StageSettingSO stageSettingSO;
    [SerializeField] private TextMeshProUGUI deliveryHouseText;
    [SerializeField] private GameEventChannelSO deliveryClearEvent;
    [SerializeField] private GameEventChannelSO claerStageEvent;
    private bool _isDeliveryStart = false;
    private float _sec = 0;
    private int _min = 0;
    private int _currentHouse = 0;

    private void Awake()
    {
        gameObject.SetActive(false);
        deliveryHouseText.text = "남은 배달 : " + stageSettingSO.MaxDeliveryHouse;
        _currentHouse = stageSettingSO.MaxDeliveryHouse;
        
    }

    private void OnEnable()
    {
        foodDeilevryEvent.AddListener<FoodPickUPEvent>(HandleFoodPickUp);
        deliveryClearEvent.AddListener<DeliverySuccess>(HandleClaerDelivery);
    }

    private void OnDisable()
    {
        foodDeilevryEvent.RemoveListener<FoodPickUPEvent>(HandleFoodPickUp);
        deliveryClearEvent.RemoveListener<DeliverySuccess>(HandleClaerDelivery);

    }


    private void Update()
    {
        if (_isDeliveryStart)
        {
           
            _sec += Time.deltaTime;
            if (_sec >= 60f)
            {
                _min += 1;
                _sec = 0;
            }
            deilevryTimeText.text = "배달 시간 : " + string.Format("{0:D2}:{1:D2}", _min, (int)_sec);
        }
    }
    private void HandleClaerDelivery(DeliverySuccess evt)
    {
        _currentHouse -= evt.count;
        deliveryHouseText.text = "남은 배달 : " + _currentHouse;
        if(_currentHouse <= 0)
        {
            string str = string.Format("{0:D2}:{1:D2}", _min, (int)_sec);
            claerStageEvent.RaiseEvent(StageEvents.StageClaerEvent.InInitializer(str ));
        }
    }

    private void HandleFoodPickUp(FoodPickUPEvent evt)
    {
        foodImage.sprite = evt.foodSO.foodSprite;
        foodText.text = evt.foodSO.foodName;
        _isDeliveryStart = evt.deliveryStart;
    }
}
