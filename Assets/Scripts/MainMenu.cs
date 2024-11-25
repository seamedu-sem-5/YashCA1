using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneName);

    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
