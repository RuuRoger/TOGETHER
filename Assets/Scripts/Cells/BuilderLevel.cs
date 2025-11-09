using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class BuilderLevel : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        [SerializeField] private GameObject m_cellPrefab;
        [SerializeField] private GameObject m_dogPrefab;

        // ================================================== PUBLIC METHODS ==================================================

        ///<Summary>
        /// Create a grid
        /// For each cell created, make an ID cell
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

                    j++;
                }

                i++;
            }
        }

        public void InstantiateCharactersAndObjects()
        {
            //Dog
            Vector3 dogPosition = new(0f, 0.1f, 10f);
            Quaternion dogRotation = Quaternion.Euler(0f, 90f, 0);
            GameObject dogInstantiatePrefab = Instantiate(m_dogPrefab, dogPosition, dogRotation);
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void Start()
        {
            MakeLevel();
        }
    }
}