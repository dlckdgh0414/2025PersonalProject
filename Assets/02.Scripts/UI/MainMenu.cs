using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameEventChannelSO sceneChangeEvent;
    [SerializeField] private DataSO dataSO;
    [SerializeField] private SettingUI settingUI;

    private void OnEnable()
    {
        DataManager.Intance.LoadData();
    }

    public void PlayBnt()
    {
        sceneChangeEvent.RaiseEvent(SceneChangeEvents.SceneChnages.Initializer(dataSO.ClearStageNum));
    }

    public void SettingBnt()
    {
        settingUI.ShowSetting();
    }

    public void OutBnt()
    {
        DataManager.Intance.SaveData();
        Application.Quit();
    }
}
