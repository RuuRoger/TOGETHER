using UnityEngine;
using TOGETHER.Assets.Scripts.Player;

namespace TOGETHER.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Camera Follow Settings")]
        [Space(10)]
        [SerializeField] private Transform m_target;
        [Space]
        [SerializeField] private float m_smoothSpeed;
        [Space]
        [SerializeField] private float m_anticipacion;

        #endregion

        #region Private Fields

        private Vector3 m_offset;
        private PlayerMove m_player;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // m_offset = transform.position - m_target.position;
            m_offset = new Vector3(0, 15, 0);
            m_player = m_target.GetComponent<PlayerMove>();
        }

        private void OnEnable()
        {
            m_player.OnPlayerInputMove += HandleCameraAndInputPlayer;
        }

        private void OnDisable()
        {
            m_player.OnPlayerInputMove -= HandleCameraAndInputPlayer;
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = m_target.position + m_offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, m_smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

        #endregion

        #region Private Methods

        private void HandleCameraAndInputPlayer(Vector3 moveDirection)
        {
            Vector3 baseOffset = new Vector3(0f, 15f, 0f);

            if (moveDirection.magnitude > 0.1f)
                m_offset = baseOffset + moveDirection.normalized * m_anticipacion;
            else
                m_offset = baseOffset;

        }


        #endregion
    }
}
