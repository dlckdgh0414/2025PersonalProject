using DG.Tweening;
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
            _currentDeilveryCount++;

            player.playerMovement.SetStop(true);

            GameObject food = Instantiate(foodObj, transform.position, Quaternion.identity);
            food.transform.SetParent(player.deliveryTrm);

            Vector3 targetLocalPos = Vector3.zero;

            food.transform.DOLocalJump(
                targetLocalPos,
                0.5f,
                1,
                0.5f
            ).SetEase(Ease.OutQuad)
             .OnComplete(async () =>
             {
                 await Awaitable.WaitForSecondsAsync(0.5f);
                 player.playerMovement.SetStop(false);
                 player.foodObj = food;
             });

            foodEvents.RaiseEvent(FoodEvents.FoodPickUPEvent.Initializer(true, foodSO));

            if (_currentDeilveryCount >= deilveryCount)
                Destroy(gameObject);
        }
    }


}
