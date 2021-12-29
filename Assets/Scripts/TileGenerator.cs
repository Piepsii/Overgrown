using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[ExecuteInEditMode]
public class TileGenerator : MonoBehaviour
{
    public GameObject prefab;
    public int rows;
    public int columns;

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
                gridtile.name = (1 + k).ToString() + "x" + (1 + i).ToString();
            }
        }
    }

    public void DeleteGrid()
    {
        ClearConsole();
        while (transform.childCount > 0)
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
