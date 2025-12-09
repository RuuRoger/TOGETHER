using UnityEngine;
using Assets.Scripts.Players;
using Assets.Scripts.Cells;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private BasePlayer m_player;

        // ================================================== PROPERTIES ==================================================
        public static PlayerManager Instance {get; set;}

        // ================================================== PUBLIC METHODS ==================================================
        
        ///  <summary>
        /// Assigns the reference of the GameObject with the BasePlayer script, used for the characters.
        /// Its use is only for instantiating the character.
        /// </summary>
        public void GetPlayer()
        {
            if (m_player == null)
            {
                m_player = FindFirstObjectByType<BasePlayer>();
            }
        }

        /// <summary>
        /// Identifies the character by its tag, reads the cell ID and then sends this information to change the color of the accessible cells.
        /// </summary>
        /// <param name="playerTag">Reads the player's tag to identify the player</param>
        public void PlayerSelected(string playerTag)
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            m_player = player.GetComponent<BasePlayer>();

            Vector2 idCell = ReadPlayerIdCell(player);

            CellManager.Instance.NotifyChangeColorAccesibleCells(idCell, player);
        }

        public void NotifyToPlayerCellDestination(Vector3 cellDestinationPosition)
        {
            m_player.MovePlayerToCell(cellDestinationPosition);
            m_player.IsSelected = false;
        }

        // ================================================== PRIVATE METHODS ==================================================
   
        // Singleton Pattern
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