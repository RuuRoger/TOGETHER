using System;
using UnityEngine;

namespace Assets.Scripts.Collectables
{
    public class Collectable1 : MonoBehaviour
    {
        // ================================================== EVENTS ==================================================
        public static event Action<string> OnCollected;

        // ================================================== PRIVATE METHODS ==================================================
        /// <summary>
        /// Destroy the object and notifiy wich tag object is
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DogPlayer") || other.CompareTag("Player"))
            {
                string sphereTag = this.gameObject.tag;

                OnCollected?.Invoke(sphereTag);
                Destroy(gameObject);
            }
        }
    }
}