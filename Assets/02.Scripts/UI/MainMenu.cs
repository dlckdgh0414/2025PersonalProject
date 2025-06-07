using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameEventChannelSO sceneChangeEvent;

   public void PlayBnt()
    {
        sceneChangeEvent.RaiseEvent(SceneChangeEvents.SceneChnages.Initializer(1));
    }

    public void OutBnt()
    {
        Application.Quit();
    }
}
