using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overgrown.GameEnums;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    public int unique_ID;
    public List<GameObject> Tileprefab = new List<GameObject>();
    public GameObject crossprefab;


    private Material[] grey;
    private Material[] red;
    private Material[] green;
    private List<GameObject> childObjects = new List<GameObject>();
    private List<Material[]> originalMats = new List<Material[]>();
    private Mesh mesh;
    private bool _checked;
    public Mesh Mesh { get; }
    public Mesh BuildBuilding(Material[] _grey, Material[] _red, Material[] _green) //To be changed to appropriate algorithm (probably get prefabs and instantiate them)
    {
        red = _red;
        grey = _grey;
        green = _green;
        GameObject go = Instantiate(Tileprefab[Random.Range(0, Tileprefab.Count)], transform);
        go.transform.position = transform.position + Vector3.up * 0.0f + Vector3.right * 5f + Vector3.forward * 5f;

        GameObject gridcross = Instantiate(crossprefab, transform);
        gridcross.transform.position = go.transform.position + Vector3.up * 1.75f - Vector3.right * 5f - Vector3.forward * 5f;
        gridcross.SetActive(false);

        for (int i = 0; i < go.transform.childCount; i++)
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


    public void SetID(int _ID)
    {
        unique_ID = _ID;
    }

    //Input Related Methods to be removed
    private void OnMouseEnter()
    {
        //if (!_checked)
        //{
        //    for (int i = 0; i < childObjects.Count; i++)
        //    {
        //        childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = green;
        //        _checked = true;
        //    }
        //}
    }



    public void SwitchColor(CellState state)
    {
        if (state == Overgrown.GameEnums.CellState.Crossed)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = red;
                _checked = true;
            }
        }
        else if (state == Overgrown.GameEnums.CellState.Empty)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = grey;
                _checked = false;
            }
        }
        else if (state == Overgrown.GameEnums.CellState.Filled)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = originalMats[i];
                _checked = true;
            }
        }
    }

    public void TileState(bool isActive)
    {
        for (int i = 0; i < childObjects.Count; i++)
        {
            childObjects[i].SetActive(isActive);
        }
    }
}