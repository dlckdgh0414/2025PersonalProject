using UnityEngine;

public static class DeliveryEvents
{
    public static DeliverySuccess DeliverySuccess = new DeliverySuccess();
}

public class DeliverySuccess : GameEvent
{
    public int count = 0;
    public DeliverySuccess InInitializer(int count)
    {
        this.count = count;
        return this;
    }
}
