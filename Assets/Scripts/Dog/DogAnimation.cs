using TOGETHER.Player;
using UnityEngine;

namespace TOGETHER.Assets.Scripts.Dog
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(DogMovement))]

    public class DogAnimation : MonoBehaviour
    {
        #region Fields

        private Animator m_animatorDog;
        private DogMovement m_dogMovement;
        private PlayerMove m_player;

        #endregion

        #region Private Methods

        private void Awake()
        {
            m_animatorDog = GetComponent<Animator>();
            m_dogMovement = GetComponent<DogMovement>();
            m_player = FindAnyObjectByType<PlayerMove>();
        }

        private void OnEnable()
        {
            m_dogMovement.OnDogisMoving += DogMove;
            m_player.OnPlayerIsRunning += Run;
        }

        private void OnDisable()
        {
            m_dogMovement.OnDogisMoving -= DogMove;
            m_player.OnPlayerIsRunning -= Run;
        }

        private void DogMove(bool value)
        {
            if (value)
            {
                m_animatorDog.SetBool("IsIdleing", false);
                m_animatorDog.SetBool("IsWalking", true);
                m_animatorDog.SetBool("IsRunning", false);
            }
            else
            {
                m_animatorDog.SetBool("IsIdleing", true);
                m_animatorDog.SetBool("IsWalking", false);
                m_animatorDog.SetBool("IsRunning", false);
            }
        }

        private void Run(bool value)
        {
            if (value)
            {
                m_animatorDog.SetBool("IsIdleing", false);
                m_animatorDog.SetBool("IsWalking", false);
                m_animatorDog.SetBool("IsRunning", true);
            }
        }

        #endregion

    }    
}