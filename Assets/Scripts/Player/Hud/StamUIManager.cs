
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

        public void UpdateStam()
        {
            if (_stamIndex < 0) return;
            _stamPoint[_stamIndex - 1].GetComponent<Image>().enabled = false;
            _stamIndex--;
            Debug.Log(_stamIndex);
        }

        public void RestoreHealth()
        {
            if (_stamIndex > _stamIndex) return;
            _stamPoint[_stamIndex].GetComponent<Image>().enabled = true;
            _stamIndex++;
            Debug.Log(_stamIndex);
        }

    }
}