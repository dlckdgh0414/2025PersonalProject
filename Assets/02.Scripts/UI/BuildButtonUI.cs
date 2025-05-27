using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtonUI : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private BuildObjectSO buildObjectSO;
    [SerializeField] private GameEventChannelSO buildObejectUI;
    [SerializeField] private GameEventChannelSO buildObject;
    private bool _isBuild = true;

    private void Awake()
    {
        buttonImage.sprite = buildObjectSO.BuildIcon;
        costText.text = buildObjectSO.BuildCost.ToString();
        buildObject.AddListener<BuildObjectCheck>(HandleBuildObjectCheck);
    }

    private void OnDestroy()
    {
        buttonImage.sprite = buildObjectSO.BuildIcon;
        costText.text = buildObjectSO.BuildCost.ToString();
        buildObject.RemoveListener<BuildObjectCheck>(HandleBuildObjectCheck);
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
    }
}
