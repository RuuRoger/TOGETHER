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
        private DogPlayer m_dogPlayer;
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

            m_dogPlayer = FindFirstObjectByType<DogPlayer>();
            m_cellManager = FindFirstObjectByType<CellManager>();

            //Force to suscribe CellManager to Dogplayer Event
            if (m_cellManager != null && m_dogPlayer != null)
            {
                m_cellManager.SubscribeToDogEvents(m_dogPlayer);
            }

            yield return new WaitForEndOfFrame();

            if (m_dogPlayer != null)
            {
                m_dogPlayer.ReadIdCell();
            }
        }
    }
}