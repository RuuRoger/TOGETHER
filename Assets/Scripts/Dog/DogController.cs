using System.Collections;
using TOGETHER.Assets.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace TOGETHER.Assets.Scripts.Dog
{
    public class DogController : MonoBehaviour
    {
        [SerializeField] private Collider _idleZone;
        [SerializeField] private DogInCollider _colliderToMove;
        private NavMeshAgent _dogNavMesh;
        private Animator _animatorDog;
        private bool _isInside;

        private void Awake()
        {
            _dogNavMesh = GetComponent<NavMeshAgent>();
            _animatorDog = GetComponent<Animator>();
            _isInside = false;
        }

        private void Start()
        {
            SetInside(false);
        }

        private void OnEnable()
        {
            _colliderToMove.OnInsideZone += OnDogEnterZone;
            _colliderToMove.OnOutZone += OnDogExitZone;
        }

        private void OnDisable()
        {
            _colliderToMove.OnInsideZone -= OnDogEnterZone;
            _colliderToMove.OnOutZone -= OnDogExitZone;
        }

        private void OnDogEnterZone() => SetInside(true);
        private void OnDogExitZone() => SetInside(false);

        public void SetInside(bool inside)
        {
            _isInside = inside;
            MoveToIdleZone();
        }

        private void MoveToIdleZone()
        {
            if (!_isInside)
                StartCoroutine(StartMovingWithDelay());
            else
                StartCoroutine(MakeDelayInMovment());
        }

        private IEnumerator StartMovingWithDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _dogNavMesh.SetDestination(_idleZone.transform.position);
            _animatorDog.SetBool("IsWalking", true);
        }

        private IEnumerator MakeDelayInMovment()
        {
            yield return new WaitForSeconds(0.2f);
            _dogNavMesh.ResetPath();
            _animatorDog.SetBool("IsWalking", false);
        }
    
    }
}