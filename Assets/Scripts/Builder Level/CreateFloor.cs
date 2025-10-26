using UnityEngine;

namespace Assets.Scripts.BuilderLevel
{
    public class CreateFloor : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        [SerializeField] private GameObject m_floorPrefab;
        [SerializeField] private GameObject m_dogPrefab;
        // =================================== PRIVATE METHODS ===================================

        private void Start()
        {
            MakeFloor();
            PutPlayers();
        }

        private void MakeFloor()
        {
            for (float k = 9f; k <= 39f; k += 2)
            {
                for (float i = 1f; i <= 29f; i += 2)
                {
                    Vector3 prefabPosition = new(i, 0f, k);
                    GameObject prefabFloorInstantiate = Instantiate(m_floorPrefab, prefabPosition, Quaternion.identity);
                }
            }
        }

        private void PutPlayers()
        {
            Vector3 prefabDogPosition = new(1f, 0f, 9f);
            GameObject prefabDogInstantiate = Instantiate(m_dogPrefab, prefabDogPosition, Quaternion.identity);
        }

    }
}