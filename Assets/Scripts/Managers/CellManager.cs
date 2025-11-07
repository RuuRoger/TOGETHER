using System;
using UnityEngine;
using Assets.Scripts.Players;
using Assets.Scripts.Cells;

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
                m_basPlayer.OnIDCellPlayer -= HandlerAccesibleCells;
            }
        }

        // =================================== PUBLIC METHODS ===================================
        public void SubscribeToBasePlayerEvents(BasePlayer player)
        {

            if (player != null)
            {
                m_basPlayer = player;
                m_basPlayer.OnIDCellPlayer += HandlerAccesibleCells;
            }
        }

        public void ResetAllCellColors()
        {
            ChangeBasePlayerSelection();

            foreach (Cell cell in FindObjectsByType<Cell>(FindObjectsSortMode.None))
            {
                cell.ResetColor();
            }
        }

        // =================================== PRIVATE METHODS ===================================
        private void HandlerAccesibleCells(Vector2 playerIdCell)
        {
            OnStartTurn?.Invoke(playerIdCell);
        }

        private void ChangeBasePlayerSelection()
        {
            m_basPlayer.IsSelected = false;
        }
    }
}