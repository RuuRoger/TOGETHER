using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using UnityEngine.AI;

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

            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent != null && agent.enabled)
            {
                //Now the the navemesh move the player
                agent.SetDestination(destination);
                
                // while calculating or more distance than 0.1
                while (agent.pathPending || agent.remainingDistance > 0.1f)
                {
                    yield return null;
                }
            }

            //Fix the position in last step
            transform.position = destination;

            CellManager.Instance.ResetAllCellsColors();

            m_isMoving = false;
        }
    }
}