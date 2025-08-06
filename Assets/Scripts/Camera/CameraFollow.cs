using UnityEngine;

namespace TOGETHER.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed;

        private Vector3 _offset;

        private void Awake()
        {
            _offset = transform.position - _target.position;
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
