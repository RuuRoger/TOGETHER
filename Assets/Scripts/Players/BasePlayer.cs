using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Unity.VisualScripting;

namespace Assets.Scripts.Players
{
    public class BasePlayer : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private bool m_isSelected = false;
        private bool m_isMoving = false;

        // ================================================== PROPERTIES ==================================================
        public bool IsSelected
        {
            get { return m_isSelected; }
            set { m_isSelected = value; }
        }

        // ================================================== PUBLIC METHODS ==================================================
        public void MovePlayerToCell(Vector3 cellDestination)
        {
            if (m_isSelected && !m_isMoving)
            {
                StartCoroutine(MoveToCell(cellDestination));
            }
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void OnMouseDown()
        {
            m_isSelected = true;
            string playerTag = gameObject.tag;
            PlayerManager.Instance.PlayerSelected(playerTag);
        }

        private IEnumerator MoveToCell(Vector3 destination)
        {
            m_isMoving = true;

            while (Vector3.Distance(transform.position, destination) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, 3f * Time.deltaTime);
                yield return null;
            }

            //To Make sure the player is in cell position
            transform.position = destination;

            CellManager.Instance.ResetAllCellsColors();

            m_isMoving = false;

        }
    }
}