using System;
using UnityEngine;
using UnityEngine.UIElements;


namespace TOGETHER.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]

    public class PlayerController : MonoBehaviour
    {
        [Header("Speed settings")]
        [Space(10)]
        [SerializeField] private float _speedPlayerMovement;
        [SerializeField] private float _rotationSpeed;

        private Rigidbody _rigidbodyPlayer;

        public event Action<float> OnPlayerIsMoving;

        private void Awake()
        {
            _rigidbodyPlayer = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            //Values for the method
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            Vector3 inputMovement = new Vector3(inputX, 0, inputZ);
            Vector3 playerMovement = inputMovement.normalized * _speedPlayerMovement;

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

    }
}