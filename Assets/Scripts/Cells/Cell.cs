using System;
using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class Cell : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private Vector2 m_idCell;

        // =================================== PROPERTIES ===================================
        public Vector2 IDCell
        {
            get { return m_idCell; }
            set { m_idCell = value; }
        }
    }
}