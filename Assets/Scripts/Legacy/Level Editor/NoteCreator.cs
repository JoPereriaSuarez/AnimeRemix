using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class NoteCreator : MonoBehaviour
    {
        public delegate void OnCreateEventHandler(NoteOnLevelEditor note);
        public event OnCreateEventHandler OnCreated;

        [SerializeField] NoteType _type;
        public NoteType type { get { return _type; } }
        [SerializeField] NoteOnLevelEditor note;
        [SerializeField] KeyCode createKey = KeyCode.A;
        bool isPressed = false;
        float timeToisPressed = 0.25F;
        float timeChecked = 0.0F;

        Transform parent;
        NoteOnLevelEditor obj;
        LineRenderer obj_rend;

        private void Awake()
        {
            parent = GameObject.Find("Notes").transform;
        }

        void Update()
        {
            if(Input.GetKeyDown(createKey))
            {
                obj = Instantiate(note.gameObject,
                    new Vector3(transform.position.x, transform.position.y, note.transform.position.z),
                    Quaternion.identity).GetComponent<NoteOnLevelEditor>();
                obj.transform.parent = parent;
                obj_rend = obj.gameObject.GetComponent<LineRenderer>();
                obj.cretedTime = AudioTracker.songTime;
                obj.xPosition = this.transform.position.x;
                obj.type = type;

                if(OnCreated != null)
                {
                    OnCreated(obj);
                }
            }

            if(Input.GetKey(createKey))
            {
                timeChecked += Time.deltaTime;
                if (timeChecked >= timeToisPressed && !isPressed)
                {
                    isPressed = true;
                }
                else if(isPressed)
                {
                    obj_rend.SetPosition(1, new Vector3(0, timeChecked * LevelData.editorSpeed, 0));
                    obj.duration = timeChecked;
                }
            }

            if (Input.GetKeyUp(createKey))
            {
                timeChecked = 0.0F;
                isPressed = false;
            }
        }
    }
}
