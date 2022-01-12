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
    private Material[] grey;
    private Material[] red;

    private List<GameObject> childObjects = new List<GameObject>();

    private List<Material[]> originalMats = new List<Material[]>();


    private Mesh mesh;

    public List<GameObject> Tileprefab = new List<GameObject>();

    private bool _checked;

    public Mesh Mesh { get; }

    public Mesh BuildBuilding(Material[] _grey, Material[] _red) //To be changed to appropriate algorithm (probably get prefabs and instantiate them)
    {
        red = _red;
        grey = _grey;
        GameObject go = Instantiate(Tileprefab[Random.Range(0, Tileprefab.Count)], transform);
        go.transform.position = transform.position + Vector3.up * 0.0f + Vector3.right * 5f + Vector3.forward * 5f;

        for(int i = 0; i < go.transform.childCount; i++)
        {
            childObjects.Add(go.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < childObjects.Count; i++)
        {
            originalMats.Add(childObjects[i].GetComponent<MeshRenderer>().sharedMaterials);
        }
        SwitchColor(Overgrown.GameEnums.CellState.Empty);

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
    public void SwitchColor(Overgrown.GameEnums.CellState state)
    {
        if (state == Overgrown.GameEnums.CellState.Crossed)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = red;
            }
        }
        else if (state == Overgrown.GameEnums.CellState.Empty)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = grey;
            }
        }
        else if (state == Overgrown.GameEnums.CellState.Filled)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = originalMats[i];
            }
        }
    }
}