using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Overgrown.GameManager;

public class ClueUI : MonoBehaviour
{
    private int width, height;
    private Puzzle puzzle;
    private VerticalLayoutGroup horizontalGroup;
    private HorizontalLayoutGroup verticalGroup;

    private TextMeshPro[] vertical, horizontal;

    public void CreateUI()
    {
        var level = GameManager.Instance.LevelManager.activeLevel;
        width = level.Width;
        height = level.Height;
        puzzle = level.Puzzle;

        horizontalGroup = GetComponentInChildren<VerticalLayoutGroup>();
        verticalGroup = GetComponentInChildren<HorizontalLayoutGroup>();
        vertical = new TextMeshPro[height];
        horizontal = new TextMeshPro[width];

        for (int i = 0; i < height; i++)
        {
            GameObject clueObject = new GameObject("Horizontal Clue " + i);
            var addedComponent = clueObject.AddComponent<TextMeshPro>();
            horizontal[i] = addedComponent;

            horizontal[i].transform.SetParent(horizontalGroup.transform);
            string clue = string.Join(" ", puzzle.CluesRows[i]);
            horizontal[i].text = clue;
            horizontal[i].alignment = TextAlignmentOptions.Right;
            horizontal[i].transform.eulerAngles = new Vector3(0, 0, 0);
        }
        for (int i = 0; i < width; i++)
        {
            GameObject clueObject = new GameObject("Vertical Clue " + i);
            var addedComponent = clueObject.AddComponent<TextMeshPro>();
            vertical[i] = addedComponent;

            vertical[i].transform.SetParent(verticalGroup.transform);
            string clue = string.Join(" ", puzzle.CluesColumns[i]);
            vertical[i].text = clue;
            vertical[i].alignment = TextAlignmentOptions.Bottom;
            vertical[i].transform.eulerAngles = new Vector3(0, 0, 0);
        }

        transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
