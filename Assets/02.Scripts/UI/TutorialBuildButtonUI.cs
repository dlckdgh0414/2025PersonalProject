using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBuildButtonUI : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private BuildObjectSO buildObjectSO;
    [SerializeField] private GameEventChannelSO buildObejectUI;
    [SerializeField] private GameEventChannelSO buildObject;
    [SerializeField] private GameEventChannelSO tutorialUIEvent;
    private bool _isBuild = true;
    private int _count = 0;

    private void Awake()
    {
        buttonImage.sprite = buildObjectSO.BuildIcon;
        costText.text = buildObjectSO.BuildCost.ToString();
        buildObject.AddListener<BuildObjectCheck>(HandleBuildObjectCheck);
        tutorialUIEvent.AddListener<TutorialUIEvent>(HandleTutorialUI);
    }

    private void OnDestroy()
    {
        buttonImage.sprite = buildObjectSO.BuildIcon;
        costText.text = buildObjectSO.BuildCost.ToString();
        buildObject.RemoveListener<BuildObjectCheck>(HandleBuildObjectCheck);
        tutorialUIEvent.RemoveListener<TutorialUIEvent>(HandleTutorialUI);
    }

    private void HandleTutorialUI(TutorialUIEvent evt)
    {
        gameObject.SetActive(!evt.IsTutorial);
    }

    private void HandleBuildObjectCheck(BuildObjectCheck check)
    {
        _isBuild = check.IsBuild;
    }

    public void HandleCilckButton()
    {
        buildObject.RaiseEvent(BuildEvents.BuildObject.Initializer(buildObjectSO.BuildCost));
        if (_isBuild)
        {
            buildObejectUI.RaiseEvent(BuildUIEventChannel.BuildObjectUI.Initializer(buildObjectSO.BuildObject, buildObjectSO.BuildCost));
        }
        else
        {
            buildObejectUI.RaiseEvent(BuildUIEventChannel.BuildObjectUI.Initializer(null, 0));
        }

        if(_count <= 0)
        {
            tutorialUIEvent.RaiseEvent(TutorialEvents.TutorialUIEvent.Initializer(true));
        }
    }
}
