
using UnityEngine;
using UnityEngine.UI;


namespace Player.Hud
{
    public class StamUIManager : MonoBehaviour
    {
        [SerializeField] private Image _stamPoint;
        
        public void UpdateStamina(float currentStamina , float maxStamina)
        {
            float percentage = currentStamina / maxStamina;
            if (!Mathf.Approximately(percentage, 1))
                _stamPoint.fillAmount = percentage;
        }
        
    }
}