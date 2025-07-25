using System.Threading.Tasks;
using UnityEngine;

public class SelectTurtuoil : MonoBehaviour
{
    private int _count = 0;
    [SerializeField] private TutorialUI tutorialUI;
    [SerializeField] private GameObject system;
    [SerializeField] private DataSO dataSO;
    private async void Start()
    {
        await Awaitable.WaitForSecondsAsync(1.75f);
        if (_count <= 0&& !dataSO.IsTutorialClear)
        {
            tutorialUI.gameObject.SetActive(true);
            _count++;
            system.gameObject.SetActive(false);
        }
    }
}
