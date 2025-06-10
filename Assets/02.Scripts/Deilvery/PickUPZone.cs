using DG.Tweening;
using System;
using UnityEngine;

public class PickUPZone : MonoBehaviour
{

    [SerializeField] private DeilveryFoodSO foodSO;
    [SerializeField] private GameEventChannelSO foodEvents;
    [SerializeField] private int deilveryCount = 0;
    [SerializeField] private GameObject foodObj;
    private int _currentDeilveryCount = 0;
    private bool _isPickUp;


    private void Awake()
    {
        foodEvents.AddListener<FoodPickUPEvent>(HandleFoodDelivertEvent);
    }

    private void OnDestroy()
    {
        foodEvents.RemoveListener<FoodPickUPEvent>(HandleFoodDelivertEvent);

    }

    private void HandleFoodDelivertEvent(FoodPickUPEvent evt)
    {
        _isPickUp = evt.deliveryStart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && !_isPickUp)
        {
            GameObject food =  Instantiate(foodObj,transform.position,Quaternion.identity);
            food.transform.DOJump(player.deilveryTrm.position, 4f, 1, 0.8f);
            _currentDeilveryCount++;
            foodEvents.RaiseEvent(FoodEvents.FoodPickUPEvent.Initializer(true, foodSO));
            if(_currentDeilveryCount >= deilveryCount)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
