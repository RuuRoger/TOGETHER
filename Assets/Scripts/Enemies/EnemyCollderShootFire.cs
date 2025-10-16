using UnityEngine;
using TOGETHER.Assets.Scripts.Player;

namespace TOGETHER.Assets.Scripts.Enemies
{
    public class EnemyColliderShootFire : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ice"))
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}