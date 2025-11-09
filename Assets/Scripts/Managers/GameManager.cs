using UnityEngine;
using System.Collections;
using Assets.Scripts.Cells;
using Assets.Scripts.Players;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Orchestrates the entire game flow and defines the excution order of all game system
    /// This script use Singleton
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // ================================================== FIELDS ==================================================
        private static GameManager m_instance;
        private BuilderLevel m_builderLevel;
        private BasePlayer m_basePlayer;
        private CellManager m_cellManager;
        private PlayerManager m_playerManager;

        // ================================================== PROPERTIES ==================================================
        public static GameManager Instance
        {
            get { return m_instance; }
            private set { m_instance = value; }
        }

        // ================================================== PRIVATE METHODS ==================================================

        /// <summary>
        /// Initialize the singleton
        /// Initialize BuilderLevel.cs
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            m_builderLevel = FindFirstObjectByType<BuilderLevel>();
        }

        ///<summary>
        /// Start the method "StartGame"
        /// </summary>
        private void Start()
        {
            StartGame();
        }

        ///<summary>
        /// Acces to MakeLevel public method
        /// When finished, start a corroutine to control flow 
        /// </summary>
        private void StartGame()
        {
            m_builderLevel.MakeLevel();
            StartCoroutine(InitializeAfterLevelBuilt());
        }

        ///<Summary>
        /// Control the game flow, waiting the end of frame to call the next step
        ///</Summary
        private IEnumerator InitializeAfterLevelBuilt()
        {
            yield return new WaitForEndOfFrame();

            m_builderLevel.InstantiateCharactersAndObjects();

            yield return new WaitForEndOfFrame();

            m_basePlayer = FindFirstObjectByType<BasePlayer>();
            m_cellManager = FindFirstObjectByType<CellManager>();

            //Force to suscribe CellManager to BasePlayer Event
            if (m_cellManager != null && m_basePlayer != null)
            {
                m_cellManager.SubscribeToBasePlayerEvents(m_basePlayer);
            }

            yield return new WaitForEndOfFrame();

            m_playerManager = FindFirstObjectByType<PlayerManager>();
            m_playerManager.PlayersInitialation();
        }
    }
}