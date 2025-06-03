using UnityEngine;

public static class StageEvents
{
    public static StageClaerEvent StageClaerEvent = new StageClaerEvent();
}

public class StageClaerEvent : GameEvent
{
    public string timeText;
    public float minTime;
    public float secTime;

    public StageClaerEvent InInitializer(string time,float minTime,float secTime)
    {
        this.minTime = minTime;
        this.secTime = secTime;
        this.timeText = time;
        return this;
    }
}
