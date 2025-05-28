using UnityEngine;

public class PickUPZone : MonoBehaviour
{

    [SerializeField] private DeilveryFoodSO foodSO;
    [SerializeField] private GameEventChannelSO foodEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foodEvents.RaiseEvent(FoodEvents.FoodPickUPEvent.Initializer(true, foodSO));
        }
    }
}
