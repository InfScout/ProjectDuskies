using UnityEngine;

public class SFXButton : MonoBehaviour
{
  public void Hover()
  {
    SoundManager.PlaySound("Buttons");
  }
  
  
  
  
  public void Press()
  {
    SoundManager.PlaySound("Click");
  }
}
