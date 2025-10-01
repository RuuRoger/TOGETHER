using UnityEngine;

namespace TOGETHER.Assets.Scripts.Player
{
    public class PlayerShoogting : MonoBehaviour
    {
        [Header("Scale Settings")]
        [SerializeField] private float m_minScale;
        [SerializeField] private float m_maxScale;
        [SerializeField] private float m_scaleSpeed;
        
        private PlayerMove m_playerMove;
        private PlayerManager m_playerManager;
        private float m_currentScale;
        private Vector3 m_originalScale;
        private GameObject m_lastSelectedPower; // Para detectar cambios de poder

        private void Awake()
        {
            m_playerMove = GetComponent<PlayerMove>();
            m_playerManager = GetComponent<PlayerManager>();
            m_currentScale = m_minScale;
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            GameObject selectedPower = m_playerManager.PowerSelected;
            
            // Si cambió el poder seleccionado, resetear la escala
            if (selectedPower != m_lastSelectedPower)
            {
                // Restaurar escala del poder anterior si existe
                if (m_lastSelectedPower != null && m_currentScale > m_minScale)
                {
                    m_lastSelectedPower.transform.localScale = m_originalScale;
                }
                
                // Resetear variables
                m_currentScale = m_minScale;
                m_lastSelectedPower = selectedPower;
            }
            
            // Si está disparando (manteniendo espacio)
            if (m_playerMove.isShooting)
            {
                // Guardar escala original la primera vez
                if (m_currentScale == m_minScale)
                {
                    m_originalScale = selectedPower.transform.localScale;
                }
                
                // Incrementar la escala gradualmente
                m_currentScale += m_scaleSpeed * Time.deltaTime;
                m_currentScale = Mathf.Min(m_currentScale, m_maxScale); // Limitar al máximo
                
                // Aplicar la escala al objeto seleccionado
                selectedPower.transform.localScale = m_originalScale * m_currentScale;
            }
            else
            {
                // Cuando se suelta espacio, resetear la escala
                if (selectedPower != null && m_currentScale > m_minScale)
                {
                    // Aquí irá el código de disparo después
                    Debug.Log($"Disparando con escala: {m_currentScale}");
                    
                    // Restaurar escala original
                    selectedPower.transform.localScale = m_originalScale;
                }
                
                // Resetear la escala actual
                m_currentScale = m_minScale;
            }
        }
    }
}