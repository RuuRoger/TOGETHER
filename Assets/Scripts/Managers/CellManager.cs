using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private static CellManager m_instance;

        // ================================================== PROPERTIES ==================================================
        public static CellManager Instance
        {
            get { return m_instance; }
            set { m_instance = value; }
        }

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