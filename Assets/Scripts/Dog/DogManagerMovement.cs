using System;
using Unity.AI.Navigation.Samples;
using UnityEngine;

namespace TOGETHER.Assets.Scripts.Dog
{
    public class DogManagerMovement : MonoBehaviour
    {
        //---------- Fields ----------
        private DogMovement m_movement;
        private ClickToMove m_clickToMove;
        private bool m_moveWitchClick;

        private void Awake()
        {
            m_movement = GetComponent<DogMovement>();
            m_clickToMove = GetComponent<ClickToMove>();
        }

        private void Start()
        {
            m_movement.enabled = true;
            m_clickToMove.enabled = false;
            m_moveWitchClick = false;
        }

        private void Update()
        {
            HandleMoveState();
            ChangeMoveState();
        }

        private void HandleMoveState()
        {
            if (!m_moveWitchClick)
            {
                m_movement.enabled = true;
                m_clickToMove.enabled = false;
            }
            else
            {
                m_movement.enabled = false;
                m_clickToMove.enabled = true;
            }
        }

        private void ChangeMoveState()
        {
            if (Input.GetMouseButtonDown(1))
            {
                m_moveWitchClick = !m_moveWitchClick;
            }
        }

    }
}