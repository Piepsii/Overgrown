using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class World : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField]
    private int grid_columns; //For now public in order to access those in the inspector
    [SerializeField]
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


    void GenerateGrid(int rows, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            for (int k = 0; k < rows; k++)
            {
                GameObject gridtile = Instantiate(prefab, transform);
                gridtile.transform.position = new Vector3(transform.position.x + (k * 10) + 5, transform.position.y, transform.position.z - (i * 10) + 5); //rotation
                gridtile.name = "Tile " + (1 + k).ToString("0#") + "x" + (1 + i).ToString("0#");
                gridtile.GetComponent<MeshCollider>().sharedMesh  = gridtile.GetComponent<Tile>().BuildBuilding();

                gridtile.GetComponent<Tile>().SetTileRowColumn(k + 1, i + 1);
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

    //method SwitchColor(int index)
}
