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
    [SerializeField] private float typingSpeed = 0.3f;
    [SerializeField] private float delayBetweenTexts = 1f;
    [SerializeField] private GameObject[] nextUI;
    [SerializeField] private RoadManager manager;
    [SerializeField] private DataSO dataSO;
    [SerializeField] private GameObject system;
    public int count = 0;

    private void OnEnable()
    {
        if (!dataSO.isTutorialClear)
        {
            manager.IsChangeing = false;
            ShowTextSequence();
        }
        else
        {
            system.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        count++;
        if (count >= nextUI.Length)
        {
            count = nextUI.Length;
            dataSO.isTutorialClear = true;
        }
        manager.IsChangeing = true;
    }

    private async void ShowTextSequence()
    {
        foreach (string fullText in picoStrings.textList[count].picotexts)
        {
            picoText.text = "";
            foreach (char c in fullText)
            {
                picoText.text += c;
                await Awaitable.WaitForSecondsAsync(typingSpeed);
            }

            await Awaitable.WaitForSecondsAsync(delayBetweenTexts);
        }

        nextUI[count].SetActive(true);
        gameObject.SetActive(false);

    }
}