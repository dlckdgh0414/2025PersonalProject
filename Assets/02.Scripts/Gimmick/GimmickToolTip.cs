using System;
using Unity.AI.Navigation;
using UnityEngine;

public class GimmickToolTip : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO tooltipEvents;
    [SerializeField] private string tooltipText;
    public void HideGimickToolTip()
    {
        tooltipEvents.RaiseEvent(TooltipEvents.HideTooltip);
    }

    public void RaiseGimickToolTip()
    {
        tooltipEvents.RaiseEvent(TooltipEvents.ShowTooltip.InInitializer(tooltipText));
    }
}
