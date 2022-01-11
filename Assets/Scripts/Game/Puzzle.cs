using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Puzzle : MonoBehaviour
{
    private int width, height;
    private Cell[] cells;
    private bool[] solution;
    private int percentage;
    private bool solved;

    public bool Solved { get; }
    public int Width { get; }
    public int Height { get; }


    private void Update()
    {
       
    }

    public void NewPuzzle()
    {
        SetSize(10, 10);
        percentage = 50;
        solution = Solution();
    }

    void CheckPuzzle()
    {
        for(int i = 0; i < cells.Length; i++)
        {
            if (cells[i]._cellState == Overgrown.GameEnums.CellState.Filled && solution[i] || cells[i]._cellState == Overgrown.GameEnums.CellState.Empty && !solution[i])
            {
                continue;
            }
            return;
        }
        solved = true;
    }

    bool[] Solution()
    {
         bool[] new_solution = new bool[width * height];

        for(int i = 0; i < percentage; i++)
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

    void SetSize(int _width, int _height)
    {
        height = _height;
        width = _width;
    }
}
