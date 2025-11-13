using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Players;
using Assets.Scripts.Cells;

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

            Vector2 idCell = ReadPlayerIdCell(player);

            CellManager.Instance.NotifyChangeColorAccesibleCells(idCell);
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
            Ray ray = new(player.transform.position, Vector3.down);
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