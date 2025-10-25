using UnityEngine;

namespace Assets.Scripts.BuilderLevel
{
    public class CreateFloor : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        [SerializeField] private GameObject m_floorPrefab;

        // =================================== PRIVATE METHODS ===================================

        private void Start()
        {
            MakeFloor();
        }

        private void MakeFloor()
        {
            for (float k = 9f; k <= 29f; k++)
            {
                for (float i = 1f; i <= 29f; i += 2)
                {
                    Vector3 prefabPosition = new(i, 0f, k);
                    GameObject prefabFloorInstantiate = Instantiate(m_floorPrefab, prefabPosition, Quaternion.identity);
                }
            }
        }

    }
}