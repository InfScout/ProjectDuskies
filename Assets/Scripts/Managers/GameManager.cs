using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject DeathScreen;
  private string sceneName;

  private void Start()
  {
    DeathScreen.SetActive(false);
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



}
