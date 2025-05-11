using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildButtonUI : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private BuildObjectSO buildObjectSO;

    private void OnEnable()
    {
        buttonImage.sprite = buildObjectSO.BuildIcon;
        costText.text = buildObjectSO.BuildCost.ToString();
    }
}
