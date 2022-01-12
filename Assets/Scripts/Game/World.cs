using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Overgrown.GameEnums;
public class World : MonoBehaviour
{
    public GameObject prefab;
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
                GameObject gridtile = Instantiate(prefab, transform);
                gridtile.transform.position = new Vector3(transform.position.x + (k * 10) + 5, transform.position.y, transform.position.z - (i * 10) + 5); //rotation
                gridtile.name = "Tile " + (1 + k).ToString("0#") + "x" + (1 + i).ToString("0#");
                gridtile.GetComponent<MeshCollider>().sharedMesh = gridtile.GetComponent<Tile>().BuildBuilding(grey, red, green);
                gridtile.GetComponent<Tile>().SetID(k + i * columns);
                tiles.Add(gridtile.GetComponent<Tile>());
            }
        }
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
    }

    public void OnWinSwitchColor()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].GetComponent<Tile>().SwitchColor(CellState.Filled);
        }
    }
}
