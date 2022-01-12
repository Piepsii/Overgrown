using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    public int unique_ID;
    private int tile_row;
    private int tile_column;

    bool clickable;

    //color

    private Mesh mesh;

    public List<GameObject> Tileprefab = new List<GameObject>();

    private bool _checked;

    public Mesh Mesh { get; }

    public Mesh BuildBuilding() //To be changed to appropriate algorithm (probably get prefabs and instantiate them)
    {
        GameObject go = Instantiate(Tileprefab[Random.Range(0, Tileprefab.Count)], transform);
        go.transform.position = transform.position + Vector3.up * 0.0f + Vector3.right * 5f + Vector3.forward * 5f;
        return go.GetComponent<MeshFilter>().sharedMesh;
    }

    public void SetTileRowColumn(int _row, int _column)
    {
        tile_row = _row;
        tile_column = _column;
    }

    public void SetID(int _ID)
    {
        unique_ID = _ID;
    }

    //Input Related Methods to be removed
    private void OnMouseEnter()
    {
        if (!_checked)
        {
            GetComponent<Renderer>().material.color = Color.green;
            clickable = true;
        }
    }


    //method SwitchColor()
}