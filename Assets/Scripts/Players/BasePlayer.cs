using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Cells;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Players
{
    public class BasePlayer : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private bool m_isSelected = false;

        // ================================================== PROPERTIES ==================================================
        public bool IsSelected
        {
            get { return m_isSelected; }
            set { m_isSelected = value; }
        }

        // ================================================== PUBLIC METHODS ==================================================
        public void MovePlayerToCell(Vector3 cellDestination)
        {
            if (m_isSelected)
            {
                transform.position = Vector3.MoveTowards(transform.position, cellDestination, 3f * Time.deltaTime);
            }
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void OnMouseDown()
        {
            m_isSelected = true;
            string playerTag = gameObject.tag;
            PlayerManager.Instance.PlayerSelected(playerTag);
        }
    }
}