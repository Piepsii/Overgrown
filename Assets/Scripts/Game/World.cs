using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Overgrown.GameEnums;
public class World : MonoBehaviour
{
    public GameObject tileprefab;
    public Material[] grey;
    public Material[] red;
    public Material[] green;

    private int grid_columns;
    private int grid_rows;
    private List<Tile> tiles = new List<Tile>();
    
    public void CreateGrid()
    {
        if (transform.childCount > 0)
        {
            Debug.LogError("Destroy previous grid first");
        }
        else
        {
            ClearConsole();
            GenerateGrid(grid_rows, grid_columns);
        }
    }

    public void GenerateGrid(int rows, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            for (int k = 0; k < rows; k++)
            {
                GameObject gridtile = Instantiate(tileprefab, transform);
                gridtile.transform.position = new Vector3(transform.position.x + (k * 11), transform.position.y, transform.position.z - (i * 11)); //rotation
                gridtile.name = "Tile " + (1 + k).ToString("0#") + "x" + (1 + i).ToString("0#");
                gridtile.GetComponent<MeshCollider>().sharedMesh = gridtile.GetComponent<Tile>().BuildBuilding(grey, red, green);
                gridtile.GetComponent<Tile>().SetID(k + i * columns);



                tiles.Add(gridtile.GetComponent<Tile>());
            }
        }

        DisableTiles();
    }

    public void DeleteGrid()
    {
        ClearConsole();
        for (int i = 0; i < transform.childCount;)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        tiles.Clear();
    }

    void ClearConsole()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    public void SetRowsColumns(int _rows, int _columns)
    {
        grid_rows = _rows;
        grid_columns = _columns;
    }

    public void SwitchColor(int index, CellState state) //Win
    {
        tiles[index].GetComponent<Tile>().SwitchColor(state);

        if(state == CellState.Filled)
        {
            tiles[index].GetComponent<Tile>().TileState(true);
            tiles[index].GetComponent<Tile>().CrossState(false);
        }
        if(state == CellState.Empty)
        {
            tiles[index].GetComponent<Tile>().TileState(false);
            tiles[index].GetComponent<Tile>().CrossState(false);
        }
        if(state == CellState.Crossed)
        {
            tiles[index].GetComponent<Tile>().TileState(false);
            tiles[index].GetComponent<Tile>().CrossState(true);
        }
    }

    public void OnWinSwitchColor()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].GetComponent<Tile>().SwitchColor(CellState.Filled);
        }
    }

    private void DisableTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].GetComponent<Tile>().TileState(false);
        }
    }
    
}
