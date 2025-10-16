using UnityEngine;
using TOGETHER.Assets.Scripts.Player;

namespace TOGETHER.Assets.Scripts.Enemies
{
    public class EnemyColliderShoot : MonoBehaviour
    {
        private PlayerShoogting m_power;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Power"))
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}