// using System;
// using UnityEngine;
// using TOGETHER.Assets.Scripts.Common;

// namespace TOGETHER.Assets.Scripts.Player
// {
//     [RequireComponent(typeof(Rigidbody))]
//     [RequireComponent(typeof(Animator))]
//     [RequireComponent(typeof(BoxCollider))]

//     public class PlayerMove : MonoBehaviour
//     {
//         #region Inspector Fields

//         [Header("Speed settings")]
//         [Space(10)]
//         [SerializeField] private float m_speedPlayerMovement;
//         [Space]
//         [SerializeField] private float m_rotationSpeed;
//         [Space]
//         [Header("Powers")]
//         [Space(10)]

//         #endregion

//         #region Fields

//         private AnimationStates m_currentState;
//         private Rigidbody m_rigidbodyPlayer;
//         private float m_currentSpeed;
//         private bool m_isShooting;
//         private PlayerManager m_playerPowers;

//         #endregion

//         public bool isShooting
//         {
//             get
//             {
//                 return m_isShooting;
//             }
//         }

//         #region Events

//         public event Action<AnimationStates> OnAnimationStateChanged;
//         public event Action<Vector3> OnPlayerInputMove;
//         public event Action<bool> OnShootFireball;

//         #endregion

//         #region Private Methods

//         private Animator m_animatorPlayer;

//         private void Awake()
//         {
//             m_animatorPlayer = GetComponent<Animator>();
//             m_rigidbodyPlayer = GetComponent<Rigidbody>();
//             m_currentSpeed = m_speedPlayerMovement;
//             m_currentState = AnimationStates.Idle;
//             m_isShooting = false;
//             m_playerPowers = GetComponent<PlayerManager>();

//         }
//             bool isRunning = false;


//         private void Update()
//         {
//             Movement();

//             if (m_rigidbodyPlayer.linearVelocity.magnitude > 0.001f)
//             {

//                 isRunning = true;
//             }
//             else
//             {
//                 isRunning = false;
//             }

//             m_animatorPlayer.SetBool("isRunning", isRunning);

//         }

//         private void Movement()
//         {
//             float inputX = Input.GetAxis("Horizontal");
//             float inputZ = Input.GetAxis("Vertical");

//             // Detectar si se mantiene la barra espaciadora
//             bool holdingSpace = Input.GetKey(KeyCode.Space);

//             // Lanzar el evento solo al presionar o soltar la barra espaciadora
//             if (Input.GetKeyDown(KeyCode.Space))
//             {
//                 m_isShooting = true;
//                 OnShootFireball?.Invoke(m_isShooting);
//             }
//             else if (Input.GetKeyUp(KeyCode.Space))
//             {
//                 m_isShooting = false;
//                 OnShootFireball?.Invoke(m_isShooting);
//             }

//             // Manejar activación/desactivación de poderes
//             if (holdingSpace)
//             {
//                 // Activa solo el poder seleccionado, desactiva los demás
//                 for (int i = 0; i < m_playerPowers.PlayerPowers.Length; i++)
//                 {
//                     m_playerPowers.PlayerPowers[i].PowerObject.SetActive(m_playerPowers.PlayerPowers[i].PowerObject == m_playerPowers.PowerSelected);
//                 }
//             }
//             else
//             {
//                 // Si no se mantiene espacio, desactiva todos los poderes
//                 for (int i = 0; i < m_playerPowers.PlayerPowers.Length; i++)
//                 {
//                     m_playerPowers.PlayerPowers[i].PowerObject.SetActive(false);
//                 }
//             }

//             // Si se mantiene la barra espaciadora, detener movimiento y solo rotar
//             if (holdingSpace)
//             {
//                 // Detener completamente el movimiento
//                 m_rigidbodyPlayer.linearVelocity = new Vector3(0, m_rigidbodyPlayer.linearVelocity.y, 0);

//                 // Solo rotar si hay input horizontal
//                 if (Mathf.Abs(inputX) > 0.1f)
//                 {
//                     transform.Rotate(Vector3.up, inputX * 90f * Time.deltaTime);
//                 }

//                 // Mantener estado idle
//                 if (m_currentState != AnimationStates.Idle)
//                 {
//                     m_currentState = AnimationStates.Idle;
//                     OnAnimationStateChanged?.Invoke(m_currentState);
//                 }

//                 OnPlayerInputMove?.Invoke(Vector3.zero);
//                 return;
//             }

//             // Movimiento normal cuando no se mantiene la barra espaciadora
//             Vector3 inputMovement = new Vector3(inputX, 0, inputZ);
//             Vector3 playerMovement = inputMovement.normalized * m_currentSpeed;

//             m_rigidbodyPlayer.linearVelocity = new Vector3(playerMovement.x, m_rigidbodyPlayer.linearVelocity.y, playerMovement.z);

//             AnimationStates newState;

//             if (inputMovement.magnitude > 0.01f)
//             {
                
//                 newState = AnimationStates.IsWalking;

//                 // Player's rotation
//                 Vector3 lookDirection = new Vector3(inputX, 0, inputZ);
//                 Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
//                 transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * m_rotationSpeed);
//             }
//             else
//             {
//                 newState = AnimationStates.Idle;
//             }

//             if (m_currentState != newState)
//             {
//                 m_currentState = newState;
//                 OnAnimationStateChanged?.Invoke(newState);
//             }

//             OnPlayerInputMove?.Invoke(inputMovement);
//         }
//     }

//     #endregion

// }