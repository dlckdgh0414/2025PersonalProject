using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI picoText;
    [SerializeField] private TextList picoStrings;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float delayBetweenTexts = 1f;
    [SerializeField] private GameObject[] tutorialUI;
    [SerializeField] private GameEventChannelSO tutorialEvent;
    private int _count = 0;

    private void OnEnable()
    {
        if(_count > 0)
        {
            tutorialUI[_count - 1].SetActive(false);
        }
        ShowTextSequence();
    }

    private void OnDisable()
    {
        _count++;
    }

    private async void ShowTextSequence()
    {
        foreach (string fullText in picoStrings.textList[_count].picotexts)
        {
            picoText.text = "";
            foreach (char c in fullText)
            {
                picoText.text += c;
                await Awaitable.WaitForSecondsAsync(typingSpeed);
            }

            await Awaitable.WaitForSecondsAsync(delayBetweenTexts);
        }

        tutorialUI[_count].SetActive(true);
        tutorialEvent.RaiseEvent(TutorialEvents.TutorialUIEvent.Initializer(false));
    }
}