using System;
using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private DogPlayer m_dog;

        // =================================== EVENTS ===================================
        public event Action<Vector2> OnStartTurn; //Goes to 'Cell.cs'

        // =================================== EVENTS SUSCRIPTIONS ===================================

        private void OnEnable()
        {
            if (m_dog != null)
            {
                m_dog.OnDogPosition += HandlerAccesibleCells;
            }
        }

        private void OnDisable()
        {
            if (m_dog != null)
            {
                m_dog.OnDogPosition -= HandlerAccesibleCells;
            }
        }

        // =================================== PRIVATE METHODS ===================================
        private void Awake()
        {
            m_dog = FindFirstObjectByType<DogPlayer>();
        }

        private void HandlerAccesibleCells(Vector2 dogIdCell)
        {
            OnStartTurn?.Invoke(dogIdCell);
        }
    }
}