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
        private void OnDisable()
        {
            if (m_dog != null)
            {
                m_dog.OnDogPosition -= HandlerAccesibleCells;
            }
        }

        // =================================== PUBLIC METHODS ===================================
        public void SubscribeToDogEvents(DogPlayer dog)
        {
            if (dog != null)
            {
                m_dog = dog;
                m_dog.OnDogPosition += HandlerAccesibleCells;
            }
        }

        // =================================== PRIVATE METHODS ===================================

        private void HandlerAccesibleCells(Vector2 dogIdCell)
        {
            OnStartTurn?.Invoke(dogIdCell);
        }
    }
}