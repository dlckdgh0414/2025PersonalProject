using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] RectTransform scooterImg;
    [SerializeField] private GameEventChannelSO sceneChangeEvent;

    private void OnEnable()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);

        scooterImg.gameObject.SetActive(false);

        sceneChangeEvent.AddListener<SceneChnages>(HandleSceneChange);
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeImage.color = Color.black;
        fadeImage.DOFade(0, 1f).SetEase(Ease.InOutSine);
    }

    private void HandleSceneChange(SceneChnages evt)
    {
        fadeImage.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        seq.Append(fadeImage.DOFade(1f, 1f)).AppendCallback(() => scooterImg.gameObject.SetActive(true));
        seq.Append(scooterImg.DOAnchorPos(new Vector2(-1180, -354), 2.5f).SetEase(Ease.Linear));

        seq.OnComplete(() =>
        {
            SceneManager.LoadScene(evt.LoadSceneCount);
        });
    }

    private void OnDisable()
    {
        sceneChangeEvent.RemoveListener<SceneChnages>(HandleSceneChange);
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }
}
