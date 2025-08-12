using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination
    /// </summary>

    //! Remember: "m_" is like "_", it's a private field

    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        #region Private Fields

        NavMeshAgent m_Agent;
        RaycastHit m_HitInfo = new RaycastHit();
        private Animator _animatorDog;

        #endregion

        #region Unity Callbacks

        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            _animatorDog = GetComponent<Animator>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                    m_Agent.destination = m_HitInfo.point;
            }

            WalkAnimation();
        }

        private void WalkAnimation()
        {
            if (m_Agent.velocity.magnitude > 0.1f)
                _animatorDog.SetBool("IsWalking", true);
            else
                _animatorDog.SetBool("IsWalking", false);
        }
        
        #endregion
    }
}