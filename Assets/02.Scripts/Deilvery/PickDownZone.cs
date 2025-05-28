using UnityEngine;

public class PickDownZone : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO foodEvent;
    [SerializeField] private DeilveryFoodSO foodSo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foodEvent.RaiseEvent(FoodEvents.FoodPickUPEvent.Initializer(false, foodSo));
        }
    }
}
