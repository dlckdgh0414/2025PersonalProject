using UnityEngine;

public class ModeButton : MonoBehaviour
{
    [SerializeField] private GameObject roadMode;
    [SerializeField] private GameObject channelMode;

    private void Awake()
    {
        roadMode.SetActive(true);
        channelMode.SetActive(false);
    }
    public void RoadMode()
    {
      roadMode.SetActive(true);
        channelMode.SetActive(false);
    }

    public void ChannelMode()
    {
        roadMode.SetActive(false);
        channelMode.SetActive(true);
    }
}
