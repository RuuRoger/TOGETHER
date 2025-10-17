using UnityEngine;

namespace TOGETHER.Assets.Scripts.Enemies
{
    public class EnemyColliderShootIce : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Fire"))
            {
                Destroy(transform.parent.gameObject);
            }   
        }
    }
}