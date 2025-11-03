using System;
using UnityEngine;
using Assets.Scripts.Cells;

namespace Assets.Scripts.Players
{
    public class Player : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private Vector3 m_currentPlayerPosition;

        // =================================== EVENTS ===================================
        public event Action<Vector2> OnDogPosition; //Goes to CellManager

        // =================================== PRIVATE METHODS ===================================
        private void Start()
        {
            ReadIdCell();
        }

        private void ReadIdCell()
        {
            Ray ray = new(transform.position, -(transform.up));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5f))
            {
                Cell cell = hit.collider.GetComponent<Cell>();
                Vector2 idCell = cell.IDCell;
                OnDogPosition?.Invoke(idCell);
            }
        }
    }
}