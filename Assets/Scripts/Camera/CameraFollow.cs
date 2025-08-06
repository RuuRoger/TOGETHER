using UnityEngine;

namespace TOGETHER.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Camera Follow Settings")]
        [Space(10)]
        [SerializeField] private Transform _target;
        [Space]
        [SerializeField] private float _smoothSpeed;

        private Vector3 _offset;

        #endregion

        #region Unity Methods

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

        #endregion
    }
}
