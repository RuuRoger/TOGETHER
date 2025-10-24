// using System;
// using TOGETHER.Assets.Scripts.Common;
// using UnityEngine;
// using UnityEngine.AI;

// namespace TOGETHER.Assets.Scripts.Dog
// {
//     [RequireComponent(typeof(NavMeshAgent))]

//     public class DogMovement : MonoBehaviour
//     {
//         #region Serilizable fields

//         [Header("Settings to Walk")]
//         [Space(10)]
//         [SerializeField] private Transform m_idleZone;

//         #endregion

//         #region private fields

//         private AnimationStates m_currentState;
//         private NavMeshAgent m_dogNavMesh;
//         private bool m_isStopped;

//         #endregion

//         #region Events

//         public event Action<AnimationStates> OnAnimationStateChanged;

//         #endregion

//         #region Private Methods

//         private void Awake()
//         {
//             m_dogNavMesh = GetComponent<NavMeshAgent>();
//             m_isStopped = false;
//             m_currentState = AnimationStates.Idle;
//         }

//         private void Start()
//         {
//             m_dogNavMesh.speed = 3.5f;
//         }

//         private void Update()
//         {
//             MoveDog();
//             CheckingDogVelocity();
//         }

//         private void OnEnable()
//         {
//             m_dogNavMesh.stoppingDistance = 4f;
//         }

//         private void MoveDog()
//         {
//             float distance = Vector3.Distance(transform.position, m_idleZone.position);

//             if (distance > m_dogNavMesh.stoppingDistance)
//             {
//                 m_dogNavMesh.isStopped = false;
//                 m_dogNavMesh.SetDestination(m_idleZone.position);
//                 m_isStopped = false;
//             }
//             else
//             {
//                 if (!m_isStopped)
//                 {
//                     m_dogNavMesh.isStopped = true;
//                     m_isStopped = true;
//                 }
//             }
//         }

//         private void CheckingDogVelocity()
//         {
//             AnimationStates newState;

//             if (m_dogNavMesh.velocity.magnitude > 0.05f)
//                 newState = AnimationStates.IsWalking;
//             else
//                 newState = AnimationStates.Idle;

//             if (m_currentState != newState)
//             {
//                 m_currentState = newState;
//                 OnAnimationStateChanged?.Invoke(newState);
//             }
//         }

//         #endregion
//     }
// }