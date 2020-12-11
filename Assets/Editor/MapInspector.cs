using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapCreator))]
public class MapInspector : Editor
{
    public override void OnInspectorGUI()
    {
        MapCreator mapCreator = (MapCreator)target;

        base.OnInspectorGUI();
        if (GUILayout.Button("RefreshMap"))
        {
            mapCreator.RefreshMap();
        }
    }
}
