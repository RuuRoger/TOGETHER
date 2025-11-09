using System;
using UnityEngine;
using Assets.Scripts.Cells;

namespace Assets.Scripts.Players
{
    public class BasePlayer : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private Transform m_currentPosition;
        private bool m_isSelected = false;

        // ================================================== PROPERTIES ==================================================
        //Maybe this is not necessary...
        public Transform CurrentPosition
        {
            get { return m_currentPosition; }
            set { m_currentPosition = value; }
        }
        public bool IsSelected
        {
            get { return m_isSelected; }
            set { m_isSelected = value; }
        }

        // ================================================== EVENTS ==================================================
        public event Action<Vector2> OnIDCellPlayer; //Goes to CellManager
        public event Action<bool> OnPlayerSelected; //Goes to PlayerManager

        // ================================================== PRIVATE METHODS ==================================================
        private void OnMouseDown()
        {
            m_isSelected = true;
            PlayerIsSelected();
            ReadIdCell();
        }

        private void PlayerIsSelected()
        {
            OnPlayerSelected?.Invoke(m_isSelected);
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
                    OnIDCellPlayer?.Invoke(idCell);
                }
            }
        }
    }
}