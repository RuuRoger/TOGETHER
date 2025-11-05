using System;
using UnityEngine;
using Assets.Scripts.Cells;

namespace Assets.Scripts.Players
{
    public class BasePlayer : MonoBehaviour
    {
        // =================================== EVENTS ===================================
        public event Action<Vector2> OnPlayerPosition; //Goes to CellManager

        // =================================== PRIVATE METHODS ===================================
        private void OnMouseDown()
        {
            ReadIdCell();
        }

        private void ReadIdCell()
        {
            Ray ray = new(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5f))
            {
                Cell cell = hit.collider.GetComponent<Cell>();
                if (cell != null)
                {
                    Vector2 idCell = cell.IDCell;
                    OnPlayerPosition?.Invoke(idCell);
                }
            }
        }
    }
}