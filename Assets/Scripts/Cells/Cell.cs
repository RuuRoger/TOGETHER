using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Cells
{
    public class Cell : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private NavMeshModifier m_naveMeshModifier;
        private Renderer m_cellRender;
        private Vector3 m_halfExtents = new Vector3(0.5f, 0.5f, 0.5f); //Using for OverlapBox
        private Color m_greenColour = new(0.4f, 0.7f, 0.2f);
        private Color m_blueColour = new(0.2f, 0.7f, 0.7f);

        // ================================================== PROPERTIES ==================================================
        public Vector2 IDCell {get; set;}
        public bool IsAccesible {get; set;}

        // ================================================== PUBLIC METHODS ==================================================
  
        /// <summary>
        /// Change the cell color if is accesible or not
        /// - Is necessary the id where player is in that moment
        /// - with this is possible to calculate the distance between the player's cell and the destination's cell
        /// - if the distance is 2 in axis x and y, is accesible
        /// - BUT this cell must be "empty"
        /// - we evalute if is empty o rnot with an overlapbox, catchiong all colliders in area
        /// - if the collider is diferent about the cell collider and the player, it's not accesible
        /// </summary>
        /// <param name="idPlayerCell"> The cell id where player is it</param>
        /// <param name="selectedPlayer">GameObject Player selected</param>
        public void ChangeColorAccesibleCells(Vector2 idPlayerCell, GameObject selectedPlayer)
        {
            float distanceX = Math.Abs(idPlayerCell.x - IDCell.x);
            float distanceY = Math.Abs(idPlayerCell.y - IDCell.y);

            if (distanceX <= 2f && distanceY <= 2f)
            {
                Vector3 boxCenter = transform.position + Vector3.up * 0.5f;
                Collider[] colliders = Physics.OverlapBox(boxCenter, m_halfExtents);
                bool hasObstacle = false;

                foreach (var collider in colliders)
                {
                    if (
                        collider.gameObject != this.gameObject && 
                        collider.gameObject != selectedPlayer && 
                        !collider.gameObject.CompareTag("Dog Points") &&
                        !collider.gameObject.CompareTag("Player Points")  &&
                        !collider.gameObject.CompareTag("Together Points"))
                    {
                        hasObstacle = true;
                        break;
                    }
                }

                if(hasObstacle)
                {
                    IsAccesible = false;
                    m_cellRender.material.color = m_greenColour;
                    m_naveMeshModifier.area = 1; //Not walkeable
                }
                else
                {
                    IsAccesible = true;
                    m_cellRender.material.color = m_blueColour;
                    m_naveMeshModifier.area = 0; // Walkeable
                }
            }
            else
            {
                IsAccesible = false;
                m_cellRender.material.color = m_greenColour;
            }
        }

        /// <summary>
        /// Put the colour cell as green and make it not walkeable
        /// </summary>
        public void ResetAllColors()
        {
            IsAccesible = false;
            m_cellRender.material.color = m_greenColour;
            m_naveMeshModifier.area = 1; //Not "walkeable"
        }

        /// <summary>
        /// Checks if this cell is isolated (no accessible neighbors) and marks it as not accessible
        /// </summary>
        public void CheckIsolatedCell()
        {
            if (IsAccesible)
            {
                List<Cell> neighbours = CellManager.Instance.GetAccessibleCells(IDCell);
                if (neighbours.Count == 0)
                {
                    IsAccesible = false;
                    m_cellRender.material.color = m_greenColour;
                    m_naveMeshModifier.area = 1;  //Not Walkeable
                }
            }
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void Awake()
        {
            m_cellRender = GetComponent<Renderer>();
            m_naveMeshModifier = GetComponent<NavMeshModifier>();
        }

        private void OnMouseDown()
        {
            if (!IsAccesible)
            {
                CellManager.Instance.ResetAllCellsColors();
            }
            else
            {
                Vector3 currentCellPosition = this.transform.position;
                CellManager.Instance.GetDestinationCellPosition(currentCellPosition);
            }
        }
    }
}