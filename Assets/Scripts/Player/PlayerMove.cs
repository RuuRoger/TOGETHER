using System;
using UnityEngine;
using TOGETHER.Assets.Scripts.Common;

namespace TOGETHER.Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]

    public class PlayerMove : MonoBehaviour
    {
        #region Inspector Fields

        [Header("Speed settings")]
        [Space(10)]
        [SerializeField] private float m_speedPlayerMovement;
        [Space]
        [SerializeField] private float m_runIncrement;
        [SerializeField] private float m_rotationSpeed;

        #endregion

        #region Fields

        private AnimationStates m_currentState;
        private Rigidbody m_rigidbodyPlayer;
        private bool m_isRunning;
        private float m_currentSpeed;

        #endregion

        #region Events

        public event Action<AnimationStates> OnAnimationStateChanged;

        #endregion

        #region Private Methods

        private void Awake()
        {
            m_rigidbodyPlayer = GetComponent<Rigidbody>();
            m_currentSpeed = m_speedPlayerMovement;
            m_currentState = AnimationStates.Idle;
        }

        private void Update()
        {
            RunningControl();
            Movement();
        }

        private void RunningControl()
        {
            m_isRunning = Input.GetKey(KeyCode.Space);
            m_currentSpeed = m_isRunning ? m_speedPlayerMovement * m_runIncrement : m_speedPlayerMovement;
        }

        private void Movement()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            Vector3 inputMovement = new Vector3(inputX, 0, inputZ);
            Vector3 playerMovement = inputMovement.normalized * m_currentSpeed;

            m_rigidbodyPlayer.linearVelocity = new Vector3(playerMovement.x, m_rigidbodyPlayer.linearVelocity.y, playerMovement.z);

            AnimationStates newState;

            if (inputMovement.magnitude > 0.1f)
            {
                newState = m_isRunning ? AnimationStates.IsRunning : AnimationStates.IsWalking;

                // Player's rotation
                Vector3 lookDirection = new Vector3(inputX, 0, inputZ);
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * m_rotationSpeed);
            }
            else
            {
                newState = AnimationStates.Idle;
            }

            if (m_currentState != newState)
            {
                m_currentState = newState;
                OnAnimationStateChanged?.Invoke(newState);
            }
        }
    }

    #endregion

}