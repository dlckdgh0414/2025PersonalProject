using System.Threading.Tasks;
using UnityEngine;

public class SelectTurtuoil : MonoBehaviour
{
    private int _count = 0;
    [SerializeField] private TutorialUI tutorialUI;
    private async void Start()
    {
        await Awaitable.WaitForSecondsAsync(1f);
        if (_count <= 0)
        {
            tutorialUI.gameObject.SetActive(true);
            _count++;
            gameObject.SetActive(false);
        }
    }
}
