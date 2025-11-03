using System;
using UnityEngine;
using Assets.Scripts.Cells;

namespace Assets.Scripts.Players
{
    public class DogPlayer : MonoBehaviour
    {
        // =================================== EVENTS ===================================
        public event Action<Vector2> OnDogPosition; //Goes to CellManager

        // =================================== PUBLIC METHODS ===================================
        public void ReadIdCell()
        {
            Ray ray = new(transform.position, Vector3.down);
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