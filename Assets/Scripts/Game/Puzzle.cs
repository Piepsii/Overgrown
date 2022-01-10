using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private int width, height;
    private Cell[] cells;
    bool[] solution;
    private int percentage;
    private bool solved;

    public bool Solved { get; }
    public int Width { get; }
    public int Height { get; }


    private void Update()
    {
       
    }

    void CheckPuzzle()
    {
        for(int i = 0; i < cells.Length; i++)
        {
            if (cells[i]._cellState == Overgrown.GameEnums.CellState.Filled && solution[i])
            {
                continue;
            }
            return;
        }
        solved = true;
    }

    bool[] Solution()
    {
         solution = new bool[width * height];

        for(int i = 0; i < percentage; i++)
        {
            solution[i] = true;
        }

        for (int i = 0; i < solution.Length; i++)
        {
            int rnd = Random.Range(0, solution.Length);
            bool _temp = solution[rnd];
            solution[rnd] = solution[i];
            solution[i] = _temp;
        }

        return solution;
    }
}



/*
int total = percentage of the total amount of tiles needed 

for loop

randomly choose if a tile will be on or off
++count

if count > total stop

row check
goes through each row

checks if a tile is on or off, if on count++ then check next, if not print count reset check next repeat

column check same as above

int arr

 */

