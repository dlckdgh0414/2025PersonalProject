using UnityEngine;

public static class TooltipEvents 
{
    public static ShowTooltip ShowTooltip = new ShowTooltip();
    public static HideTooltip HideTooltip = new HideTooltip();
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

public class HideTooltip : GameEvent
{
    
}
