using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class CameraLevelEditor : BeatReactor
    {
        Camera cam;

        public float camOffset = 0.0F;
        float camSpeed;
        public float songTime
        {
            get { return AudioTracker.songTime; }
        }
        AudioTracker audio_tracker;
        Vector3 pos = new Vector3(0, 0, -10F);

        private void Awake()
        {
            cam = Camera.main;
            audio_tracker = FindObjectOfType<AudioTracker>();
            pos = transform.position;
        }

        protected override void Start()
        {
            base.Start();
            camSpeed = 5;
        }

        protected override void OnTimeChecker(float delta)
        {               
            pos.y = (songTime * camSpeed) + camOffset;
            transform.position = pos;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
                if(hit.transform == null)
                { return; }

#if (UNITY_EDITOR)
                UnityEditor.Selection.objects = new UnityEngine.Object[1] { hit.transform.gameObject as UnityEngine.Object };
#endif   
            }
        }
    }
}
