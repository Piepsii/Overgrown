using UnityEngine;

namespace Overgrown.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private string sceneToLoad;

        private static GameManager gameManager;
        private TimeManager timeManager;
        private GridManager gridManager;
        private SceneManager sceneManager;
        private LevelManager levelManager;
        private AudioManager audioManager;
        private Config config;

        public string SceneToLoad { get => sceneToLoad; }

        public static GameManager Instance { get => gameManager; }
        public TimeManager Time { get => timeManager; set => timeManager = value; }
        public GridManager GridManager { get => gridManager; set => gridManager = value; }
        public SceneManager SceneController { get => sceneManager; set => sceneManager = value; }
        public LevelManager LevelManager { get => levelManager; set => levelManager = value; }
        public AudioManager AudioManager { get => audioManager; set => audioManager = value; }
        public Config Config { get => config; set => config = value;}


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

