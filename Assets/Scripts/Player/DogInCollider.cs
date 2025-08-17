using System;
using UnityEngine;

namespace TOGETHER.Assets.Scripts.Player
{
    public class DogInCollider : MonoBehaviour
    {
        public event Action OnInsideZone;
        public event Action OnOutZone;

        private void OnTriggerEnter(Collider other)
        {
            OnInsideZone?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            OnOutZone?.Invoke();
        }
    }
}