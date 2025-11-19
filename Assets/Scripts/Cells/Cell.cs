using System;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class Cell : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private Vector3 m_halfExtents = new Vector3(0.5f, 0.5f, 0.5f);
        private Vector2 m_idCell;
        private Renderer m_cellRender;
        private Color m_greenColour = new(0.4f, 0.7f, 0.2f);
        private Color m_blueColour = new(0.2f, 0.7f, 0.7f);
        private bool m_isAccesible = false;

        // ================================================== PROPERTIES ==================================================
        public Vector2 IDCell
        {
            get { return m_idCell; }
            set { m_idCell = value; }
        }

        public bool IsAccesible
        {
            get { return m_isAccesible; }
            set { m_isAccesible = value; }
        }

        // ================================================== PUBLIC METHODS ==================================================
        /*
            Change the cell color if is accesible or not
            - Is necessary the id where player is in that moment
            - with this is possible to calculate the distance between the player's cell and the destination's cell
            - if the distance is 1 in axis x and y, is accesible
            - BUT this cell must be "empty"
            - we evalute if is empty o rnot with an overlapbox, catchiong all colliders in area
            - if the collider is diferent about the cell collider and the player, it's not accesible
        */
        public void ChangeColorAccesibleCells(Vector2 idPlayerCell)
        {
            float distanceX = Math.Abs(idPlayerCell.x - IDCell.x);
            float distanceY = Math.Abs(idPlayerCell.y - IDCell.y);

            if (distanceX <= 1f && distanceY <= 1f)
            {
                Vector3 boxCenter = transform.position + Vector3.up * 0.5f;
                Collider[] colliders = Physics.OverlapBox(boxCenter, m_halfExtents);
                bool hasObstacle = false;

                foreach (var collider in colliders)
                {
                    if (collider.gameObject != this.gameObject && !collider.CompareTag("DogPlayer"))
                    {
                        hasObstacle = true;
                        break;
                    }
                }

                if(hasObstacle)
                {
                    m_isAccesible = false;
                    m_cellRender.material.color = m_greenColour;
                }
                else
                {
                    m_isAccesible = true;
                    m_cellRender.material.color = m_blueColour;
                }

            }
            else
            {
                m_isAccesible = false;
                m_cellRender.material.color = m_greenColour;
            }
        
        }

        public void ResetAllColors()
        {
            m_isAccesible = false;
            m_cellRender.material.color = m_greenColour;
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void Awake()
        {
            m_cellRender = GetComponent<Renderer>();
        }

        private void OnMouseDown()
        {
            if (!m_isAccesible)
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