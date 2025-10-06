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

        public event Action<AnimationStates> OnWalk;

        private void Awake()
        {
            m_agentEnemy1 = GetComponent<NavMeshAgent>();
            m_player = FindAnyObjectByType<PlayerMove>();
            m_currentState = AnimationStates.Idle;
            m_isFollow = false;
        }

        private void Update()
        {
            CheckEnemyMove();
            GoToPlayer();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_isFollow = true;
                CheckEnemyMove();
            }
        }

        private void CheckEnemyMove()
        {
            AnimationStates newState;

            if (m_agentEnemy1.velocity.magnitude > 0.05f)
            {
                newState = AnimationStates.IsWalking;
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
    }
}