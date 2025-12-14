using System;
using UnityEngine;

namespace Assets.Scripts.Collectables
{
    public class Collectable1 : MonoBehaviour
    {
        // ================================================== EVENTS ==================================================
        public event Action OnCollected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Dog") || other.CompareTag("Player"))
            {
                OnCollected?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}