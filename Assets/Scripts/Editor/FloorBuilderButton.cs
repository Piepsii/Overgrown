using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile))]
public class FloorBuilderButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Tile myScript = (Tile)target;
        if (GUILayout.Button("BuildFloors"))
        {
            Debug.ClearDeveloperConsole();
            myScript.BuildFloors();
        }
        if (GUILayout.Button("DeleteFloors"))
        {
            Debug.ClearDeveloperConsole();
            myScript.DeleteFloors();
        }
    }
}
