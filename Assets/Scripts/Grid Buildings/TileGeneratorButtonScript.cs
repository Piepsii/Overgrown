using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileGenerator))]
public class TileGeneratorButtonScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TileGenerator myScript = (TileGenerator)target;
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
