using UnityEngine;
using UnityEngine.AI;

namespace TOGETHER.Assets.Scripts.Dog
{
    public class DogController : MonoBehaviour
    {
        [SerializeField] private Transform _idleZone;
        private NavMeshAgent _dogNavMesh;
        private Animator _animatorDog;
        [SerializeField] private float _stopDistance;
        private bool _isStopped;

        private void Awake()
        {
            _dogNavMesh = GetComponent<NavMeshAgent>();
            _animatorDog = GetComponent<Animator>();
            _isStopped = false;
        }

        private void Update()
        {
            MoveDog();
        }

        private void MoveDog()
        {
            float distance = Vector3.Distance(transform.position, _idleZone.position);

            if (distance > _stopDistance)
            {
                _dogNavMesh.isStopped = false;
                _dogNavMesh.SetDestination(_idleZone.position);
                _animatorDog.SetBool("IsWalking", true);
                _isStopped = false;
            }
            else
            {
                if (!_isStopped)
                {
                    _dogNavMesh.isStopped = true;
                    _animatorDog.SetBool("IsWalking", false);
                    _isStopped = true;
                }
            }
        }
    
    }
}