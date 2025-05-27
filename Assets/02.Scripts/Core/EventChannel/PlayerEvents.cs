using UnityEngine;

public static class PlayerEvents
{
    public static StartPlayerEvent StartPlayer = new StartPlayerEvent();
}

public class StartPlayerEvent : GameEvent
{
    public bool IsStart = false;

    public StartPlayerEvent Initializer(bool isStart)
    {
        IsStart = isStart;
        return this;
    }
}
