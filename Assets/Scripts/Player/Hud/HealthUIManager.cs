using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Hud
{
    public class HealthUIManager : MonoBehaviour
    {
        private int _healthIndex = 3;
        private int _maxHealthIndex = 3;
        [SerializeField] private Image[] _healthPoint;

        private void Start()
        {
            for (int i = 0; i < _healthPoint.Length; i++)
            {
                _healthPoint[i].GetComponent<Image>().enabled = true;
            }
        }

        public void UpdateHealth()
        {
            if (_healthIndex < 0) return;
            _healthPoint[_healthIndex - 1].GetComponent<Image>().enabled = false;
            _healthIndex--;
            Debug.Log(_healthIndex);
        }

        public void RestoreHealth()
        {
            if (_healthIndex > _maxHealthIndex) return;
            _healthPoint[_healthIndex].GetComponent<Image>().enabled = true;
            _healthIndex++;
            Debug.Log(_healthIndex);
        }
        
        
    }
}