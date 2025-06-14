using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Intance;
    [SerializeField] private DataSO dataSO;
    public StageData stageData;
    private void Awake()
    {
        if (Intance == null)
        {
            Intance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        stageData = new StageData();

        LoadData();
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        OverwritingData(false);

        SaveManager.Save(stageData, "StageJson");

        LoadData();
    }


    public void LoadData()
    {
        stageData = SaveManager.Load<StageData>("StageJson");
        if (stageData == null)
        {
            stageData = new StageData();
            SaveData();
            Debug.Log("새로운 세이브 파일을 생성했습니다.");
        }
        OverwritingData(true);
    }

    private void OverwritingData(bool isLoad)
    {
        if (isLoad)
        {
            if (stageData.StageLavelNum >= 6)
            {
                stageData.StageLavelNum = 6;
            }
            dataSO.ClearStageNum = stageData.StageLavelNum;
            dataSO.MasterVolume = stageData.MasterVolume;
            dataSO.BgmVolume = stageData.BgmVolume;
            dataSO.SfxVolume = stageData.SfVolume;
            dataSO.IsTutorialClear = stageData.IsTutorialClear;
            dataSO.ClearStageStarCount = stageData.ClearStarCount;
        }
        else
        {
            stageData.StageLavelNum = dataSO.ClearStageNum;
            stageData.MasterVolume = dataSO.MasterVolume;
            stageData.BgmVolume = dataSO.BgmVolume;
            stageData.SfVolume = dataSO.SfxVolume;
            stageData.IsTutorialClear = dataSO.IsTutorialClear;
            stageData.ClearStarCount = dataSO.ClearStageStarCount;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}

public class StageData
{
    public int StageLavelNum = 1;
    public float MasterVolume = 0;
    public float BgmVolume = 0;
    public float SfVolume = 0;
    public bool IsTutorialClear = false;
    public List<int> ClearStarCount = new List<int> { 0, 0, 0, 0, 0 };
}

