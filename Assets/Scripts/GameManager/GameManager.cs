using UnityEngine;
using UnityEngine.Rendering;
using Overgrown.GameEnums;

namespace Overgrown.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public GameState gameState = GameState.GameStart;

        private Volume ppVolume;
        [SerializeField]
        private VolumeProfile ppProfileMenu;
        [SerializeField]
        private VolumeProfile ppProfileGame;

        private static GameManager gameManager;
        private TimeManager timeManager;
        private GridManager gridManager;
        private SceneManager sceneManager;
        private LevelManager levelManager;
        private AudioManager audioManager;
        private UIManager uIManager;
        private Config config;
        private Player player;

        public static GameManager Instance { get => gameManager; }
        public TimeManager Time { get => timeManager; set => timeManager = value; }
        public GridManager GridManager { get => gridManager; set => gridManager = value; }
        public SceneManager SceneController { get => sceneManager; set => sceneManager = value; }
        public LevelManager LevelManager { get => levelManager; set => levelManager = value; }
        public AudioManager AudioManager { get => audioManager; set => audioManager = value; }
        public UIManager UIManager { get => uIManager; set => uIManager = value; }
        public Config Config { get => config; set => config = value;}
        public Player Player { get => player; set => player = value; }

        private void Start()
        {
            ppVolume = FindObjectOfType<Volume>();
        }

        public void SetStateToGameStart()
        {
            gameState = GameState.GameStart;
            uIManager.SwitchState(gameState);
            ppVolume.profile = ppProfileMenu;
            player.state = CameraState.Idle;
            levelManager.activeLevel.World.ToggleHoverColoring(false);
        }

        public void SetStateToGame()
        {
            gameState = GameState.Game;
            uIManager.SwitchState(gameState);
            ppVolume.profile = ppProfileGame; 
            player.state = CameraState.Automatic;
            levelManager.activeLevel.World.ToggleHoverColoring(true);
            levelManager.activeLevel.CreateLevel();
        }

        public void SetStateToGameOver()
        {
            gameState = GameState.GameOver;
            uIManager.SwitchState(gameState);
            ppVolume.profile = ppProfileGame;
            player.state = CameraState.Idle;
            levelManager.activeLevel.World.ToggleHoverColoring(false);
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
        }

    }
    

}

