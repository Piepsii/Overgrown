using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CustomGrid : MonoBehaviour
{
    public GameObject prefab;
    public int grid_columns; //For now public in order to access those in the inspector
    public int grid_rows;

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
        for (int i = 0; i < rows; i++)
        {
            for (int k = 0; k < columns; k++)
            {
                GameObject gridtile = Instantiate(prefab, transform);
                gridtile.transform.position = new Vector3(transform.position.x + (k * 10) + 5, transform.position.y, transform.position.z - (i * 10) + 5); //rotation
                gridtile.name = "Tile " + (1 + k).ToString("0#") + "x" + (1 + i).ToString("0#");
                gridtile.GetComponent<Tile>().BuildBuilding();
                gridtile.GetComponent<Tile>().SetTileRowColumn(k + 1, i + 1);
                gridtile.GetComponent<Tile>().SetID(k + i * 10);
            }
        }
    }

    public void DeleteGrid()
    {
        ClearConsole();
        for(int i = 0; i < transform.childCount;)
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
