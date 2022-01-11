using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Puzzle))]
public class PuzzleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Puzzle myScript = (Puzzle)target;
        if (GUILayout.Button("New Puzzle"))
        {
            Debug.ClearDeveloperConsole();
            myScript.NewPuzzle();
        }
    }
}
