using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameModel))]
public class GameModelOnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameModel controller = (GameModel)target;
        if(GUILayout.Button("SAVE RANKING"))
        {
            controller.SaveRanking();
        }
        else if (GUILayout.Button("LOAD RANKING"))
        {
            controller.LoadRanking();
        }
        else if (GUILayout.Button("SAVE SCORE"))
        {
            controller.AddScoreToRanking();
        }
    }
}