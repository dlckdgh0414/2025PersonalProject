using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private TutorialUI tutorialUI;
    [SerializeField] private GameObject channelMod;
    private int _count;

    public void ShowtorialUI()
    {
        if (_count <= 0)
        {
            tutorialUI.count = 3;
            tutorialUI.gameObject.SetActive(true);
            _count++;
        }
    }
}
