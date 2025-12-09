using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SceneSwitch(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // async void FadeTransition(int sceneName)
    // {
    //     await Screen.instance.FadeIn();
    //     SceneManager.LoadScene(sceneName);
    //     await Screen.instance.FadeOut();
    // }
    //
}
