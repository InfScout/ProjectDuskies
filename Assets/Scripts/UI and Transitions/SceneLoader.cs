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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SceneSwitch(int sceneName)
    {
        FadeTransition(sceneName);
    }

    async void FadeTransition(int sceneName)
    {
        await Screen.instance.FadeIn();
        SceneManager.LoadScene(sceneName);
        await Screen.instance.FadeOut();
    }
    
}
