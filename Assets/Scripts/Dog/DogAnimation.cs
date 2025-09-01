using UnityEngine;
using TOGETHER.Assets.Scripts.Player;
using TOGETHER.Assets.Scripts.Common;

namespace TOGETHER.Assets.Scripts.Dog
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(DogMovement))]

    public class DogAnimation : MonoBehaviour
    {
        #region Fields

        private AnimationStates m_currentState;
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
            m_currentState = AnimationStates.Idle;
        }

        private void OnEnable()
        {
            m_dogMovement.OnAnimationStateChanged += UpdateDogAnimation;
        }

        private void OnDisable()
        {
            m_dogMovement.OnAnimationStateChanged -= UpdateDogAnimation;
        }

        private void UpdateDogAnimation(AnimationStates newState)
        {
            if (m_currentState == newState)
                return;

            m_animatorDog.SetTrigger(newState.ToString());
            m_currentState = newState;
        }

        #endregion

    }    
}