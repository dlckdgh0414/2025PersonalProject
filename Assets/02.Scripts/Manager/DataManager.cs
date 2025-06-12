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
            if (stageData.StageLavelNum >= 5)
            {
                stageData.StageLavelNum = 5;
            }
            dataSO.ClearStageNum = stageData.StageLavelNum;
            dataSO.masterVolume = stageData.MasterVolume;
            dataSO.bgmVolume = stageData.BgmVolume;
            dataSO.sfxVolume = stageData.SfVolume;
        }
        else
        {
            stageData.StageLavelNum = dataSO.ClearStageNum;
            stageData.MasterVolume = dataSO.masterVolume;
            stageData.BgmVolume = dataSO.bgmVolume;
            stageData.SfVolume = dataSO.sfxVolume;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}

public class StageData
{
    public int StageLavelNum = 0;
    public float MasterVolume = 0;
    public float BgmVolume = 0;
    public float SfVolume = 0;
}

