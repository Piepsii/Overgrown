using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileSize
{
    None,
    OneFloor,
    TwoFloors,
    ThreeFloors
}
[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    public TileSize size;
    public GameObject Tileprefab;
    private List<GameObject> tiles = new List<GameObject>();

    public void BuildFloors()
    {
        if (size == TileSize.None)
        {
            DeleteFloors();
        }
        if (size == TileSize.OneFloor)
        {
            DeleteFloors();
            GameObject go = Instantiate(Tileprefab, transform);
            go.transform.position = transform.position + Vector3.up * 1.75f;
            tiles.Add(go);
        }
        else if (size == TileSize.TwoFloors)
        {
            DeleteFloors();
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate(Tileprefab, transform);
                go.transform.position = transform.position + Vector3.up * (1.75f + (i * 3.5f));
                tiles.Add(go);
            }
        }
        else if (size == TileSize.ThreeFloors)
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
            size = TileSize.OneFloor;
        }
        else if (num == 1)
        {
            size = TileSize.TwoFloors;
        }
        else
        {
            size = TileSize.ThreeFloors;
        }
        BuildFloors();
    }
}