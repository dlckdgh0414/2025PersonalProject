using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageSettingSO stageSetting;
    [SerializeField] private GameEventChannelSO stageClearEvent;
    [SerializeField] private ClearUI stageClaerUI;

    private void Awake()
    {
        stageClearEvent.AddListener<StageClaerEvent>(HandleClearStageEvent);
    }

    private void OnDestroy()
    {
        stageClearEvent.RemoveListener<StageClaerEvent>(HandleClearStageEvent);
    }

    private void HandleClearStageEvent(StageClaerEvent evt)
    {
        stageClaerUI.gameObject.SetActive(true);
        stageClaerUI.SetDeliveryTime(evt.timeText,evt.minTime,evt.secTime);
    }
}
