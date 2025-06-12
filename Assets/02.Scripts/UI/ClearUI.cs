using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ClearUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI deliveryTimeText;
    [SerializeField] private int nextSceneIdx;
    [SerializeField] private float threestarsMin,threestarsSec;
    [SerializeField] private float twostarsMin, twostarsSec;
    [SerializeField] private Image[] stars;
    [SerializeField] private GameEventChannelSO sceneChangeEvent;
    [SerializeField] private DataSO dataSO;

    private void Awake()
    {
        foreach (var star in stars)
        {
            star.transform.localScale = Vector3.zero;
            star.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        DataManager.Intance.SaveData();
    }


    public void SetDeliveryTime(string deliveryTime , float minTime,float secTime)
    {
        deliveryTimeText.text = "배달 시간 : " + deliveryTime;
        int starCount = 1;

        if (minTime <= threestarsMin && secTime <= threestarsSec)
            starCount = 3;
        else if (minTime <= twostarsMin && secTime <= twostarsSec)
            starCount = 2;

        ShowStarsAnimated(starCount);
    }

    private void ShowStarsAnimated(int count)
    {
        float delay = 0f;
        for (int i = 0; i < count && i < stars.Length; i++)
        {
            Image star = stars[i];
            star.gameObject.SetActive(true);
            star.transform.localScale = Vector3.zero;

            star.transform.DOScale(Vector3.one, 0.4f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack);

            delay += 0.8f;
        }
    }

    public void NextScene()
    {
        dataSO.ClearStageNum = nextSceneIdx;
        sceneChangeEvent.RaiseEvent(SceneChangeEvents.SceneChnages.Initializer(nextSceneIdx));
    }

    public void MainMenu()
    {
        dataSO.ClearStageNum = nextSceneIdx;
        sceneChangeEvent.RaiseEvent(SceneChangeEvents.SceneChnages.Initializer(0));
    }
}
