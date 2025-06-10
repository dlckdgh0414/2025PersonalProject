using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private TutorialUI tutorialUI;
    [SerializeField] private GameObject channelMod;

    public void ShowtorialUI()
    {
        tutorialUI.gameObject.SetActive(true);
        tutorialUI.count = 3;
    }
}
