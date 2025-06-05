using System;
using System.Threading.Tasks;
using UnityEngine;

public class PickDownZone : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO foodEvent;
    [SerializeField] private DeilveryFoodSO foodSo;
    [SerializeField] private GameEventChannelSO deilevryClear;
    [SerializeField] private FoodEnum foodType;
    [SerializeField] private ParticleSystem deilveryEffect;
    private bool _isPickDown;

    private void Awake()
    {
        foodEvent.AddListener<FoodPickUPEvent>(HandleFoodTypeCheck);
    }

    private void OnDestroy()
    {
        foodEvent.RemoveListener<FoodPickUPEvent>(HandleFoodTypeCheck);
    }

    private void HandleFoodTypeCheck(FoodPickUPEvent evt)
    {
        if(evt.foodSO.foodType == foodType)
        {
            _isPickDown = true;
        }
        else
        {
            _isPickDown = false;
        }
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& _isPickDown)
        {
            foodEvent.RaiseEvent(FoodEvents.FoodPickUPEvent.Initializer(false, foodSo));
            deilevryClear.RaiseEvent(DeliveryEvents.DeliverySuccess.InInitializer(1));
            deilveryEffect.Play();
            await Awaitable.WaitForSecondsAsync(1f);
            Destroy(gameObject);
        }
    }
}
