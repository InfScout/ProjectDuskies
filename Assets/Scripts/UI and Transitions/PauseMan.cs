using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMan : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    private bool _isPaused = false;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
    }
    
    
    public void PauseMethod(InputAction.CallbackContext context)
    {
        if (context.performed & _isPaused)
        {
            ResumeGame();
        }
        else if  (context.performed & !_isPaused)
        {
            PauseGame();
        }
    }
    
    

    private void PauseGame()
    {
        MusicScript.PausedBGM();
        _pauseMenu.SetActive(true);
        _isPaused = true;
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        MusicScript.UnpausedBGM();
        _pauseMenu.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1;
    }

    public void PauseButton()
    {
        PauseGame();
    }
    
    
    public void ResumeButton()
    {
        ResumeGame();
    }
}
