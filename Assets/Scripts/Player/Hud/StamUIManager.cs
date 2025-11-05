
using UnityEngine;
using UnityEngine.UI;


namespace Player.Hud
{
    public class StamUIManager
    {
        private int _stamIndex = 3;
        private int _maxstamIndex = 3;
        [SerializeField] private Image[] _stamPoint;

        private void Start()
        {
            for (int i = 0; i < _stamPoint.Length; i++)
            {
                _stamPoint[i].GetComponent<Image>().enabled = true;
            }
        }

        public void UpdateStamina()
        {
            
        }

        public void RestoreStamina()
        {
            
        }

    }
}