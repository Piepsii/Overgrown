using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CustomGrid : MonoBehaviour
{
    public GameObject prefab;
    public int columns;
    public int rows;

    public void CreateGrid()
    {
        if (transform.childCount > 0)
        {
            Debug.LogError("Destroy previous grid first");
        }
        else
        {
            ClearConsole();
            GenerateGrid(rows, columns);
        }
    }


    void GenerateGrid(int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int k = 0; k < columns; k++)
            {
                GameObject gridtile = Instantiate(prefab, transform);
                gridtile.transform.position = new Vector3(transform.position.x + (k * 10) + 5, transform.position.y, transform.position.z + (i * 10) + 5);
                gridtile.name = "Tile " + (1 + k).ToString() + "x" + (1 + i).ToString();
                gridtile.GetComponent<Tile>().GenerateTile(Random.Range(0, 2));

                gridtile.GetComponent<Tile>().row = k + 1;
                gridtile.GetComponent<Tile>().column = i + 1; //private, check constructor
            }
        }
    }

    public void DeleteGrid()
    {
        ClearConsole();
        while (transform.childCount > 0) //make it for
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    void ClearConsole()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
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

 */
