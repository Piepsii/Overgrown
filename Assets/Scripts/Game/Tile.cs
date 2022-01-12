using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    private int tile_row;
    private int tile_column;
    private int unique_ID;

    bool clickable;

    public List<GameObject> Tileprefab = new List<GameObject>();
    private List<GameObject> tiles = new List<GameObject>();

    private bool _checked;

    public void BuildBuilding() //To be changed to appropriate algorithm (probably get prefabs and instantiate them)
    {
        GameObject go = Instantiate(Tileprefab[Random.Range(0, Tileprefab.Count)], transform);
        go.transform.position = transform.position + Vector3.up * 0.0f + Vector3.right * 5f + Vector3.forward * 5f;
        tiles.Add(go);
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
    private void OnMouseExit()
    {
        if (!_checked)
        {
            GetComponent<Renderer>().material.color = Color.white;
            clickable = false;
        }

    }
    private void Update()
    {
        if (clickable)
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

            if (Input.GetMouseButtonDown(1))
            {
                if (!_checked)
                {
                    GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }
}