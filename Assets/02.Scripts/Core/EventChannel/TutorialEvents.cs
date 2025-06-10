using UnityEngine;

public static class TutorialEvents
{
    public static TutorialUIEvent TutorialUIEvent = new TutorialUIEvent();
}

public class TutorialUIEvent : GameEvent
{
    public bool IsTutorial = true;

    public TutorialUIEvent Initializer(bool isTutorial)
    {
        this.IsTutorial = isTutorial;
        return this;
    }
}
