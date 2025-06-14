using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Slider master;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider sfx;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private DataSO data;
    [SerializeField] private Image setting;
    [SerializeField] private GameEventChannelSO sceneChangeEvent;
    [SerializeField] private GameObject mainMenuBtn;
    public bool IsEsc;
    private float _isTimeScale;
    private bool _pausedBySetting = false;

    private void OnEnable()
    {
        SetMasterVolume();
        SetBgmVolume();
        SetSfxCVolume();
        mainMenuBtn.SetActive(false);
    }

    private void Start()
    {
        LoadSettingUI();
    }

    private void OnDisable()
    {
        SaveSettingUI();        
    }


    public void ShowSetting()
    {
        if (!_pausedBySetting)
        {
            _isTimeScale = Time.timeScale;
            _pausedBySetting = true;
        }

        setting.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideSetting()
    {
        setting.gameObject.SetActive(false);

        if (_pausedBySetting)
        {
            Time.timeScale = _isTimeScale;
            _pausedBySetting = false;
        }
    }

    private void SaveSettingUI()
    {
        SetMasterVolume();
        SetBgmVolume();
        SetSfxCVolume();

        DataManager.Intance.SaveData();
    }

    private void LoadSettingUI()
    {
        if(DataManager.Intance != null)
        {
            DataManager.Intance.LoadData();
        }

        master.value = DbToLinear(data.MasterVolume);
        bgm.value = DbToLinear(data.BgmVolume);
        sfx.value = DbToLinear(data.SfxVolume);

        mixer.SetFloat("Master", data.MasterVolume);
        mixer.SetFloat("BGM", data.BgmVolume);
        mixer.SetFloat("SFX", data.SfxVolume);
    }


    private float DbToLinear(float dB)
    {
        return Mathf.Pow(10, dB / 20f);
    }

    public void CloseBnt()
    {
        HideSetting();
    }

    public void MainMenuBnt()
    {
        Time.timeScale = 1f;
        sceneChangeEvent.RaiseEvent(SceneChangeEvents.SceneChnages.Initializer(0));
        HideSetting();
    }

    private void Update()
    {
        if (IsEsc)
        {
            mainMenuBtn.SetActive(true);
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ShowSetting();
            }
        }
    }

    public void SetMasterVolume()
    {
        float vol = Mathf.Clamp(master.value, 0.0001f, 1f);
        float volume = Mathf.Log10(vol) * 20;
        data.MasterVolume = volume;
        mixer.SetFloat("Master", volume);
    }

    public void SetBgmVolume()
    {
        float vol = Mathf.Clamp(bgm.value, 0.0001f, 1f);
        float volume = Mathf.Log10(vol) * 20;
        data.BgmVolume = volume;
        mixer.SetFloat("BGM", volume);
    }

    public void SetSfxCVolume()
    {
        float vol = Mathf.Clamp(sfx.value, 0.0001f, 1f);
        float volume = Mathf.Log10(vol) * 20;
        data.SfxVolume = volume;
        mixer.SetFloat("SFX", volume);
    }
}
