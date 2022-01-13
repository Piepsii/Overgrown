using UnityEngine;
using Overgrown.GameEnums;

namespace Overgrown.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public GameState gameState = GameState.GameStart;

        [SerializeField]
        private string sceneToLoad;


        private static GameManager gameManager;
        private TimeManager timeManager;
        private GridManager gridManager;
        private SceneManager sceneManager;
        private LevelManager levelManager;
        private AudioManager audioManager;
        private UIManager uIManager;
        private Config config;
        private Player player;

        public string SceneToLoad { get => sceneToLoad; }

        public static GameManager Instance { get => gameManager; }
        public TimeManager Time { get => timeManager; set => timeManager = value; }
        public GridManager GridManager { get => gridManager; set => gridManager = value; }
        public SceneManager SceneController { get => sceneManager; set => sceneManager = value; }
        public LevelManager LevelManager { get => levelManager; set => levelManager = value; }
        public AudioManager AudioManager { get => audioManager; set => audioManager = value; }
        public UIManager UIManager { get => uIManager; set => uIManager = value; }
        public Config Config { get => config; set => config = value;}
        public Player Player { get => player; set => player = value; }

        public void SetStateToGameStart()
        {
            gameState = GameState.GameStart;
            uIManager.SwitchState(gameState);
        }

        public void SetStateToGame()
        {
            gameState = GameState.Game;
            uIManager.SwitchState(gameState);
        }

        public void SetStateToGameOver()
        {
            gameState = GameState.GameOver;
            uIManager.SwitchState(gameState);
        }


        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            GameObject prefab = GameObject.Instantiate(Resources.Load("GameManager")) as GameObject;
            if (!(gameManager = prefab.GetComponent<GameManager>()))
            {
                Debug.Log("GameManager: GameManager prefab does not contain a GameManager script");
            }
            DontDestroyOnLoad(prefab);

#if UNITY_WEBGL
        Cursor.lockState = CursorLockMode.Locked;
#endif
        }

    }
    

}

