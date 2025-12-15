using System;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        [SerializeField] private TextMeshProUGUI m_textLivesHuman;
        [SerializeField] private TextMeshProUGUI m_textLivesDog;

        // ================================================== PROPERTIES ==================================================
        public static UIManager Instance {get; set;}

        // ================================================== EVENTS ==================================================
        private void OnEnable()
        {
            PlayerManager.OnPoints += ChangePoints;
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

        private void ChangePoints(string playerTag, int points)
        {
            if (playerTag == "Player")
            {
                m_textLivesHuman.text = points.ToString();    
            }
            else
            {
                m_textLivesDog.text = points.ToString();
            }
        }
    }
}