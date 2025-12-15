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
        [SerializeField] private TextMeshProUGUI m_textWin;
        [SerializeField] private TextMeshProUGUI m_textLose;

        // ================================================== PROPERTIES ==================================================
        public static UIManager Instance {get; set;}

        // ================================================== EVENTS ==================================================
        public static event Action OnFinishGame;
        
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

            if (points < 0)
            {
                Time.timeScale = 0f;
                m_textLose.gameObject.SetActive(true);
                OnFinishGame?.Invoke();                
            }

            if (PlayerManager.PlayerPoints >= 5 && PlayerManager.DogPlayerPoints >= 5)
            {
                Time.timeScale = 0f;
                m_textWin.gameObject.SetActive(true);
                OnFinishGame?.Invoke();
            }

        }
    }
}