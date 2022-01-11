using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overgrown.GameManager;
using Overgrown.GameEnums;

public class Player : MonoBehaviour
{
    private Camera cam;
    private string leftMouseButton = "LMB";
    private string rightMouseButton = "RMB";

    private void Start()
    {
        if (GameManager.Instance.Player == null)
        {
            GameManager.Instance.Player = this;
        }
        cam = Camera.main;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        var levelManager = GameManager.Instance.LevelManager;
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        Vector2 mousePosition = new Vector2(mouseX, mouseY);
        Vector3 worldPosition = cam.ScreenToWorldPoint(mousePosition);
        if (PressedPrimaryButton())
        {
            levelManager.ToggleCellState(worldPosition);
            Debug.Log("Toggle Cell State");
        }
        else if (PressedSecondaryButton())
        {
            levelManager.CrossCell(worldPosition);
            Debug.Log("Cross out Cell");
        }
    }

    private bool PressedPrimaryButton()
    {
        return Input.GetButtonDown(leftMouseButton);
    }

    private bool PressedSecondaryButton()
    {
        return Input.GetButtonDown(rightMouseButton);
    }
}
