using System;
using System.Collections.Generic;
using Assets.Scripts.Cells;
using UnityEngine;
using Unity.AI.Navigation;


namespace Assets.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private static CellManager m_instance;
        private NavMeshSurface m_navMeshSurface;
        private List<Cell> m_cells = new List<Cell>();

        // ================================================== PROPERTIES ==================================================
        public static CellManager Instance
        {
            get { return m_instance; }
            set { m_instance = value; }
        }

        // ================================================== PUBLIC METHODS ==================================================
        public void AddCell(Cell cell)
        {
            m_cells.Add(cell);
        }

        ///<summary>
        /// Iterate each cell to players's IDcell
        ///</summary>
        public void NotifyChangeColorAccesibleCells(Vector2 idPlayerCell, GameObject selectedPlayer)
        {
            foreach (var cell in m_cells)
            {
                cell.ChangeColorAccesibleCells(idPlayerCell, selectedPlayer);
            }

            m_navMeshSurface.BuildNavMesh();
        }

        public void ResetAllCellsColors()
        {
            foreach (var cell in m_cells)
            {
                cell.ResetAllColors();
            }
            
            m_navMeshSurface.BuildNavMesh();
        }

        public void GetDestinationCellPosition(Vector3 destinationCellPosition)
        {
            PlayerManager.Instance.NotifyToPlayerCellDestination(destinationCellPosition);
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

            m_navMeshSurface = GetComponent<NavMeshSurface>();
        }
    }
}