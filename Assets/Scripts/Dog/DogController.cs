using UnityEngine;
using UnityEngine.AI;

namespace TOGETHER.Assets.Scripts.Dog
{
    public class DogController : MonoBehaviour
    {
        #region Serilizable fields

        [Header("Settings to Walk")]
        [Space(10)]
        [SerializeField] private float _stopDistance;
        [Space]
        [SerializeField] private Transform _idleZone;

        #endregion

        #region private fields

        private NavMeshAgent _dogNavMesh;
        private Animator _animatorDog;
        private bool _isStopped;

        #endregion

        #region Unity Methods

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

        #endregion

        #region  Private Methods

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
                    _isStopped = true;
                }
            }

            _animatorDog.SetBool("IsWalking", _dogNavMesh.velocity.magnitude > 0.05f);
        }
    
        #endregion
    }
}