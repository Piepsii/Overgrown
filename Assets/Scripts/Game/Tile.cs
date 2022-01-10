using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    None,
    OneFloor,
    TwoFloors,
    ThreeFloors
}

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    public float row;
    public float column;
    public TileType type;
    public GameObject Tileprefab;
    private List<GameObject> tiles = new List<GameObject>();

    private bool _checked;

    public void BuildFloors() //To be changed to appropriate algorithm (probably get prefabs and instantiate them)
    {
        if (type == TileType.None)
        {
            DeleteFloors();
        }
        else if(type == TileType.OneFloor)
        {
            DeleteFloors();
            GameObject go = Instantiate(Tileprefab, transform);
            go.transform.position = transform.position + Vector3.up * 1.75f;
            tiles.Add(go);
        }
        else if (type == TileType.TwoFloors)
        {
            DeleteFloors();
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate(Tileprefab, transform);
                go.transform.position = transform.position + Vector3.up * (1.75f + (i * 3.5f));
                tiles.Add(go);
            }
        }
        else if (type == TileType.ThreeFloors)
        {
            DeleteFloors();
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(Tileprefab, transform);
                go.transform.position = transform.position + Vector3.up * (1.75f + (i * 3.5f));
                tiles.Add(go);
            }
        }
    }

    public void DeleteFloors()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            DestroyImmediate(tiles[i].gameObject);
        }
        tiles.Clear();
    }

    public void GenerateTile(int num)
    {
        if (num == 0)
        {
            type = TileType.OneFloor;
        }
        else if (num == 1)
        {
            type = TileType.TwoFloors;
        }
        else
        {
            type = TileType.ThreeFloors;
        }
        BuildFloors();
    }

    private void OnMouseEnter()
    {
        if (!_checked)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
    private void OnMouseExit()
    {
        if (!_checked)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }

    }


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_checked)
            {
                GetComponent<Renderer>().material.color = Color.black;
                _checked = true;

            }
            else
            {
                GetComponent<Renderer>().material.color = Color.white;
                _checked = false;
            }
        }
        
        if (Input.GetMouseButtonDown(2))
        {
            if (!_checked)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}