using UnityEngine;

namespace Assets.Scripts.Cell
{
    public class Cell : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private bool m_isAccesible;

        private void Awake()
        {
            m_isAccesible = false;
        }

        private void Update()
        {
            //This is not the palce to execute this. Only for trainee
            CheckActive();
        }

        private void CheckActive()
        {
            Ray ray = new(transform.position, Vector3.up);
            float distance = 3f;

            if (Physics.Raycast(ray, distance))
            {
                m_isAccesible = false;
                Debug.Log(transform.position);
            }
            else
            {
                m_isAccesible = true;
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                Color newColor = Color.red;
                newColor.a = 0.7f;
                meshRenderer.material.color = newColor;
            }
        }
    }
}