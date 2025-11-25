using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Players;
using Assets.Scripts.Cells;
using UnityEngine.AI;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private static PlayerManager m_instance;
        private BasePlayer m_player;

        // ================================================== PROPERTIES ==================================================
        public static PlayerManager Instance
        {
            get { return m_instance; }
            set { m_instance = value; }
        }

        // ================================================== PUBLIC METHODS ==================================================
        public void GetPlayer()
        {
            if (m_player == null)
            {
                m_player = FindFirstObjectByType<BasePlayer>();
            }
        }

        public void PlayerSelected(string playerTag)
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            m_player = player.GetComponent<BasePlayer>();

            Vector2 idCell = ReadPlayerIdCell(player);

            // diseable agent
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }

            CellManager.Instance.NotifyChangeColorAccesibleCells(idCell);
            
            StartCoroutine(EnableAgentAfterBuild(player));
        }

        private IEnumerator EnableAgentAfterBuild(GameObject player)
        {
            yield return null;
            
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null && !agent.enabled)
            {
                agent.enabled = true;
            }
        }

        public void NotifyToPlayerCellDestination(Vector3 cellDestinationPosition)
        {
            m_player.MovePlayerToCell(cellDestinationPosition);
            m_player.IsSelected = false;
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private Vector2 ReadPlayerIdCell(GameObject player)
        {
            /*
            ¡¡¡IMPORTANT!!!
            It's very important add "Vector3.up * 0.5f" because made probles and bugs without this.
            This is because if dog coudn't works correctly the raycast, the game turns crazy with the movement
            */
            Ray ray = new(player.transform.position + Vector3.up * 0.5f, Vector3.down);
            RaycastHit hit;

            Vector2 idCell = Vector2.zero; //Default Value

            if (Physics.Raycast(ray, out hit, 5f))
            {
                Cell cell = hit.collider.GetComponent<Cell>();
                if (cell != null)
                {
                    idCell = cell.IDCell;
                }
            }

            return idCell;
        }
    }
}