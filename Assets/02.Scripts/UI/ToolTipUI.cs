using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ToolTipUI : MonoBehaviour
{
    [SerializeField] private Vector2 offset = new Vector2(8, 8);
    [SerializeField] private RectTransform tootipRect;
    [SerializeField] private TextMeshProUGUI tootipText;
    [SerializeField] private GameEventChannelSO tooltipEvents;
    [SerializeField] private PlayerInputSO playerInput;

    private void OnEnable()
    {
        tootipRect.gameObject.SetActive(false);
        tooltipEvents.AddListener<ShowTooltip>(HandleShowTooltip);
        tooltipEvents.AddListener<HideTooltip>(HandleHideTooltip);
    }
    private void Update()
    {
        playerInput.GetWorldPosition();
        tootipRect.transform.position = Input.mousePosition;
    }

    private void OnDisable()
    {
        tooltipEvents.RemoveListener<ShowTooltip>(HandleShowTooltip);
        tooltipEvents.RemoveListener<HideTooltip>(HandleHideTooltip);
    }

    private void HandleHideTooltip(HideTooltip tooltip)
    {
        tootipRect.gameObject.SetActive(false);
    }

    private void HandleShowTooltip(ShowTooltip evt)
    {
        tootipRect.gameObject.SetActive(true);
        SetText(evt.message);
    }

    private void SetText(string message)
    {
        gameObject.SetActive(true);
        tootipText.text = message;
        tootipText.ForceMeshUpdate();
        Vector2 textSize = tootipText.GetRenderedValues(false) + offset;
        tootipRect.sizeDelta = textSize;
    }
}
