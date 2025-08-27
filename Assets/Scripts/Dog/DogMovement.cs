using System;
using UnityEngine;
using UnityEngine.AI;

namespace TOGETHER.Assets.Scripts.Dog
{
    [RequireComponent(typeof(NavMeshAgent))]

    public class DogMovement : MonoBehaviour
    {
        #region Serilizable fields

        [Header("Settings to Walk")]
        [Space(10)]
        [SerializeField] private Transform m_idleZone;

        #endregion

        #region private fields

        private NavMeshAgent m_dogNavMesh;
        private bool m_isStopped;
        private bool m_isMoving;

        #endregion

        #region Events

        public event Action<bool> OnDogisMoving;

        #endregion

        #region Private Methods

        private void Awake()
        {
            m_dogNavMesh = GetComponent<NavMeshAgent>();
            m_isStopped = false;
            m_isMoving = false;
        }

        private void Update()
        {
            MoveDog();
            CheckingDogVelocity();
        }

        private void MoveDog()
        {
            float distance = Vector3.Distance(transform.position, m_idleZone.position);

            if (distance > m_dogNavMesh.stoppingDistance)
            {
                m_dogNavMesh.isStopped = false;
                m_dogNavMesh.SetDestination(m_idleZone.position);
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
        }

        private void CheckingDogVelocity()
        {
            if (m_dogNavMesh.velocity.magnitude > 0.05f)
            {
                m_isMoving = true;
                OnDogisMoving?.Invoke(m_isMoving);
            }
            else
            {
                m_isMoving = false;
                OnDogisMoving?.Invoke(m_isMoving);
            }
        }

        #endregion
    }
}