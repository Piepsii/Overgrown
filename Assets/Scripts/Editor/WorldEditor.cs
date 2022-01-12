using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(World))]
public class WorldEditor : Editor
{
    [Range(0, 24)]
    public int mySlider;
    public override void OnInspectorGUI()
    {
        int no = 0;
        DrawDefaultInspector();
        EditorGUILayout.IntSlider(no, 0, 24);

        World myScript = (World)target;
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
        if (GUILayout.Button("SwitchColorFilled"))
        {
            Debug.ClearDeveloperConsole();
            myScript.SwitchColor(no, Overgrown.GameEnums.CellState.Filled);
        }
        if (GUILayout.Button("SwitchColorRed"))
        {
            Debug.ClearDeveloperConsole();
            myScript.SwitchColor(mySlider, Overgrown.GameEnums.CellState.Crossed);
        }
        if (GUILayout.Button("SwitchColorGrey"))
        {
            Debug.ClearDeveloperConsole();
            myScript.SwitchColor(mySlider, Overgrown.GameEnums.CellState.Empty);
        }
    
    }
}
