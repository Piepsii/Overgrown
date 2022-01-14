using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(World))]
public class WorldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        World myScript = (World)target;
        if (GUILayout.Button("BuildGrid"))
        {
            Debug.ClearDeveloperConsole();
            myScript.GenerateGrid(5, 5);
        }
        if (GUILayout.Button("DeleteGrid"))
        {
            Debug.ClearDeveloperConsole();
            myScript.DeleteGrid();
        } 
    }
}
