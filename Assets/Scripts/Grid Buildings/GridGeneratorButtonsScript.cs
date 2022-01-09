using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorButtonsScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridGenerator myScript = (GridGenerator)target;
        if(GUILayout.Button("BuildGrid"))
        {
            Debug.ClearDeveloperConsole();
            myScript.CreateGrid();
        }
        if(GUILayout.Button("DeleteGrid"))
        {
            Debug.ClearDeveloperConsole();
            myScript.DeleteGrid();
        }
    }
}
