using UnityEngine;

public class PickDownZone : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO foodEvent;
    [SerializeField] private DeilveryFoodSO foodSo;
    [SerializeField] private GameEventChannelSO deilevryClear;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foodEvent.RaiseEvent(FoodEvents.FoodPickUPEvent.Initializer(false, foodSo));
            deilevryClear.RaiseEvent(DeliveryEvents.DeliverySuccess.InInitializer(1));
        }
    }
}
