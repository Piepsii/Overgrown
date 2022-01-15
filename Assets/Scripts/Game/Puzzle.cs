using System.Collections.Generic;
using UnityEngine;
using Overgrown.GameEnums;
using Overgrown.GameManager;

[ExecuteInEditMode]
public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private bool[] solution;
    private int width, height;
    private CellState[] cellState;
    private int percentage;
    private bool solved;
    private List<int[]> cluesRows = new List<int[]>();
    private List<int[]> cluesColumns = new List<int[]>();

    public bool Solved { get => solved; }
    public int Width { get => width; }
    public int Height { get => height; }
    public List<int[]> CluesRows { get => cluesRows; }
    public List<int[]> CluesColumns { get => cluesColumns; }



    private void Start()
    {
    }
    public void NewPuzzle()
    {
        solution = CreateNewSolution();
        cluesColumns.Clear();
        cluesRows.Clear();
        GenerateClues(width, height);
        cellState = new CellState[solution.Length];
    }
    public void PrintClues()
    {
        for (int i = 0; i < cluesRows.Count; i++)
        {
            string result = string.Join(" ", cluesRows[i]);
            Debug.Log(result);
        }
        for (int i = 0; i < cluesColumns.Count; i++)
        {
            string result = string.Join(" ", cluesColumns[i]);
            Debug.Log(result);
        }
    }

    public CellState ToggleCellState(int id)
    {
        if (cellState[id] == CellState.Empty || cellState[id] == CellState.Crossed)
        {
            cellState[id] = CellState.Filled;
        }
        else
        {
            cellState[id] = CellState.Empty;
        }

        if (IsPuzzleSolved())
        {
            GameManager.Instance.SetStateToGameOver();
        }
        return cellState[id];

    }

    public CellState CrossCell(int id)
    {
        return cellState[id] = cellState[id] == CellState.Crossed ? CellState.Empty : CellState.Crossed;
    }

    private bool IsPuzzleSolved()
    {
        for (int i = 0; i < cellState.Length; i++)
        {
            if (cellState[i] == CellState.Filled && solution[i] || (cellState[i] == CellState.Empty || cellState[i] == CellState.Crossed) && !solution[i])
            {
                continue;
            }
            return false;
        }
        return solved = true;
    }

    private bool[] CreateNewSolution()
    {
        bool[] new_solution = new bool[width * height];

        for (int i = 0; i < percentage; i++)
        {
            new_solution[i] = true;
        }

        for (int i = 0; i < new_solution.Length; i++)
        {
            int rnd = Random.Range(0, new_solution.Length);
            bool _temp = new_solution[rnd];
            new_solution[rnd] = new_solution[i];
            new_solution[i] = _temp;
        }
        int count = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if(!new_solution[(i * height) + j])
                {
                    count++;
                }
            }
            if (count == 0)
            {
                new_solution[i * height] = true;
            }
           count = 0;
        }
   
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!new_solution[i + (height * j)])
                {
                    count++;
                }
            }
            if (count == 0)
            {
                new_solution[i] = true;
            }
            count = 0;
        }

        return new_solution;
    }

    public void SetSizePercentage(int _width, int _height, int _percentage)
    {
        height = _height;
        width = _width;
        float size = height * width;
        float tempperc = _percentage;
        percentage = (int) (size * tempperc / 100.0f);
    }

    private void GenerateClues(int width, int height)
    {
        List<int> temp = new List<int>();
        int count = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (solution[(i * height) + j])
                {
                    count++;
                }
                else if (count > 0)
                {
                    temp.Add(count);
                    count = 0;
                }
            }

            if (count > 0)
            {
                temp.Add(count);
                count = 0;
            }
            cluesRows.Add(temp.ToArray());
            temp.Clear();
        }

        for (int i = 0; i < height; i++)
        {
            if (count > 0)
            {
                temp.Add(count);
                count = 0;
            }

            for (int j = 0; j < width; j++)
            {
                if (solution[i + (j * height)])
                {
                    count++;
                }
                else if (count > 0)
                {
                    temp.Add(count);
                    count = 0;
                }
            }

            if (count > 0)
            {
                temp.Add(count);
                count = 0;
            }
            cluesColumns.Add(temp.ToArray());
            temp.Clear();
        }
    }
}
