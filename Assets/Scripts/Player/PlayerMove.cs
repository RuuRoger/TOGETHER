using System;
using UnityEngine;

namespace TOGETHER.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]

    public class PlayerMove : MonoBehaviour
    {
        #region Inspector Fields

        [Header("Speed settings")]
        [Space(10)]
        [SerializeField] private float _speedPlayerMovement;
        [Space]
        [SerializeField] private float _runIncrement;
        [SerializeField] private float _rotationSpeed;
        #endregion

        #region Fields

        private Rigidbody _rigidbodyPlayer;
        private bool _isRunning;
        private float _currentSpeed;
        #endregion

        #region Events

        public event Action<float> OnPlayerIsMoving;
        public event Action<bool> OnPlayerIsRunning;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _rigidbodyPlayer = GetComponent<Rigidbody>();
            _currentSpeed = _speedPlayerMovement;
        }

        private void Update()
        {
            RunningControl();
            Movement();
        }

        #endregion

        #region Private Methods

        private void Movement()
        {
            //Values for the method
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            Vector3 inputMovement = new Vector3(inputX, 0, inputZ);
            Vector3 playerMovement = inputMovement.normalized * _currentSpeed;

            //The movmement of player here
            _rigidbodyPlayer.linearVelocity = new Vector3(playerMovement.x, _rigidbodyPlayer.linearVelocity.y, playerMovement.z);

            if (inputMovement.magnitude > 0.1f)
            {
                OnPlayerIsMoving?.Invoke(inputMovement.magnitude);

                //This is for rotation!
                Vector3 lookDirection = new Vector3(inputX, 0, inputZ);
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            }
            else
                OnPlayerIsMoving?.Invoke(inputMovement.magnitude);
        }

        private void RunningControl()
        {
            _isRunning = Input.GetKey(KeyCode.Space);

            _currentSpeed = _isRunning ? _speedPlayerMovement * _runIncrement : _speedPlayerMovement;
            OnPlayerIsRunning?.Invoke(_isRunning);
        }

        #endregion
    }
}