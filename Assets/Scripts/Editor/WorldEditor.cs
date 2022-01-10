using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class WorldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Grid myScript = (Grid)target;
        if (GUILayout.Button("BuildGrid"))
        {
            Debug.ClearDeveloperConsole();
            myScript.CreateGrid();
        }
        if (GUILayout.Button("DeleteGrid"))
        {
            Debug.ClearDeveloperConsole();
            myScript.DeleteGrid();
        }
    }
}

