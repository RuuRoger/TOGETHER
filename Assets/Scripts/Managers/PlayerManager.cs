using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Players;
using Assets.Scripts.Cells;
using Assets.Scripts.Collectables;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        [SerializeField] private GameObject m_spherePrefabTogether;
        [SerializeField] private GameObject m_spherePrefabDog;
        [SerializeField] private GameObject m_spherePrefabHuman;
        private BasePlayer m_player;

        // ================================================== PROPERTIES ==================================================
        public static PlayerManager Instance {get; set;}
        public int PlayerLives {get; set;}
        public int DogPlayerLives {get;set;}
    

        // ================================================== EVENTS ==================================================
        public static event Action<string, int> OnPoints;
        
        private void OnEnable()
        {
            Collectable1.OnCollected += InstantiateOneCollectible;
        }

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

        private void Start()
        {
            PlayerLives = 0;
            DogPlayerLives = 0;
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

        private void InstantiateOneCollectible(string colletableTag, string playerTag, bool isCorrect)
        {
            StartCoroutine(InstantiateOneCollectibleCoroutine(colletableTag, playerTag, isCorrect));
        }

        private IEnumerator InstantiateOneCollectibleCoroutine(string colletableTag, string playerTag, bool isCorrect)
        {
            if (isCorrect)
            {
                if (playerTag == "DogPlayer")
                {
                    DogPlayerLives ++;
                    OnPoints?.Invoke(playerTag, DogPlayerLives);
                }

                if (playerTag == "Player")
                {
                    PlayerLives ++;
                    OnPoints?.Invoke(playerTag, PlayerLives);
                }
            }
            else
            {
                if (playerTag == "DogPlayer")
                {
                    DogPlayerLives --;
                    OnPoints?.Invoke(playerTag, DogPlayerLives);
                }

                if (playerTag == "Player")
                {
                    PlayerLives --;
                    OnPoints?.Invoke(playerTag, PlayerLives);
                }
            }

            yield return new WaitForSeconds(1f);

            // Collectables
            List<Cell> emptyCells = CellManager.Instance.GetCellsWithOutObstacles();

            if (colletableTag == "Together Points")
            {
                int randomIndex1 = UnityEngine.Random.Range(0, emptyCells.Count);
                Cell cell1 = emptyCells[randomIndex1];
                Vector3 position1 = new(cell1.transform.position.x, 0.5f, cell1.transform.position.z);
                Instantiate(m_spherePrefabTogether, position1, Quaternion.identity);
                emptyCells.RemoveAt(randomIndex1);
            }

            if (colletableTag == "Dog Points")
            {
                // Dog sphere
                int randomIndex2 = UnityEngine.Random.Range(0, emptyCells.Count);
                Cell cell2 = emptyCells[randomIndex2];
                Vector3 position2 = new(cell2.transform.position.x, 0.5f, cell2.transform.position.z);
                Instantiate(m_spherePrefabDog, position2, Quaternion.identity);
                emptyCells.RemoveAt(randomIndex2);
            }

            if (colletableTag == "Player Points")
            {
                // Human sphere
                int randomIndex3 = UnityEngine.Random.Range(0, emptyCells.Count);
                Cell cell3 = emptyCells[randomIndex3];
                Vector3 position3 = new(cell3.transform.position.x, 0.5f, cell3.transform.position.z);
                Instantiate(m_spherePrefabHuman, position3, Quaternion.identity);
            }
        }
    }
}