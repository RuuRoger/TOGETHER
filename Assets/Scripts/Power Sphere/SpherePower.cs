using UnityEngine;
using TOGETHER.Assets.Scripts.Player;

namespace TOGETHER.Assets.Scripts.PowerSphere
{
    public class SpherePower : MonoBehaviour
    {
        private PlayerShoogting m_player;

        private void Awake()
        {
            m_player = GetComponent<PlayerShoogting>();
        }

        private void Start()
        {
            Destroy(gameObject, 5f);
        }
    }
}