using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSO", menuName = "SO/Data/DataSO")]
public class DataSO : ScriptableObject
{
    public int ClearStageNum = 1;
    public float MasterVolume;
    public float BgmVolume;
    public float SfxVolume;
    public bool IsTutorialClear = false;
    public List<int> ClearStageStarCount;
}
