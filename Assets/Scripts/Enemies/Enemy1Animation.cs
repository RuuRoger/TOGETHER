// using UnityEngine;
// using TOGETHER.Assets.Scripts.Common;

// namespace TOGETHER.Assets.Scripts.Enemies
// {
//     public class Enemy1Animation : MonoBehaviour
//     {
//         private Animator m_enemy1Animator;
//         private AnimationStates m_currentState;
//         private Enemy1 m_enemy1;

//         private void Awake()
//         {
//             m_enemy1Animator = GetComponent<Animator>();
//             m_currentState = AnimationStates.Idle;
//             m_enemy1 = GetComponent<Enemy1>();
//         }

//         private void OnEnable()
//         {
//             m_enemy1.OnWalk += UpdateAnimation;
//             m_enemy1.OnAttack += Attack;
//         }

//         private void OnDisable()
//         {
//             m_enemy1.OnWalk -= UpdateAnimation;
//             m_enemy1.OnAttack -= Attack;
//         }

//         private void UpdateAnimation(AnimationStates newState)
//         {
//             Debug.Log($"UpdateAnimation llamado con estado: {newState}");

//             if (m_currentState == newState)
//             {
//                 Debug.Log("Estado no cambió, saliendo...");
//                 return;
//             }

//             Debug.Log($"Activando trigger: {newState.ToString()}");
//             m_enemy1Animator.SetTrigger(newState.ToString());
//             m_currentState = newState;
//         }
        
//         private void Attack(bool value)
//         {
//             if (value)
//             {
//                 m_enemy1Animator.SetBool("Attack", true);
//             }
//             else
//             {
//                 m_enemy1Animator.SetBool("Attack", false);
//             }
//         }
//     }
// }