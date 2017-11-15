using UnityEngine;
using UnityEditor;


namespace LevelEditor
{
    [CustomEditor(typeof(NoteOnLevelEditor))]
    public class NoteOnLevelEditorOnEditor : Editor
    {
        NoteOnLevelEditor controller;
        float createTime;
        float duration;
        float xPos;
        int id;
        NoteType type;

        void OnEnable()
        {
            controller = (NoteOnLevelEditor)target;
            createTime = controller.cretedTime;
            duration = controller.duration;
            type = controller.type;
            xPos = controller.xPosition;
        }

        public override void OnInspectorGUI()
        {
            createTime =  EditorGUILayout.DelayedFloatField("Time in Song ", createTime);
            duration = EditorGUILayout.DelayedFloatField("Length in Seconds ", duration);
            //type = (NoteType)EditorGUILayout.EnumPopup("Type", type);
            xPos = EditorGUILayout.DelayedFloatField("TrackBar ", xPos);
            

            if (GUI.changed)
            {
                controller.cretedTime = createTime;
                controller.duration = duration;                
                controller.xPosition = xPos;
                controller.type = (NoteType)xPos;
            }
        }
    }

}