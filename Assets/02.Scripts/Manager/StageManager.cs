using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageSettingSO stageSetting;
    [SerializeField] private GameEventChannelSO stageClearEvent;
    [SerializeField] private GameEventChannelSO audioChange;
    [SerializeField] private ClearUI stageClaerUI;
    [SerializeField] private AudioClip stageBgm;

    private void Awake()
    {
        stageClearEvent.AddListener<StageClaerEvent>(HandleClearStageEvent);
    }

    private void Start()
    {
        audioChange.RaiseEvent(AudioEvents.AudioChangeEvent.Initializer(AudioType.BGM, stageBgm));
    }

    private void OnDestroy()
    {
        stageClearEvent.RemoveListener<StageClaerEvent>(HandleClearStageEvent);
    }

    private void HandleClearStageEvent(StageClaerEvent evt)
    {
        if(stageClaerUI != null)
        {
            stageClaerUI.gameObject.SetActive(true);
            stageClaerUI.SetDeliveryTime(evt.timeText,evt.minTime,evt.secTime);
        }
    }
}
