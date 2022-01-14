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
    public GameObject treesprefab;
    public bool enableHoverColoring = false;

    private Material[] grey;
    private Material[] red;
    private Material[] green;
    private List<GameObject> childObjects = new List<GameObject>();
    private List<Material[]> originalMats = new List<Material[]>();
    private Mesh mesh;
    private CellState cellState;

    private GameObject cross;
    private GameObject trees;
    private GameObject tilebase;
    private Material tilebasemat;
    public Mesh Mesh { get; }
    public Mesh BuildBuilding(Material[] _grey, Material[] _red, Material[] _green) //To be changed to appropriate algorithm (probably get prefabs and instantiate them)
    {
        red = _red;
        grey = _grey;
        green = _green;

        tilebase = Instantiate(Tileprefab[Random.Range(0, Tileprefab.Count)], transform);
        tilebase.transform.position = transform.position + Vector3.up * 0.0f + Vector3.right * 5f + Vector3.forward * 5f;
        tilebasemat = tilebase.GetComponent<MeshRenderer>().sharedMaterial;

        cross = Instantiate(crossprefab, transform);
        cross.transform.position = tilebase.transform.position + Vector3.up * 1.75f - Vector3.right * 5f - Vector3.forward * 5f;
        cross.SetActive(false);

        trees = Instantiate(treesprefab, transform);
        trees.transform.position = tilebase.transform.position + Vector3.up * 1.75f - Vector3.right * 5f - Vector3.forward * 5f;
        trees.SetActive(false);

        for (int i = 0; i < tilebase.transform.childCount; i++)
        {
            childObjects.Add(tilebase.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < childObjects.Count; i++)
        {
            originalMats.Add(childObjects[i].GetComponent<MeshRenderer>().sharedMaterials);
        }
        SwitchColor(Overgrown.GameEnums.CellState.Empty);

        return tilebase.GetComponent<MeshFilter>().sharedMesh;
    }


    public void SetID(int _ID)
    {
        unique_ID = _ID;
    }

    //Input Related Methods to be removed
    private void OnMouseEnter()
    {
        if (!enableHoverColoring)
            return;
        if (cellState == CellState.Empty || cellState == CellState.Crossed)
        {
            tilebase.GetComponent<MeshRenderer>().sharedMaterial = green[0];
        }
        else
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = green;
            }
        }
    }

    private void OnMouseExit()
    {
        if (!enableHoverColoring)
            return;
        if (cellState == CellState.Empty || cellState == CellState.Crossed)
        {
            tilebase.GetComponent<MeshRenderer>().sharedMaterial = tilebasemat;
        }
        else
        {
            SwitchColor(cellState);
        }
    }

    public void SwitchColor(CellState state)
    {
        cellState = state;
        if (state == Overgrown.GameEnums.CellState.Crossed)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                tilebase.GetComponent<MeshRenderer>().sharedMaterial = tilebasemat;
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = red;
            }
        }
        else if (state == Overgrown.GameEnums.CellState.Empty)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                tilebase.GetComponent<MeshRenderer>().sharedMaterial = tilebasemat;
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = grey;
            }
        }
        else if (state == Overgrown.GameEnums.CellState.Filled)
        {
            for (int i = 0; i < childObjects.Count; i++)
            {
                tilebase.GetComponent<MeshRenderer>().sharedMaterial = tilebasemat;
                childObjects[i].GetComponent<MeshRenderer>().sharedMaterials = originalMats[i];
            }
        }
    }

    public void EnableTrees()
    {
        if (cellState == Overgrown.GameEnums.CellState.Crossed || cellState == Overgrown.GameEnums.CellState.Empty)
        {
            trees.SetActive(true);
        }
    }
    public void DisableTrees()
    {
        if (cellState == Overgrown.GameEnums.CellState.Crossed || cellState == Overgrown.GameEnums.CellState.Empty)
        {
            trees.SetActive(true);
        }
    }


    public void TileState(bool isActive)
    {
        for (int i = 0; i < childObjects.Count; i++)
        {
            childObjects[i].SetActive(isActive);
        }
    }

    public void CrossState(bool isActive)
    {
        cross.SetActive(isActive);
    }

    public void SwitchState(CellState state)
    {
        cellState = state;
    }
}