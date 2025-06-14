using System;
using Unity.AI.Navigation;
using UnityEngine;

public class GimmickToolTip : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO tooltipEvents;
    [SerializeField] private PlayerInputSO playerInputSO;
    [SerializeField] private string tooltipText;

    private void Update()
    {
        playerInputSO.GetWorldPosition();
    }
    public void HideGimickToolTip()
    {
        tooltipEvents.RaiseEvent(TooltipEvents.HideTooltip);
    }

    public void RaiseGimickToolTip()
    {
        tooltipEvents.RaiseEvent(TooltipEvents.ShowTooltip.InInitializer(tooltipText));
    }
}
