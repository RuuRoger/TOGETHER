using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        // ================================================== PROPERTIES ==================================================
        public static GameManager Instance {get; set;}

        // ================================================== PRIVATE METHODS ==================================================
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}