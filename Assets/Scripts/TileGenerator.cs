using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileGenerator : MonoBehaviour
{
    public float sizeOfmap;
    public GameObject prefab;
    bool generated;

    void Update()
    {
        if (Mathf.Sqrt(sizeOfmap) % 1 > 0)
        {
            Debug.LogError("Invalid grid size");
            generated = false;

        }
        else if (!generated)
        {
            GenerateGrid();
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
        generated = true;
    }
}
