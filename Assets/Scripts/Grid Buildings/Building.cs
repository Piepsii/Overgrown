using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingSize
{
    None,
    OneFloor,
    TwoFloors,
    ThreeFloors
}
[ExecuteInEditMode]
public class Building : MonoBehaviour
{
    public BuildingSize size;
    public GameObject buildingprefab;

    public void BuildFloors()
    {
        if (size == BuildingSize.None)
        {
            DeleteFloors();
        }
        if (size == BuildingSize.OneFloor)
        {
            DeleteFloors();
            GameObject go = Instantiate(buildingprefab, transform);
            go.transform.position = transform.position + Vector3.up * 1.75f;
        }
        else if (size == BuildingSize.TwoFloors)
        {
            DeleteFloors();
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate(buildingprefab, transform);
                go.transform.position = transform.position + Vector3.up * (1.75f + (i * 3.5f));
            }
        }
        else if (size == BuildingSize.ThreeFloors)
        {
            DeleteFloors();
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(buildingprefab, transform);
                go.transform.position = transform.position + Vector3.up * (1.75f + (i * 3.5f));
            }
        }
    }

    void DeleteFloors()
    {
        while (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }

    public void GenerateBuilding(int num)
    {
        if(num == 0)
        {
            size = BuildingSize.OneFloor;
        }
        else if(num == 1)
        {
            size = BuildingSize.TwoFloors;
        }
        else
        {
            size = BuildingSize.ThreeFloors;
        }
        BuildFloors();
    }
}