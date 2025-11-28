using UnityEngine;

public class PauseTransition : MonoBehaviour
{
  [SerializeField]private Animator animator;
  private void OnEnable()
  {
    animator.SetTrigger("pause");
  }
  
}
