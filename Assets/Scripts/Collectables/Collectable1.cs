using System;
using UnityEngine;

namespace Assets.Scripts.Collectables
{
    public class Collectable1 : MonoBehaviour
    {
        // ================================================== EVENTS ==================================================
        public static event Action<string, string, bool> OnCollected;

        // ================================================== PRIVATE METHODS ==================================================
        /// <summary>
        /// Detected sphere tag and player tag
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {

            string sphereTag = this.gameObject.tag;
            string playerTag = other.gameObject.tag;
            bool isCorrect = false;

            if (sphereTag == "Together Points" && (other.CompareTag("DogPlayer") || other.CompareTag("Player")))
            {
                isCorrect = true;
                CheckCorrectSphere(sphereTag, playerTag, isCorrect);
            }

            if (sphereTag == "Dog Points" && other.CompareTag("DogPlayer"))
            {
                isCorrect = true;
                CheckCorrectSphere(sphereTag, playerTag, isCorrect);
            }

            if (sphereTag == "Dog Points" && !other.CompareTag("DogPlayer"))
            {
                CheckCorrectSphere(sphereTag, playerTag, isCorrect);
            }

            if (sphereTag == "Player Points" && other.CompareTag("Player"))
            {
                isCorrect = true;
                CheckCorrectSphere(sphereTag, playerTag, isCorrect);
            }

            if (sphereTag == "Player Points" && !other.CompareTag("Player"))
            {
                CheckCorrectSphere(sphereTag, playerTag, isCorrect);
            }
        }

        /// <summary>
        /// Destroy the object and notifiy wich tag object is and if players get win or lose points
        /// </summary>
        /// <param name="sphereTag"></param>
        /// <param name="isCorrect"></param>
        private void CheckCorrectSphere(string sphereTag, string playerTag, bool isCorrect)
        {
            if (isCorrect)
            {
                isCorrect = true;
                OnCollected?.Invoke(sphereTag, playerTag, isCorrect);
            }
            else
            {
                isCorrect = false;
                OnCollected?.Invoke(sphereTag, playerTag, isCorrect);
            }

            Destroy(gameObject);
        }
    }
}