
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
            _stamPoint.fillAmount = percentage;
        }
    }
}