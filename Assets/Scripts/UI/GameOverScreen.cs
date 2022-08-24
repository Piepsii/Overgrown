using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Overgrown.GameManager;

public class GameOverScreen : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(GameManager.Instance.RestartGame);
    }
}
