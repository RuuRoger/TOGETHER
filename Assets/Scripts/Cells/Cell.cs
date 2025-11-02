using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class Cell : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private Vector2 m_idCell;
        private bool m_isAccesible;
        private Renderer m_render;
        private Color m_cellColor = new(0.4f, 0.7f, 0.02f, 1f);
        private GameObject m_dog;

        // =================================== PROPERTIES ===================================
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

        public Color CellColor
        {
            get { return m_cellColor; }
            set { m_cellColor = value; }
        }

        // =================================== PRIVATE METHODS ===================================
        private void Awake()
        {
            m_render = GetComponent<Renderer>();
            m_isAccesible = false;
            m_dog = GameObject.FindGameObjectWithTag("Dog");
        }

        private void Start()
        {
            m_render.material.color = m_cellColor;
        }

        private void OnMouseDown()
        {
            Debug.Log(IDCell);
        }
    }
}