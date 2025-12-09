using System;
using System.Collections;
using Player.Hud;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class GameManager : MonoBehaviour
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject DeathScreen;
  [SerializeField] private GameObject WinScreen;
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private GameObject S;
  [SerializeField] private GameObject A;
  [SerializeField] private GameObject B;
  [SerializeField] private GameObject C;
  [SerializeField] private int maxScore = 1000;
  private int _timeScore;
  
  [SerializeField] private TimeManager timeManager;
  
  private string sceneName;
  [SerializeField] private float rankDisplayWait;
  
  private int currentScore = 0;
  private void Start()
  {
    timeManager = GetComponentInChildren<TimeManager>();
    DeathScreen.SetActive(false);
    WinScreen.SetActive(false);
  }
  public void Death()
  {
    SoundManager.PlaySound("Death");
    Time.timeScale = 0;
    DeathScreen.SetActive(true);
  }

  public void Retry()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void ReLoadScene()
  {
    Retry();
    Time.timeScale = 1;
    DeathScreen.SetActive(false);
  }

  public void ShowRankDisplay()
  {
    StartCoroutine(DisplayFinalScore());
  }
  
  
  private IEnumerator DisplayFinalScore()
  {
    MusicScript.PauseSong();
    timeManager.Stop();
    
    //calculate score
    
    _timeScore = (int)timeManager.time;
    int scoreToDivide = _timeScore * 10;
    maxScore -= scoreToDivide;
    currentScore += maxScore;
    WinScreen.SetActive(true);
    int iStart = currentScore / 2;
    
    for (int i = iStart; i < currentScore; i += 10)
    {
      scoreText.text = i.ToString();
      yield return new WaitForSeconds(.001f);
    }
    
    if (currentScore >= 1000) S.SetActive(true);
    else if (currentScore >= 900) A.SetActive(true);
    else if (currentScore >= 700) B.SetActive(true);
    else if (currentScore < 700) C.SetActive(true);
      
  }
  
  public UnityAction AddScore(int score)
  {
    Debug.Log(score);
    currentScore += score;

    return null;
  }

}
