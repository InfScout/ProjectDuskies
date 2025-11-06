using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private void Awake()
    {
        // Singleton pattern setup
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
    
    public void SceneSwitch(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
