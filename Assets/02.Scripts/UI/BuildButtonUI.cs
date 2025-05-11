using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtonUI : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private BuildObjectSO buildObjectSO;
    [SerializeField] private GameEventChannelSO buildObejectUI;

    private void OnEnable()
    {
        buttonImage.sprite = buildObjectSO.BuildIcon;
        costText.text = buildObjectSO.BuildCost.ToString();
    }

    public void HandleCilckButton()
    {
        buildObejectUI.RaiseEvent(BuildUIEventChannel.BuildObjectUI.Initializer(buildObjectSO.BuildObject, buildObjectSO.BuildCost));
    }
}
