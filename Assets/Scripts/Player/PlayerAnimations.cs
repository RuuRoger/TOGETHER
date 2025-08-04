using UnityEngine;

namespace TOGETHER.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        #region Fields

        private Animator _animator;
        private PlayerController _playerController;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            _playerController.OnPlayerIsMoving += UpdateAnimation;
            _playerController.OnPlayerIsRunning += UpdateRunningAnimation;
        }

        private void OnDisable()
        {
            _playerController.OnPlayerIsMoving -= UpdateAnimation;
            _playerController.OnPlayerIsRunning -= UpdateRunningAnimation;
        }

        #endregion

        #region Private Methods

        private void UpdateAnimation(float actionValue)
        {
            if (actionValue > 0.1f)
                _animator.SetFloat("Walk", actionValue);
            else
                _animator.SetFloat("Walk", 0f);
        }

        private void UpdateRunningAnimation(bool isRunning)
        {
            _animator.SetBool("Run", isRunning);
        }


        #endregion
    }
}

