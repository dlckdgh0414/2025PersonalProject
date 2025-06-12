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
    public bool IsEsc;

    private void Awake()
    {
        HideSetting();
    }

    private void OnEnable()
    {
        SetMasterVolume();
        SetBgmVolume();
        SetSfxCVolume();
        LoadSettingUI();
        
    }

    private void OnDisable()
    {
        SaveSettingUI();
        
    }

    public void HideSetting()
    {
        setting.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowSetting()
    {
        setting.gameObject.SetActive(true);
        Time.timeScale = 0f;
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
        DataManager.Intance.LoadData();

        master.value = DbToLinear(data.masterVolume);
        bgm.value = DbToLinear(data.bgmVolume);
        sfx.value = DbToLinear(data.sfxVolume);

        mixer.SetFloat("Master", data.masterVolume);
        mixer.SetFloat("BGM", data.bgmVolume);
        mixer.SetFloat("SFX", data.sfxVolume);
    }


    private float DbToLinear(float dB)
    {
        return Mathf.Pow(10, dB / 20f);
    }

    public void CloseBnt()
    {
        HideSetting();
    }

    private void Update()
    {
        if (IsEsc)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
            }
        }
    }

    public void SetMasterVolume()
    {
        float vol = Mathf.Clamp(master.value, 0.0001f, 1f);
        float volume = Mathf.Log10(vol) * 20;
        data.masterVolume = volume;
        mixer.SetFloat("Master", volume);
    }

    public void SetBgmVolume()
    {
        float vol = Mathf.Clamp(bgm.value, 0.0001f, 1f);
        float volume = Mathf.Log10(vol) * 20;
        data.bgmVolume = volume;
        mixer.SetFloat("BGM", volume);
    }

    public void SetSfxCVolume()
    {
        float vol = Mathf.Clamp(sfx.value, 0.0001f, 1f);
        float volume = Mathf.Log10(vol) * 20;
        data.sfxVolume = volume;
        mixer.SetFloat("SFX", volume);
    }
}
