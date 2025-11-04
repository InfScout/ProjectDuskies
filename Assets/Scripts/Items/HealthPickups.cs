using UnityEngine;
using Player;

namespace Items
{
    public class HealthPickups : MonoBehaviour , IItem
    {
        [SerializeField]private PlayerHealth _playerHealth;
        [SerializeField]private int healAmmount = 1;

        private void Awake()
        {
            if (_playerHealth == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    _playerHealth = player.GetComponent<PlayerHealth>();
                }
            }
        }
        public void Collect()
        {
            _playerHealth.HealthUp(healAmmount);
            
            Destroy(gameObject);
            Debug.Log("destroyed");
        }
    }
}