using System;
using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private BasePlayer m_basPlayer;

        // =================================== EVENTS ===================================
        public event Action<Vector2> OnStartTurn; //Goes to 'Cell.cs'

        // =================================== EVENTS SUSCRIPTIONS ===================================
        private void OnDisable()
        {
            if (m_basPlayer != null)
            {
                m_basPlayer.OnPlayerPosition -= HandlerAccesibleCells;
            }
        }

        // =================================== PUBLIC METHODS ===================================
        public void SubscribeToDogEvents(BasePlayer player)
        {
            if (player != null)
            {
                m_basPlayer = player;
                m_basPlayer.OnPlayerPosition += HandlerAccesibleCells;
            }
        }

        // =================================== PRIVATE METHODS ===================================

        private void HandlerAccesibleCells(Vector2 playerIdCell)
        {
            OnStartTurn?.Invoke(playerIdCell);
        }
    }
}