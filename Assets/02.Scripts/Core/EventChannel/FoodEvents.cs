using UnityEngine;

public static class FoodEvents
{
    public static FoodPickUPEvent FoodPickUPEvent = new FoodPickUPEvent();
}

public class FoodPickUPEvent : GameEvent
{
    public bool deliveryStart;
    public DeilveryFoodSO foodSO;

    public FoodPickUPEvent Initializer(bool deliveryStart, DeilveryFoodSO foodSO)
    {
        this.deliveryStart = deliveryStart;
        this.foodSO = foodSO;
        return this;
    }
}
