using System.Threading.Tasks;
using UnityEngine;

public class SelectTurtuoil : MonoBehaviour
{
    private int _count = 0;
    [SerializeField] private TutorialUI tutorialUI;
    [SerializeField] private GameObject system;
    private async void Start()
    {
        await Awaitable.WaitForSecondsAsync(2f);
        if (_count <= 0)
        {
            tutorialUI.gameObject.SetActive(true);
            _count++;
            system.gameObject.SetActive(false);
        }
    }
}
