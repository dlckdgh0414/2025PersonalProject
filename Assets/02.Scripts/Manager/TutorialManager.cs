using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO tutorialUIEvent;
    [SerializeField] private GameObject tutorialUI;

    private void Awake()
    {
        tutorialUIEvent.AddListener<TutorialUIEvent>(HandleTutorialUIEvent);
    }

    private void HandleTutorialUIEvent(TutorialUIEvent evt)
    {
        tutorialUI.SetActive(evt.IsTutorial);
    }

    private void OnDestroy()
    {
        tutorialUIEvent.RemoveListener<TutorialUIEvent>(HandleTutorialUIEvent);
    }
}
