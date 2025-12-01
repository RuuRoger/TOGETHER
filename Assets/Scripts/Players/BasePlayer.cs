using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using UnityEngine.AI;

namespace Assets.Scripts.Players
{
    public class BasePlayer : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private bool m_isSelected = false;
        private bool m_isMoving = false;

        // ================================================== PROPERTIES ==================================================
        public bool IsSelected
        {
            get { return m_isSelected; }
            set { m_isSelected = value; }
        }

        // ================================================== PUBLIC METHODS ==================================================
        public void MovePlayerToCell(Vector3 cellDestination)
        {
            if (m_isSelected && !m_isMoving)
            {
                StartCoroutine(MoveToCell(cellDestination));
            }
        }

        // ================================================== PRIVATE METHODS ==================================================
        private void OnMouseDown()
        {
            m_isSelected = true;
            string playerTag = gameObject.tag;
            PlayerManager.Instance.PlayerSelected(playerTag);
        }

        private IEnumerator MoveToCell(Vector3 destination)
        {
            m_isMoving = true;

            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = true;
                
                // Give time to initialize agent
                yield return null;
                yield return null;
                
                //this block is to fix potenccial errors
                if (!agent.isOnNavMesh)
                {
                    NavMeshHit hit;
                    Vector3 searchPos = new Vector3(transform.position.x, 0f, transform.position.z);
                    
                    if (NavMesh.SamplePosition(searchPos, out hit, 3f, NavMesh.AllAreas))
                    {
                        agent.Warp(hit.position);
                        transform.position = hit.position;
                        
                        // Give more time
                        for (int i = 0; i < 5; i++)
                        {
                            yield return null;
                            if (agent.isOnNavMesh) break;
                        }
                    }
                    
                    if (!agent.isOnNavMesh)
                    {
                        agent.enabled = false;
                        m_isMoving = false;
                        yield break;
                    }
                }
                
                agent.SetDestination(destination);
                
                while (agent.pathPending || agent.remainingDistance > 0.1f)
                {
                    yield return null;
                }
                
                agent.enabled = false;
            }

            transform.position = destination;

            CellManager.Instance.ResetAllCellsColors();

            m_isMoving = false;
        }
    }
}