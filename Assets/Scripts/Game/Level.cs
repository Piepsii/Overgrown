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
    private World world;
    private Puzzle puzzle;

    public int Width { get => width; }
    public int Height { get => height; }
    public Puzzle Puzzle { get => puzzle; }
    public World World { get => world; }

    VoidEventChannelSO OnLevelStart;
    VoidEventChannelSO OnLevelEnd;

    private void Awake()
    {
        timeAtStart = Time.time;
        
        puzzle = GetComponentInChildren<Puzzle>();
        puzzle.SetSizePercentage(width, height, percentage);
        puzzle.NewPuzzle();

        AdjustIslandSize(width, height);

        world = GetComponentInChildren<World>();
        world.GenerateGrid(width, height);
    }

    public void CreatePuzzle()
    {
        puzzle.NewPuzzle();
    }

    public void CreateLevel()
    {
        world.GenerateGrid(width, height);
        world.ToggleHoverColoring(true);
    }

    private void Update()
    {
        if (puzzle.Solved)
        {
            world.OnWinSwitchOnTrees();
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

    private float CalculateTime()
    {
        return Time.time - timeAtStart;
    }

    private void AdjustIslandSize(int width, int height)
    {
        Transform IslandTransform = GameObject.FindGameObjectWithTag("Island").transform;
        if(width * height == 25)
        {
            IslandTransform.position = new Vector3(21.0f, -20.0f, -23.0f);
            IslandTransform.localScale = new Vector3(15f, 15f, 15f);
        }
        else if(width * height == 100)
        {
            IslandTransform.position = new Vector3(48.0f, -44.0f, -50.0f);
            IslandTransform.localScale = new Vector3(32f, 32f, 32f);
        }
    }
}
