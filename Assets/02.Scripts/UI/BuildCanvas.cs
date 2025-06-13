using System;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class BuildCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private StageSettingSO stageSetting;
    [SerializeField] private GameEventChannelSO buildObject;
    [SerializeField] private GameEventChannelSO playerEvent;
    [SerializeField] private TutorialUI tutorialUI;
    private int _count;
    private int _currentCost;

    private void Awake()
    {
        costText.text = "Cost : " + stageSetting.stageMaxCost;
        _currentCost = stageSetting.stageMaxCost;
       
    }

    private void OnEnable()
    {

        buildObject.AddListener<BuildObject>(HandleBuildCostDown);
        buildObject.AddListener<DelObject>(HandleDelObject);
        if (_count <= 0 && tutorialUI != null)
        {
            tutorialUI.gameObject.SetActive(true);
            _count++;
        }
    }

    private void OnDisable()
    {
        buildObject.RemoveListener<BuildObject>(HandleBuildCostDown);
        
    }

    private void OnDestroy()
    {
        buildObject.RemoveListener<DelObject>(HandleDelObject);
    }

    private void HandleDelObject(DelObject evt)
    {
        _currentCost += evt.buildCost;
        Mathf.Clamp(_currentCost - evt.buildCost, 0, _currentCost);
        costText.text = "Cost : " + _currentCost;
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
