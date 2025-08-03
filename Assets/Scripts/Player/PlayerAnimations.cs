using UnityEngine;

namespace TOGETHER.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;
        private PlayerController _playerController;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            _playerController.OnPlayerIsMoving += UpdateAnimation;
        }

        private void OnDisable()
        {
            _playerController.OnPlayerIsMoving -= UpdateAnimation;
        }

        private void UpdateAnimation(float actionValue)
        {
            if (actionValue > 0.1f)
                _animator.SetFloat("Walk", actionValue);
            else
                _animator.SetFloat("Walk", 0f);
        }
    }
}

