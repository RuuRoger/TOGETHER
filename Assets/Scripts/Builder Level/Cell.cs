using UnityEngine;

namespace Assets.Scripts.BuilderLevel
{
    public class Cell : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private Vector2 m_idCell;

        // =================================== PROPERTIES ===================================
        public Vector2 IdCell
        {
            get => m_idCell;
            set => m_idCell = value;
        }

        // =================================== PRIVATE METHODS ===================================
        private void OnMouseDown()
        {
            Debug.Log($"ID: {IdCell}");
        }
    }
}