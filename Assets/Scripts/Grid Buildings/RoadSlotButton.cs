using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoadSlot))]
public class RoadSlotButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoadSlot myScript = (RoadSlot)target;
        if (GUILayout.Button("Build Road"))
        {
            myScript.BuildRoad();
        }
        if (GUILayout.Button("Reset Road"))
        {
            myScript.ResetRoad();
        }
    }
}
