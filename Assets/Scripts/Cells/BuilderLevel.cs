using UnityEngine;

namespace Assets.Scripts.Cells
{
    public class BuilderLevel : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        [SerializeField] private GameObject m_cellPrefab;

        // =================================== PUBLIC METHODS ===================================
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

    }
}