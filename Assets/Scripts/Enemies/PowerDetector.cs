using UnityEngine;

namespace TOGETHER.Assets.Scripts.Enemies
{
    public class PowerDetector : MonoBehaviour
    {
        private Enemy1 m_enemy;

        private void Awake()
        {
            // Busca el componente Enemy1 en el objeto padre
            m_enemy = GetComponentInParent<Enemy1>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Power"))
            {
                Debug.Log("Power detectado por collider específico!");
                m_enemy.OnPowerDetected();
            }
        }
    }
}