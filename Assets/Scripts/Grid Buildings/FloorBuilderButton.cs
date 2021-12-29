using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Building))]
public class FloorBuilderButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Building myScript = (Building)target;
        if (GUILayout.Button("BuildFloors"))
        {
            Debug.ClearDeveloperConsole();
            myScript.BuildFloors();
        }
    }
}
