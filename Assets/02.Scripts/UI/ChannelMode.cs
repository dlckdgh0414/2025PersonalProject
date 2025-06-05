using Unity.AI.Navigation;
using UnityEngine;

public class ChannelMode : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO buildObject;
    [SerializeField] private GameEventChannelSO playerEvent;
    [SerializeField] private GameObject starDeilveryUI;
    [SerializeField] private GameObject selectUI;
    [SerializeField] private Camera came;

    public void StartButton()
    {
        playerEvent.RaiseEvent(PlayerEvents.StartPlayer.Initializer(true));
        buildObject.RaiseEvent(BuildEvents.BuildObjectCheck.Initializer(false));
        starDeilveryUI.SetActive(true);
        came.orthographic = false;
        selectUI.SetActive(false);
    }
}
