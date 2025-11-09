using System;
using UnityEngine;
using Assets.Scripts.Players;
using Assets.Scripts.Cells;

namespace Assets.Scripts.Managers
{
    public class CellManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private BasePlayer m_basPlayer;

        // ================================================== EVENTS ==================================================
        public event Action<Vector2> OnStartTurn; //Goes to 'Cell.cs'

        // ================================================== EVENT SUSCRIPTIONS ==================================================

        ///<Summary>
        /// Unsuscribe if is necessary
        ///</Summary
        private void OnDisable()
        {
            if (m_basPlayer != null)
            {
                m_basPlayer.OnIDCellPlayer -= HandlerAccesibleCells;
            }
        }

        // ================================================== PUBLIC METHODS ==================================================

        ///<Summary>
        /// Suscribe the event checking if is possible
        ///</Summary
        public void SubscribeToBasePlayerEvents(BasePlayer player)
        {

            if (player != null)
            {
                m_basPlayer = player;
                m_basPlayer.OnIDCellPlayer += HandlerAccesibleCells;
            }
        }

        ///<Summary>
        /// Call a method
        /// Accesible public method to resstart the cell's colours
        /// This happens when the player touched some cell to cancell character selection
        ///</Summary
        public void ResetStatus()
        {
            ChangeBasePlayerSelection();

            foreach (Cell cell in FindObjectsByType<Cell>(FindObjectsSortMode.None))
            {
                cell.ResetColor();
            }
        }

        // ================================================== PRIVATE METHODS ==================================================

        ///<Summary>
        /// Notify to Calculate if a cell is accesible or not
        ///</Summary
        private void HandlerAccesibleCells(Vector2 playerIdCell)
        {
            OnStartTurn?.Invoke(playerIdCell);
        }

        ///<Summary>
        /// Change the bool BasePlayer.cs propertie to false
        ///</Summary
        private void ChangeBasePlayerSelection()
        {
            m_basPlayer.IsSelected = false;
        }
    }
}