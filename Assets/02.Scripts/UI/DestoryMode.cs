using System;
using UnityEngine;

public class DestoryMode : MonoBehaviour
{
    [SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private RoadManager manager;

    private void OnEnable()
    {
        playerInput.OnBuildPressed += HandleDestoryClick;
    }

    private void OnDisable()
    {
        playerInput.OnBuildPressed -= HandleDestoryClick;
    }

    private void HandleDestoryClick()
    {
        if(playerInput.clickRoadPrefab != null)
        {

            playerInput.clickRoadPrefab.DestoryRoad(manager.mapGrid,manager.roadPoints);
        }
    }
}
