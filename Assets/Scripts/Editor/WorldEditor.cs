using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomGrid))]
public class WorldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CustomGrid myScript = (CustomGrid)target;
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
