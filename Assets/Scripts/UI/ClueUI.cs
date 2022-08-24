using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Overgrown.GameManager;

public class ClueUI : MonoBehaviour
{
    public bool displayAsSymbols = true;

    private int width, height;
    private Puzzle puzzle;
    private VerticalLayoutGroup horizontalGroup;
    private HorizontalLayoutGroup verticalGroup;

    private TextMeshPro[] vertical, horizontal;

    private HorizontalLayoutGroup[] rows;
    private VerticalLayoutGroup[] columns;
    private Sprite[] icons = new Sprite[5];
    private Image[][] rowClues, columnClues;

    private void Start()
    {
        for(int i = 0; i < icons.Length; i++)
        {
            icons[i] = Resources.Load<Sprite>("Sprites/S_House_" + (i + 1).ToString());
        }
        CreateUI();
        transform.Rotate(new Vector3(1, 0, 0), 90);
        verticalGroup.transform.Translate(new Vector3(-5, 45));
    }

    private void CreateUI()
    {
        var level = GameManager.Instance.LevelManager.level;
        width = level.Width;
        height = level.Height;
        puzzle = level.Puzzle;

        horizontalGroup = GetComponentInChildren<VerticalLayoutGroup>();
        verticalGroup = GetComponentInChildren<HorizontalLayoutGroup>();

        if (displayAsSymbols)
        {
            CreateUIWithSymbols();
        }
        else
        {
            CreateUIWithNumbers();
        }

    }

    private void CreateUIWithSymbols()
    {
        rows = new HorizontalLayoutGroup[height];
        for(int i = 0; i < height; i++)
        {
            rowClues = new Image[height][];
            GameObject row = new GameObject("Row" + i);
            var addedHLG = row.AddComponent<HorizontalLayoutGroup>();
            addedHLG.childControlWidth = false;
            addedHLG.childForceExpandWidth = false;
            addedHLG.childAlignment = TextAnchor.MiddleRight;
            rows[i] = addedHLG;
            rows[i].transform.SetParent(horizontalGroup.transform);

            for(int j = 0; j < puzzle.CluesRows[i].Length; j++)
            {
                rowClues[i] = new Image[puzzle.CluesRows[i].Length];
                GameObject clue = new GameObject("Row " + i + ", Clue " + j);
                var addedImage = clue.AddComponent<Image>();
                int value = puzzle.CluesRows[i][j];
                int index = Mathf.Max(value - 1, 0);
                addedImage.sprite = icons[index];
                addedImage.transform.SetParent(rows[i].transform);
                addedImage.preserveAspect = true;
                addedImage.rectTransform.sizeDelta = new Vector2(15f, 0f);
                rowClues[i][j] = addedImage;
            }
        }
        columns = new VerticalLayoutGroup[width];
        verticalGroup.transform.position = new Vector2(50f, -10f);
        for (int i = 0; i < width; i++)
        {
            columnClues = new Image[width][];
            GameObject col = new GameObject("Column" + i);
            var addedVLG = col.AddComponent<VerticalLayoutGroup>();
            addedVLG.childControlHeight = false;
            addedVLG.childForceExpandHeight = false;
            addedVLG.childAlignment = TextAnchor.LowerCenter;
            columns[i] = addedVLG;
            columns[i].transform.SetParent(verticalGroup.transform);

            for (int j = 0; j < puzzle.CluesColumns[i].Length; j++)
            {
                columnClues[i] = new Image[puzzle.CluesColumns[i].Length];
                GameObject clue = new GameObject("Column " + i + ", Clue " + j);
                var addedImage = clue.AddComponent<Image>();
                int value = puzzle.CluesColumns[i][j];
                int index = Mathf.Max(value - 1, 0);
                addedImage.sprite = icons[index];
                addedImage.transform.SetParent(columns[i].transform);
                addedImage.preserveAspect = true;
                addedImage.rectTransform.sizeDelta = new Vector2(0f, 15f);
                columnClues[i][j] = addedImage;
            }
        }
    }

    private void CreateUIWithNumbers()
    {
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
    }
}
