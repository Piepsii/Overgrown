using UnityEngine;
using Overgrown.GameEnums;

namespace Overgrown.GameManager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject canvas;
        [SerializeField]
        private GameObject canvas3D;
        private GameStartScreen gameStartScreen;
        private GameScreen gameScreen;
        private GameOverScreen gameOverScreen;


        private void Awake()
        {
            canvas = Instantiate(canvas);
            canvas3D = Instantiate(canvas3D);
            canvas.name = "Canvas";
            canvas3D.name = "Canvas 3D";
            gameStartScreen = canvas.transform.GetComponentInChildren<GameStartScreen>(true);
            gameScreen = canvas3D.transform.GetComponentInChildren<GameScreen>(true);
            gameOverScreen = canvas.transform.GetComponentInChildren<GameOverScreen>(true);
        }

        private void Start()
        {
            if (GameManager.Instance.UIManager == null)
            {
                GameManager.Instance.UIManager = this;
            }
            gameScreen.gameObject.SetActive(false);
            gameOverScreen.gameObject.SetActive(false);
        }

        public void SwitchState(GameState state)
        {
            switch (state)
            {
                case GameState.GameStart:
                    gameStartScreen.gameObject.SetActive(true);
                    gameScreen.gameObject.SetActive(false);
                    gameOverScreen.gameObject.SetActive(false);

                    break;
                case GameState.Game:
                    gameStartScreen.gameObject.SetActive(false);
                    gameScreen.gameObject.SetActive(true);
                    gameScreen.Create();
                    gameOverScreen.gameObject.SetActive(false);

                    break;
                case GameState.GameOver:
                    gameStartScreen.gameObject.SetActive(false);
                    gameScreen.gameObject.SetActive(false);
                    gameOverScreen.gameObject.SetActive(true);

                    break;
            }
        }
    }

}
