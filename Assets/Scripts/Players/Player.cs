using UnityEngine;

namespace Assets.Scripts.Players
{
    public class Player : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private Vector3 m_currentPlayerPosition;

        // =================================== PROPERTIES ===================================
        public Vector3 CurrentPlayerPosition
        {
            get
            {
                return m_currentPlayerPosition;
            }
            set
            {

            }
        }

    }
}