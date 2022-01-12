using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overgrown.Utility;

public class Level : MonoBehaviour
{
    public int index = 0;

    [SerializeField]
    int width, height, percentage;


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
        puzzle = GetComponentInChildren<Puzzle>();
        puzzle.SetSizePercentage(width, height, percentage);
        puzzle.NewPuzzle();
        world = GetComponentInChildren<World>();
        world.GenerateGrid(width, height);
    }

    private void Update()
    {
        if (puzzle.Solved)
        {
            won = true;
        }
    }

    public void ToggleCellState(int id)
    {
        var state = puzzle.ToggleCellState(id);
        world.SwitchColor(id, state);
    }

    public void CrossCell(int id)
    {
        var state = puzzle.CrossCell(id);
        world.SwitchColor(id, state);
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
