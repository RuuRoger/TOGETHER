using System;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class ItemPoints : MonoBehaviour
    {
        private void OnTriggerEnter()
        {
            Destroy(gameObject);
        }
    }
}