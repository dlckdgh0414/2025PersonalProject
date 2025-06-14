using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSellection : MonoBehaviour
{
    [SerializeField] private List<GameObject> starList;
    [SerializeField] private GameEventChannelSO stageSelectEvent;
    [SerializeField] private DataSO data;
    [SerializeField] private GameObject[] stageSelectBnt;
    private int _count;

    private void Awake()
    {
        for(int i = 0; i < stageSelectBnt.Length; i++)
        {
            stageSelectBnt[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        foreach (GameObject star in starList)
        {
            star.SetActive(false);
        }

        foreach (var btn in stageSelectBnt)
        {
            btn.SetActive(false);
        }


        for (int i = 0; i < data.ClearStageNum - 2; i++)
        {
            stageSelectBnt[i].SetActive(true);
        }

        for (int stage = 0; stage < data.ClearStageStarCount.Count; stage++)
        {
            int starCount = data.ClearStageStarCount[stage];

            for (int i = 0; i < starCount; i++)
            {
                int starIndex = stage * 3 + i; 
                if (starIndex < starList.Count)
                {
                    starList[starIndex].SetActive(true);
                }
            }
        }
    }

    public void StageSelect(int StageNum)
    {
        stageSelectEvent.RaiseEvent(SceneChangeEvents.SceneChnages.Initializer(StageNum));
    }
}
