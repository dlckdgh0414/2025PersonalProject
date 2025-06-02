using System;
using UnityEngine;

public class PickUPZone : MonoBehaviour
{

    [SerializeField] private DeilveryFoodSO foodSO;
    [SerializeField] private GameEventChannelSO foodEvents;
    [SerializeField] private int deilveryCount = 0;
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
        if (other.CompareTag("Player") && !_isPickUp)
        {
            _currentDeilveryCount++;
            foodEvents.RaiseEvent(FoodEvents.FoodPickUPEvent.Initializer(true, foodSO));
            if(_currentDeilveryCount >= deilveryCount)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
