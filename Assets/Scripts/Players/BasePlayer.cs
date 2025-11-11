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

        // ================================================== PRIVATE METHODS ==================================================
        private void OnMouseDown()
        {
            m_isSelected = true;
            string playerTag = gameObject.tag;
            PlayerManager.Instance.PlayerSelected(playerTag);
        }
    }
}