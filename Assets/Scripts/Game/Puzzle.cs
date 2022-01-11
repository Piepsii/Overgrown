using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Puzzle : MonoBehaviour
{
    private int width, height;
    private Overgrown.GameEnums.CellState[] _cellState;
    private bool[] solution; //Editable
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
        SetSizePercentage(10, 10, 50);
        solution = Solution();
    }

    void CheckPuzzle()
    {
        for(int i = 0; i < _cellState.Length; i++)
        {
            if (_cellState[i] == Overgrown.GameEnums.CellState.Filled && solution[i] || _cellState[i] == Overgrown.GameEnums.CellState.Empty && !solution[i])
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

    void SetSizePercentage(int _width, int _height, int _percentage)
    {
        height = _height;
        width = _width;
        percentage = _percentage;
    }
}
