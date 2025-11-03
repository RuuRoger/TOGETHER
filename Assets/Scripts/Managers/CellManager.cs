using System;
using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private DogPlayer m_dog;

        // =================================== EVENTS SUSCRIPTIONS ===================================

        private void OnEnable()
        {
            m_dog.OnDogPosition += HandlerAccesibleCells;
        }

        private void OnDisable()
        {
            m_dog.OnDogPosition -= HandlerAccesibleCells;
        }

        // =================================== PRIVATE METHODS ===================================
        private void HandlerAccesibleCells(Vector2 dogPosition)
        {
            //WIP
        }
    }
}