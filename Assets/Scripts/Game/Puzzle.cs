using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Puzzle : MonoBehaviour
{
    private int width, height;
    private Overgrown.GameEnums.CellState[] _cellState;
    [SerializeField]
    public bool[] solution; //Editable
    private int percentage;
    private bool solved;

    private List<int[]> cluesRows = new List<int[]>();
    private List<int[]> cluesColumns = new List<int[]>();

    public bool Solved { get; }
    public int Width { get; }
    public int Height { get; }

    private void Update()
    {

    }

    public void NewPuzzle()
    {
        SetSizePercentage(5, 5, 15);
        solution = Solution();
        cluesColumns.Clear();
        cluesRows.Clear();
        GenerateClues(5, 5);
    }

    void CheckPuzzle()
    {
        for (int i = 0; i < _cellState.Length; i++)
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

    void SetSizePercentage(int _width, int _height, int _percentage)
    {
        height = _height;
        width = _width;
        percentage = _percentage;
    }

    public void SwitchCellStateAt(Vector3 position)
    {

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

}
