using System;
using UnityEngine;
using Player;
using UnityEngine.Events;

namespace Items
{
    public class HealthPickups : MonoBehaviour , IItem
    {
        [SerializeField]private PlayerHealth _playerHealth;
        [SerializeField]private int healAmmount = 1;
        public UnityEvent OnPickUp;

        private void Start()
        {
            OnPickUp.AddListener(PickUpHealth);
        }

        public void PickUpHealth()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().HealthUp(healAmmount);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _playerHealth = collision.GetComponent<PlayerHealth>();
        }
        public void Collect()
        {
            OnPickUp.Invoke();
            Destroy(gameObject);
        }
    }
}