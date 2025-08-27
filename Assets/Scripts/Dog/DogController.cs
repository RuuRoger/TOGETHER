using UnityEngine;
using UnityEngine.AI;

namespace TOGETHER.Assets.Scripts.Dog
{
    public class DogController : MonoBehaviour
    {
        #region Serilizable fields

        [Header("Settings to Walk")]
        [Space(10)]
        [SerializeField] private Transform m_idleZone;

        #endregion

        #region private fields

        private NavMeshAgent m_dogNavMesh;
        private Animator m_animatorDog;
        private bool m_isStopped;

        #endregion


        #region Unity Methods

        private void Awake()
        {
            m_dogNavMesh = GetComponent<NavMeshAgent>();
            m_animatorDog = GetComponent<Animator>();
            m_isStopped = false;
        }

        private void Update()
        {
            MoveDog();
        }

        #endregion

        #region  Private Methods

        private void MoveDog()
        {
            float distance = Vector3.Distance(transform.position, m_idleZone.position);

            if (distance > m_dogNavMesh.stoppingDistance)
            {
                m_dogNavMesh.isStopped = false;
                m_dogNavMesh.SetDestination(m_idleZone.position);
                m_animatorDog.SetBool("IsWalking", true);
                m_isStopped = false;
            }
            else
            {
                if (!m_isStopped)
                {
                    m_dogNavMesh.isStopped = true;
                    m_isStopped = true;
                }
            }

            m_animatorDog.SetBool("IsWalking", m_dogNavMesh.velocity.magnitude > 0.05f);
        }
    
        #endregion
    }
}