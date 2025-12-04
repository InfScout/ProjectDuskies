using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
  [SerializeField] private GameObject player;
  private string sceneName;
  
  public void Death()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }



}
