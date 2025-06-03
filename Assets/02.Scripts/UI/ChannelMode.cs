using Unity.AI.Navigation;
using UnityEngine;

public class ChannelMode : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO buildObject;
    [SerializeField] private GameEventChannelSO playerEvent;
    [SerializeField] private GameObject starDeilveryUI;
    [SerializeField] private GameObject selectUI;

    public void StartButton()
    {
        playerEvent.RaiseEvent(PlayerEvents.StartPlayer.Initializer(true));
        buildObject.RaiseEvent(BuildEvents.BuildObjectCheck.Initializer(false));
        starDeilveryUI.SetActive(true);
        selectUI.SetActive(false);
    }
}
