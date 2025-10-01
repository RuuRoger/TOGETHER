using UnityEngine;

namespace TOGETHER.Assets.Scripts.Player
{
    /// <summary>
    /// Gestiona los poderes del jugador y permite cambiar entre ellos
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        // Array de poderes disponibles (asignar en el Inspector)
        [SerializeField] private GameObject[] m_playerPower;
        
        // Poder actualmente seleccionado
        private GameObject m_powerSelected;
        
        // Índice del poder actual en el array
        private byte m_index;

        /// <summary>
        /// Propiedad pública de solo lectura para acceder al poder seleccionado
        /// </summary>
        public GameObject PowerSelected
        {
            get
            {
                return m_powerSelected;
            }
        }

        /// <summary>
        /// Propiedad pública de solo lectura para acceder al array completo de poderes
        /// </summary>
        public GameObject[] PlayerPowers
        {
            get
            {
                return m_playerPower;
            }
        }

        private void Awake()
        {
            // Inicializa el índice en 0 (primer poder)
            m_index = 0;
        }

        private void Start()
        {
            // Establece el primer poder como el seleccionado al inicio
            m_powerSelected = m_playerPower[m_index];
        }

        private void Update()
        {
            // Llama al método que gestiona el cambio de poderes cada frame
            PowerHandler();
        }

        /// <summary>
        /// Gestiona el cambio de poderes cuando se presiona la tecla R
        /// </summary>
        public void PowerHandler()
        {
            // Si se presiona la tecla R
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Incrementa el índice para pasar al siguiente poder
                m_index++;
                
                // Si el índice supera el tamaño del array, vuelve a 0 (ciclo)
                if (m_index >= m_playerPower.Length)
                {
                    m_index = 0;
                }
                
                // Actualiza el poder seleccionado con el nuevo índice
                m_powerSelected = m_playerPower[m_index];
            }
        }
    }
}