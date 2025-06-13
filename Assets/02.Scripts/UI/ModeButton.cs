using UnityEngine;

public class ModeButton : MonoBehaviour
{
    [SerializeField] private GameObject roadMode;
    [SerializeField] private GameObject channelMode;
    [SerializeField] private GameObject destoryMode;
    private void OnEnable()
    {
        roadMode.SetActive(true);
        channelMode.SetActive(false);
        destoryMode.SetActive(false);
    }
    public void RoadMode()
    {
        roadMode.SetActive(true);
        channelMode.SetActive(false);
        destoryMode.SetActive(false);
    }

    public void ChannelMode()
    {
        roadMode.SetActive(false);
        channelMode.SetActive(true);
        destoryMode.SetActive(false);
    }

    public void DestoryMode()
    {
        roadMode.SetActive(false);
        channelMode.SetActive(false);
        destoryMode.SetActive(true);
    }
}
