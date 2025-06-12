using UnityEngine;

[CreateAssetMenu(fileName = "DataSO", menuName = "SO/Data/DataSO")]
public class DataSO : ScriptableObject
{
    public int ClearStageNum = 1;
    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;
}
