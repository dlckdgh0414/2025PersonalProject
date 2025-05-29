using UnityEngine;

public static class StageEvents
{
    public static StageClaerEvent StageClaerEvent = new StageClaerEvent();
}

public class StageClaerEvent : GameEvent
{
    public string time;

    public StageClaerEvent InInitializer(string time)
    {
        this.time = time;
        return this;
    }
}
