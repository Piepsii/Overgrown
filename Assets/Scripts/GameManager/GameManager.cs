using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
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
        private LevelManager levelManager;
        private UIManager uIManager;

        public static GameManager Instance { get => gameManager; }
        public TimeManager Time { get => timeManager; set => timeManager = value; }
        public LevelManager LevelManager { get => levelManager; set => levelManager = value; }
        public UIManager UIManager { get => uIManager; set => uIManager = value; }

        private void Start()
        {
            ppVolume = FindObjectOfType<Volume>();
        }

        public void SetStateToGameStart()
        {
            gameState = GameState.GameStart;
            uIManager.SwitchState(gameState);
            ppVolume.profile = ppProfileMenu;
            GameObject.FindWithTag("Player").GetComponent<Player>().state = CameraState.Idle;
            levelManager.level.World.ToggleHoverColoring(false);
        }

        public void SetStateToGame()
        {
            gameState = GameState.Game;
            uIManager.SwitchState(gameState);
            ppVolume.profile = ppProfileGame;
            GameObject.FindWithTag("Player").GetComponent<Player>().state = CameraState.Automatic;
            levelManager.level.World.ToggleHoverColoring(true);
            levelManager.level.CreateLevel();
        }

        public void RestartGame()
        {
            gameState = GameState.Game;
            uIManager.SwitchState(gameState);
            ppVolume.profile = ppProfileGame;
            GameObject.FindWithTag("Player").GetComponent<Player>().state = CameraState.Automatic;
            levelManager.level.World.ToggleHoverColoring(false);
            levelManager.level.CreatePuzzle();
            levelManager.level.CreateLevel();
            levelManager.level.World.DisableTrees();
        }

        [ContextMenu("Set Game Over")]
        public void SetStateToGameOver()
        {
            gameState = GameState.GameOver;
            uIManager.SwitchState(gameState);
            ppVolume.profile = ppProfileGame;
            GameObject.FindWithTag("Player").GetComponent<Player>().state = CameraState.Idle;
            levelManager.level.World.ToggleHoverColoring(false);
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

