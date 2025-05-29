using System;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;

public class BuildCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private StageSettingSO stageSetting;
    [SerializeField] private GameEventChannelSO buildObject;
    [SerializeField] private GameEventChannelSO playerEvent;
    private int _currentCost;


    private void Awake()
    {
        costText.text = "Cost : " + stageSetting.stageMaxCost;
        _currentCost = stageSetting.stageMaxCost;
        buildObject.AddListener<BuildObject>(HandleBuildCostDown);
    }

    private void OnDestroy()
    {
        buildObject.RemoveListener<BuildObject>(HandleBuildCostDown);
    }

    private void HandleBuildCostDown(BuildObject evt)
    {
        if(_currentCost - evt.buildCost < 0)
        {
            buildObject.RaiseEvent(BuildEvents.BuildObjectCheck.Initializer(false));
            return;
        }
        if (evt.IsUseCost)
        {
            _currentCost -= evt.buildCost;
             Mathf.Clamp(_currentCost - evt.buildCost, 0, _currentCost);
             costText.text = "Cost : " + _currentCost;
        }
        buildObject.RaiseEvent(BuildEvents.BuildObjectCheck.Initializer(true));

    }

   
}
