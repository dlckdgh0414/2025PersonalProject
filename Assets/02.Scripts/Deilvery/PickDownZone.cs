using DG.Tweening;
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
    [SerializeField] private Transform deliveryTargetTrm;
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
        if (other.TryGetComponent(out Player player) && _isPickDown)
        {
            GameObject food = player.foodObj; 
            if (food != null)
            {
                await food.transform.DOMove(deliveryTargetTrm.position, 0.5f).SetEase(Ease.InOutSine).AsyncWaitForCompletion();
                GameObject.Destroy(food);
            }
            deilveryEffect.Play();
            await Awaitable.WaitForSecondsAsync(0.5f);
            foodEvent.RaiseEvent(FoodEvents.FoodPickUPEvent.Initializer(false, foodSo));
            deilevryClear.RaiseEvent(DeliveryEvents.DeliverySuccess.InInitializer(1));
            Destroy(gameObject);
        }
    }
}
