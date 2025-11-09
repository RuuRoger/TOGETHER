using System;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Cells
{
    public class Cell : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private CellManager m_cellManger;
        private Renderer m_render;
        private Vector2 m_idCell;
        private Color m_cellGreenColor = new(0.4f, 0.7f, 0.02f, 1f);
        private Color m_cellBlueColor = new(0.4f, 0.7f, 0.9f, 1f);
        private bool m_isAccesible;

        // =================================== PROPERTIES ===================================
        public Vector2 IDCell
        {
            get { return m_idCell; }
            set { m_idCell = value; }
        }

        // =================================== EVENTS SUSCRIPTIONS ===================================
        private void Start()
        {
            m_render.material.color = m_cellGreenColor;
            m_cellManger ??= FindFirstObjectByType<CellManager>();
            if (m_cellManger != null)
            {
                m_cellManger.OnStartTurn += CheckCellAccessibility;
            }
        }
        private void OnDisable()
        {
            if (m_cellManger != null)
            {
                m_cellManger.OnStartTurn -= CheckCellAccessibility;
            }
        }

        // =================================== PUBLIC METHODS ===================================
        public void ResetColor()
        {
            m_isAccesible = false;
            m_render.material.color = m_cellGreenColor;
        }

        // =================================== PRIVATE METHODS ===================================
        private void Awake()
        {
            m_render = GetComponent<Renderer>();
            m_isAccesible = false;
            m_cellManger = FindFirstObjectByType<CellManager>();
        }


        private void OnMouseDown()
        {
            if (!m_isAccesible)
            {
                m_cellManger.ResetStatus();
            }
        }

        private void CheckCellAccessibility(Vector2 dogCellID)
        {
            float distanceX = Math.Abs(dogCellID.x - IDCell.x);
            float distanceY = Math.Abs(dogCellID.y - IDCell.y);

            if (distanceX <= 2f && distanceY <= 2f)
            {
                m_isAccesible = true;
                m_render.material.color = m_cellBlueColor;
            }
            else
            {
                m_isAccesible = false;
                m_render.material.color = m_cellGreenColor;
            }
        }
    }
}