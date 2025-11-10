using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Cells;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Players
{
    public class BasePlayer : MonoBehaviour
    {
        // ================================================== PRIVATE METHODS ==================================================
        private void OnMouseDown()
        {
            ReadIdCell();
        }

        ///<summary>
        /// Trhow a raycast to read ID Cell
        /// Notofy to CellManager wich ID is
        ///</summary>
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
                    CellManager.Instance.NotifyChangeColorAccesibleCells(idCell);
                }
            }
        }
    }
}