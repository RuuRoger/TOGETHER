using System;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class Cell : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private Vector2 m_idCell;
        private Color m_greenColour = new(0.4f, 0.7f, 0.2f);
        private Color m_blueColour = new(0.2f, 0.7f, 0.7f);
        private Renderer m_cellRender;
        private bool m_isAccesible = false;

        // ================================================== PUBLIC METHODS ==================================================
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

        public void ChangeColorAccesibleCells(Vector2 idPlayerCell)
        {
            float distanceX = Math.Abs(idPlayerCell.x - IDCell.x);
            float distanceY = Math.Abs(idPlayerCell.y - IDCell.y);


            if (distanceX <= 2f && distanceY <= 2f)
            {
                m_isAccesible = true;
                m_cellRender.material.color = m_blueColour;
            }
            else
            {
                m_isAccesible = false;
                m_cellRender.material.color = m_greenColour;
            }
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void Awake()
        {
            m_cellRender = GetComponent<Renderer>();
        }

        private void OnMouseDown()
        {

        }
    }
}