using UnityEngine;

public static class TooltipEvents 
{
    public static ShowTooltip ShowTooltip = new ShowTooltip();
}

public class ShowTooltip : GameEvent
{
    public string message;

    public ShowTooltip InInitializer(string message)
    {
        this.message = message;
        return this;
    }
}
