using UnityEngine;
using UnityEngine.AI;
using TOGETHER.Assets.Scripts.Player;
using System;
using TOGETHER.Assets.Scripts.Common;

namespace TOGETHER.Assets.Scripts.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BoxCollider))]

    public class Enemy1 : MonoBehaviour
    {
        private NavMeshAgent m_agentEnemy1;
        private PlayerMove m_player;
        private AnimationStates m_currentState;
        private bool m_isFollow;
        private bool m_isPossibleAttack;
        
        public event Action<AnimationStates> OnWalk;
        public event Action<bool> OnAttack;

        private void Awake()
        {
            m_agentEnemy1 = GetComponent<NavMeshAgent>();
            m_player = FindAnyObjectByType<PlayerMove>();
            m_currentState = AnimationStates.Idle;
            m_isFollow = false;
            m_isPossibleAttack = false;
        }

        private void Update()
        {
            CheckEnemyMove();
            GoToPlayer();
            Attack();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_isFollow = true;
                CheckEnemyMove();
            }
        }

        // Método específico que se llamará desde el collider de detección de Power
        public void OnPowerDetected()
        {
            Destroy(gameObject);
        }

        private void CheckEnemyMove()
        {
            AnimationStates newState;

            if (m_agentEnemy1.velocity.magnitude > 0.05f)
            {
                newState = AnimationStates.IsWalking;
                m_isPossibleAttack = true;

            }
            else
            {
                newState = AnimationStates.Idle;
            }

            if (m_currentState != newState)
            {
                m_currentState = newState;
                OnWalk?.Invoke(newState);
            }
        }

        private void GoToPlayer()
        {
            if (m_isFollow)
            {
                m_agentEnemy1.SetDestination(m_player.transform.position);
            }
        }

        private void Attack()
        {
            float distancePlayer = Vector3.Distance(m_player.transform.position, this.transform.position);

            if (m_isPossibleAttack && distancePlayer <= 1f)
            {
                OnAttack?.Invoke(true);
            }
            else
            {
                OnAttack?.Invoke(false);
            }
        }

    }
}