using UnityEngine;

namespace TOGETHER.Assets.Scripts.Enemies
{
    public class EnemyColliderShootDark : MonoBehaviour
    {
        private void OnTrigerEnter(Collider other)
        {
            if (other.CompareTag("Ice"))
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}