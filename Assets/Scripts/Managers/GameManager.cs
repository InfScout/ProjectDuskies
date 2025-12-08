using System;
using System.Collections;
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
  
  private string sceneName;
  [SerializeField] private float rankDisplayWait;
  
  private int currentScore = 0;
  private void Start()
  {
    DeathScreen.SetActive(false);
    WinScreen.SetActive(false);
  }
  public void Death()
  {
    SoundManager.PlaySound("Death");
    Time.timeScale = 0;
    DeathScreen.SetActive(true);
  }

  public void ReLoadScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timeScale = 1;
    DeathScreen.SetActive(false);
  }

  public void NextLevel()
  {
    StartCoroutine(DisplayFinalScore());
  }
  
  
  private IEnumerator DisplayFinalScore()
  {
    WinScreen.SetActive(true);
    for (int i = 0; i < currentScore; i++)
    {
      scoreText.text = i.ToString();
    }

    yield return new WaitForSeconds(rankDisplayWait);

    if (currentScore > 1000)
    {
      S.SetActive(true);
    }
    else if (currentScore > 900)
    {
      A.SetActive(true);
    }
    else if (currentScore > 700)
    {
      B.SetActive(true);
    }
    else
    {
      C.SetActive(true);
    }
    
  }
  
  public UnityAction AddScore(int score)
  {
    currentScore += score;

    return null;
  }

}
