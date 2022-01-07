using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoadSlot : MonoBehaviour
{
    public Material roadMat;
    public Material def;
    int layerMask = 1 << 6;

    public void BuildRoad()
    {
        GetComponent<Renderer>().material = roadMat;
    } 
    
    public void ResetRoad()
    {
        GetComponent<Renderer>().material = def;
    }

    
}
