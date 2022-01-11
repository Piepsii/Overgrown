using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overgrown.Utility;

public class Level : MonoBehaviour
{
    public int index = 0;

    private float timeAtStart;
    private bool won;
    private World world;
    private Puzzle puzzle;

    VoidEventChannelSO OnLevelStart;
    VoidEventChannelSO OnLevelEnd;

    private void Awake()
    {
        timeAtStart = Time.time;
        won = false;
    }

    private void Update()
    {
        if (puzzle.Solved)
        {
            won = true;
        }
    }

    public void ChangeCellState(Vector3 position)
    {
        puzzle.SwitchCellStateAt(position);
    }

    public int GetWidth()
    {
        return puzzle.Width;
    }

    public int GetHeight()
    {
        return puzzle.Height;
    }

    private float CalculateTime()
    {
        return Time.time - timeAtStart;
    }
}
