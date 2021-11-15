using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Graph))]
public class GraphEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Graph myTarget = (Graph)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Show Graph"))
        {
            myTarget.ClearGraph();
            myTarget.MakeGraph();
        }
    }
}
