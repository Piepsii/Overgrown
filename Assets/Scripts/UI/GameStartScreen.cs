using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Overgrown.GameManager;

public class GameStartScreen : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(GameManager.Instance.SetStateToGame);
    }
}
