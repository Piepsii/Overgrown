using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileGenerator : MonoBehaviour
{
    public float sizeOfmap;
    public GameObject prefab;

    public void CreateGrid()
    {
        if (Mathf.Sqrt(sizeOfmap) % 1 > 0)
        {
            Debug.LogError("Invalid grid size");
        }
        else
        {
            if (transform.childCount > 0)
            {
                Debug.LogError("Destroy previous grid first");
            }
            else
            {
                GenerateGrid();
            }
        }
    }


    void GenerateGrid()
    {
        for (int i = 0; i < Mathf.Sqrt(sizeOfmap); i++)
        {
            for (int k = 0; k < Mathf.Sqrt(sizeOfmap); k++)
            {
                GameObject gridtile = Instantiate(prefab, transform);
                gridtile.transform.position = new Vector3(transform.position.x + (k * 10) + 5, transform.position.y, transform.position.z + (i * 10) + 5);
                gridtile.name = (1 + k).ToString() + "x" + (1 + i).ToString();
            }
        }
    }

    public void DeleteGrid()
    {
        while (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}
