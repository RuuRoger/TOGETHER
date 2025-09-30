using UnityEngine;
using TOGETHER.Assets.Scripts.Common;

namespace TOGETHER.Assets.Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        #region Fields

        private AnimationStates m_currentState;
        private Animator m_animator;
        private PlayerMove m_playerController;

        #endregion

        #region Private Methods

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_playerController = GetComponent<PlayerMove>();
        }

        private void OnEnable()
        {
            m_playerController.OnAnimationStateChanged += UpdateAnimation;
            m_playerController.OnShootFireball += ShootFireballAnimation;
        }

        private void OnDisable()
        {
            m_playerController.OnAnimationStateChanged -= UpdateAnimation;
            m_playerController.OnShootFireball -= ShootFireballAnimation;
        }
        private void Start()
        {
            m_currentState = AnimationStates.Idle;
        }

        private void UpdateAnimation(AnimationStates newState)
        {
            if (m_currentState == newState)
                return;

            m_animator.SetTrigger(newState.ToString());
            m_currentState = newState;
        }

        private void ShootFireballAnimation(bool value)
        {
            if (value)
            {
                m_animator.SetBool("Fireball", true);
            }
            else
            {
                m_animator.SetBool("Fireball", false);
            }
        }
    }

    #endregion
}
