using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelDataOnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelData controller = (LevelData)target;
        if(GUILayout.Button("SAVE LEVEL DATA"))
        {
            controller.SaveLevelData();
        }
    }
}
