using UnityEngine;
using Overgrown.GameEnums;

namespace Overgrown.GameManager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject canvas;
        private GameStartScreen gameStartScreen;
        private GameScreen gameScreen;
        private GameOverScreen gameOverScreen;

        private ScreenState screenState = ScreenState.GameStart;

        private void Awake()
        {
            screenState = ScreenState.GameStart;
            canvas = Instantiate(canvas);
            canvas.name = "Canvas";
            var transform = canvas.transform;
            gameStartScreen = transform.GetComponentInChildren<GameStartScreen>(true);
            gameScreen = transform.GetComponentInChildren<GameScreen>(true);
            gameOverScreen = transform.GetComponentInChildren<GameOverScreen>(true);
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
    }

}
