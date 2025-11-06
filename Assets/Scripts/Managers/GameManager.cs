using UnityEngine;
using System.Collections;
using Assets.Scripts.Cells;
using Assets.Scripts.Players;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        // =================================== FIELDS ===================================
        private static GameManager m_instance;
        private BuilderLevel m_builderLevel;
        private BasePlayer m_basePlayer;
        private CellManager m_cellManager;

        // =================================== PROPERTIES ===================================
        public static GameManager Instance
        {
            get { return m_instance; }
            private set { m_instance = value; }
        }

        // =================================== PRIVATE METHODS ===================================
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

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            m_builderLevel.MakeLevel();
            StartCoroutine(InitializeAfterLevelBuilt());
        }

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
        }
    }
}