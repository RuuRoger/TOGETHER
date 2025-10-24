using UnityEngine;
using TOGETHER.Assets.Scripts.Common;
using Unity.AI.Navigation.Samples;

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
        private ClickToMove m_dogMovingWithClick;
        

        #endregion

        #region Private Methods

        private void Awake()
        {
            m_animatorDog = GetComponent<Animator>();
            m_dogMovement = GetComponent<DogMovement>();
            m_dogMovingWithClick = GetComponent<ClickToMove>();
            m_currentState = AnimationStates.Idle;
        }

        private void OnEnable()
        {
            m_dogMovement.OnAnimationStateChanged += UpdateDogAnimation;
            m_dogMovingWithClick.OnDogMoveWithClick += UpdateDogAnimation;

        }

        private void OnDisable()
        {
            m_dogMovement.OnAnimationStateChanged -= UpdateDogAnimation;
            m_dogMovingWithClick.OnDogMoveWithClick -= UpdateDogAnimation;

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