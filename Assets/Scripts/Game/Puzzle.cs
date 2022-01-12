using System.Collections.Generic;
using UnityEngine;
using Overgrown.GameEnums;

[ExecuteInEditMode]
public class Puzzle : MonoBehaviour
{
    public bool[] solution;

    private int width, height;
    private CellState[] cellState;
    private int percentage;
    private bool solved;

    private List<int[]> cluesRows = new List<int[]>();
    private List<int[]> cluesColumns = new List<int[]>();

    public bool Solved { get; }
    public int Width { get; }
    public int Height { get; }

    public void NewPuzzle()
    {
        SetSizePercentage(5, 5, 15);
        solution = CreateNewSolution();
        cluesColumns.Clear();
        cluesRows.Clear();
        GenerateClues(5, 5);
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

    public void ToggleCellState(int id)
    {
        if(cellState[id] == CellState.Empty || cellState[id] == CellState.Crossed)
        {
            cellState[id] = CellState.Filled;
        }
        else
        {
            cellState[id] = CellState.Empty;
        }
    }

    public void CrossCell(int id)
    {
        cellState[id] = CellState.Crossed;
    }

    private void CheckPuzzle()
    {
        for (int i = 0; i < cellState.Length; i++)
        {
            if (cellState[i] == CellState.Filled && solution[i] || cellState[i] == CellState.Empty && !solution[i])
            {
                continue;
            }
            return;
        }
        solved = true;
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

        return new_solution;
    }

    private void SetSizePercentage(int _width, int _height, int _percentage)
    {
        height = _height;
        width = _width;
        percentage = _percentage;
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
