using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayBnt()
    {
        SceneManager.LoadScene(1);
    }

    public void OutBnt()
    {
        Application.Quit();
    }
}
