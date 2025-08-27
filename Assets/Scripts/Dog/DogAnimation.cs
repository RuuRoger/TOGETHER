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

        #endregion

        #region Private Methods

        private void Awake()
        {
            m_animatorDog = GetComponent<Animator>();
            m_dogMovement = GetComponent<DogMovement>();
        }

        private void OnEnable()
        {
            m_dogMovement.OnDogisMoving += DogMove;
        }

        private void OnDisable()
        {
            m_dogMovement.OnDogisMoving -= DogMove;
        }

        private void DogMove(bool value)
        {
            if (value)
                m_animatorDog.SetBool("IsWalking", true);
            else
                m_animatorDog.SetBool("IsWalking", false);
        }

        #endregion

    }    
}