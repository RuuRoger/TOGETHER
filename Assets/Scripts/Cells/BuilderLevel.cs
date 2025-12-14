using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Collectables;

namespace Assets.Scripts.Cells
{
    public class BuilderLevel : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        [SerializeField] private GameObject m_cellPrefab;
        [SerializeField] private GameObject m_dogPrefab;
        [SerializeField] private GameObject m_humanPrefab;
        [SerializeField] private GameObject m_spherePrefabTogether;
        [SerializeField] private GameObject m_spherePrefabDog;
        [SerializeField] private GameObject m_spherePrefabHuman;

        // ================================================== EVENTS ==================================================
        private void OnEnable()
        {
            Collectable1.OnCollected += InstantiateOneCollectible;
        }

        // ================================================== PUBLIC METHODS ==================================================
        ///<Summary>
        /// Create a grid
        /// For each cell created, make an ID cell
        /// Also, add every cell in a list in CellManager
        ///</Summary
        public void MakeLevel()
        {
            int i = 1; // row

            for (float l = 0f; l <= 14; l += 2)
            {
                int j = 1; // column

                for (float k = 0f; k <= 20; k += 2)
                {
                    //Instantiation
                    Vector3 prefabPosition = new(k, 0f, l);
                    GameObject prefabFloorInstantiate = Instantiate(m_cellPrefab, prefabPosition, Quaternion.identity, transform);

                    //Get script reference
                    Cell cellComponent = prefabFloorInstantiate.GetComponent<Cell>();

                    //Make ID
                    Vector2 id = new(j, i);
                    cellComponent.IDCell = id;

                    //Add to CellManager list
                    CellManager.Instance.AddCell(cellComponent);

                    j++;
                }

                i++;
            }
        }

        /// <summary>
        /// Instantiate in scene every Player
        /// Later, call the method to insanteate collectables
        /// </summary>
        public void InstantiateCharacters()
        {
            // Dog
            Vector3 dogPosition = new(0f, 0.1f, 10f);
            Quaternion dogRotation = Quaternion.Euler(0f, 90f, 0);
            GameObject dogInstantiatePrefab = Instantiate(m_dogPrefab, dogPosition, dogRotation);

            // Human
            Vector3 humanPosition = new(0f, 0.1f, 12f);
            Quaternion humanROtation = Quaternion.Euler(0f, 90f, 0f);
            GameObject humanInstantiatePrefab = Instantiate(m_humanPrefab, humanPosition, humanROtation);

            PlayerManager.Instance.GetPlayer();

            InstantiateAllCollectables();
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void Start()
        {
            MakeLevel();
            InstantiateCharacters();
        }

        /// <summary>
        /// Instantiate the collectables
        /// </summary>
        private void InstantiateAllCollectables()
        {
            // Collectables
            List<Cell> emptyCells = CellManager.Instance.GetCellsWithOutObstacles();

            if (emptyCells.Count >= 3)
            {
                // Together sphere
                int randomIndex1 = Random.Range(0, emptyCells.Count);
                Cell cell1 = emptyCells[randomIndex1];
                Vector3 position1 = new(cell1.transform.position.x, 0.5f, cell1.transform.position.z);
                Instantiate(m_spherePrefabTogether, position1, Quaternion.identity);
                emptyCells.RemoveAt(randomIndex1);

                // Dog sphere
                int randomIndex2 = Random.Range(0, emptyCells.Count);
                Cell cell2 = emptyCells[randomIndex2];
                Vector3 position2 = new(cell2.transform.position.x, 0.5f, cell2.transform.position.z);
                Instantiate(m_spherePrefabDog, position2, Quaternion.identity);
                emptyCells.RemoveAt(randomIndex2);

                // Human sphere
                int randomIndex3 = Random.Range(0, emptyCells.Count);
                Cell cell3 = emptyCells[randomIndex3];
                Vector3 position3 = new(cell3.transform.position.x, 0.5f, cell3.transform.position.z);
                Instantiate(m_spherePrefabHuman, position3, Quaternion.identity);
            }
        }

        /// <summary>
        /// Instantiate a specific collectable. Recives a tag and depending its value, calls and collectable or another
        /// </summary>
        /// <param name="colletableTag"></param>
        
        private void InstantiateOneCollectible(string colletableTag)
        {
            StartCoroutine(InstantiateOneCollectibleCoroutine(colletableTag));
        }

        private IEnumerator InstantiateOneCollectibleCoroutine(string colletableTag)
        {
            yield return new WaitForSeconds(1f);

            // Collectables
            List<Cell> emptyCells = CellManager.Instance.GetCellsWithOutObstacles();

            if (colletableTag == "Together Points")
            {
                int randomIndex1 = Random.Range(0, emptyCells.Count);
                Cell cell1 = emptyCells[randomIndex1];
                Vector3 position1 = new(cell1.transform.position.x, 0.5f, cell1.transform.position.z);
                Instantiate(m_spherePrefabTogether, position1, Quaternion.identity);
                emptyCells.RemoveAt(randomIndex1);
            }

            if (colletableTag == "Dog Points")
            {
                // Dog sphere
                int randomIndex2 = Random.Range(0, emptyCells.Count);
                Cell cell2 = emptyCells[randomIndex2];
                Vector3 position2 = new(cell2.transform.position.x, 0.5f, cell2.transform.position.z);
                Instantiate(m_spherePrefabDog, position2, Quaternion.identity);
                emptyCells.RemoveAt(randomIndex2);
            }

            if (colletableTag == "Player Points")
            {
                // Human sphere
                int randomIndex3 = Random.Range(0, emptyCells.Count);
                Cell cell3 = emptyCells[randomIndex3];
                Vector3 position3 = new(cell3.transform.position.x, 0.5f, cell3.transform.position.z);
                Instantiate(m_spherePrefabHuman, position3, Quaternion.identity);
            }
        }
    }
}