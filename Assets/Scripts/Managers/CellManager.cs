using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using Assets.Scripts.Cells;


namespace Assets.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private NavMeshSurface m_navMeshSurface;
        private List<Cell> m_cells = new List<Cell>();

        // ================================================== PROPERTIES ==================================================
        public static CellManager Instance {get; set;}

        // ================================================== PUBLIC METHODS ==================================================
        
        ///<summary>
        /// Add cells in list
        /// </summary>
        public void AddCell(Cell cell)
        {
            m_cells.Add(cell);
        }

        ///<summary>
        /// Iterate each cell to players's IDcell
        ///</summary>
        public void NotifyChangeColorAccesibleCells(Vector2 idPlayerCell, GameObject selectedPlayer)
        {
            // Evaluate all cells
            foreach (var cell in m_cells)
            {
                cell.ChangeColorAccesibleCells(idPlayerCell, selectedPlayer);
            }

            // Delete not accesible
            foreach (var cell in m_cells)
            {
                cell.CheckIsolatedCell();
            }

            m_navMeshSurface.BuildNavMesh();
        }

        /// <summary>
        /// Change all colors to green
        /// </summary>
        public void ResetAllCellsColors()
        {
            foreach (var cell in m_cells)
            {
                cell.ResetAllColors();
            }
            
            m_navMeshSurface.BuildNavMesh();
        }

        /// <summary>
        /// Select the cell position to player goes
        /// </summary>
        /// <param name="destinationCellPosition"> Destination position</param>
        public void GetDestinationCellPosition(Vector3 destinationCellPosition)
        {
            PlayerManager.Instance.NotifyToPlayerCellDestination(destinationCellPosition);
        }

        public List<Cell> GetAccessibleCells(Vector2 idCell)
        {
            List<Cell> activeCells = new List<Cell>();
            
            foreach (Cell cell in m_cells)
            {
                bool isNeighbor = (
                    (cell.IDCell.x == idCell.x && Mathf.Abs(cell.IDCell.y - idCell.y) == 1) ||
                    (cell.IDCell.y == idCell.y && Mathf.Abs(cell.IDCell.x - idCell.x) == 1)
                );
                
                if (isNeighbor && cell.IsAccesible)
                {
                    activeCells.Add(cell);
                }
            }
            
            return activeCells;
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