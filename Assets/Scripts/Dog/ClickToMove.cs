using UnityEngine;
using UnityEngine.AI;
using TOGETHER.Assets.Scripts.Common;
using System;

namespace Unity.AI.Navigation.Samples
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination
    /// </summary>

    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        #region Private Fields

        NavMeshAgent m_Agent;
        RaycastHit m_HitInfo = new RaycastHit();
        private Animator m_animatorDog;
        private AnimationStates m_currentState;

        #endregion

        #region Events

        public event Action<AnimationStates> OnDogMoveWithClick;

        #endregion

        #region Unity Callbacks

        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            m_animatorDog = GetComponent<Animator>();
            m_currentState = AnimationStates.Idle;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                {
                    m_Agent.speed = 8f;
                    m_Agent.angularSpeed = 120f;
                    m_Agent.destination = m_HitInfo.point;
                }
            }

            WalkAnimation();
        }

        private void WalkAnimation()
        {
            AnimationStates newState;

            if (m_Agent.velocity.magnitude > 0.05f)
                newState = AnimationStates.IsWalking;
            else
                newState = AnimationStates.Idle;

            if (m_currentState != newState)
            {
                m_currentState = newState;
                OnDogMoveWithClick?.Invoke(newState);
            }
        }
        
        #endregion
    }
}