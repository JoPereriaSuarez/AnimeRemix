using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelEditor
{
    public class NoteOnLevelEditor : MonoBehaviour
    {
        public delegate void OnDestroyEventHandler(int noteId);
        public event OnDestroyEventHandler OnDestroyed;
        public delegate void OnValueChangeEventHandler(int id, int index, float value);
        public event OnValueChangeEventHandler OnValueChanged;

        [HideInInspector]public NoteData data;

        LineRenderer l_rend;

        public int id
        {
            get { return data.id; }
            set { data.id = value; }
        }

        public NoteType type
        {
            get { return (NoteType)data.data[0]; }
            set
            {
                data.data[0] = (int)value;
                if(OnValueChanged!=null)
                { OnValueChanged(id,0, (int)value); }

                string spr_name;
                SpriteRenderer rend = GetComponent<SpriteRenderer>();
                switch (value)
                {
                    case NoteType.green:
                    spr_name = "ButtonSprites/GreenButton";
                    rend.color = Color.white;
                    break;

                    case NoteType.blue:
                    spr_name = "ButtonSprites/BlueButton";
                    rend.color = Color.white;
                    break;

                    case NoteType.yellow:
                    spr_name = "ButtonSprites/YellowButton";
                    rend.color = Color.white;
                    break;

                    case NoteType.red:
                    spr_name = "ButtonSprites/RedButton";
                    rend.color = Color.white;
                    break;

                    case NoteType.right:
                    spr_name = "ButtonSprites/RightButton";
                    rend.color = Color.white;
                    break;

                    case NoteType.left:
                    spr_name = "ButtonSprites/LeftButton";
                    rend.color = Color.white;
                    break;

                    case NoteType.up:
                    spr_name = "ButtonSprites/UpButton";
                    rend.color = Color.white;
                    break;

                    case NoteType.down:
                    spr_name = "ButtonSprites/DownButton";
                    rend.color = Color.white;
                    break;

                    default:
                        spr_name = "ButtonSprites/DefaultButton";
                        rend.color = Color.black;
                        break;
                }
                Sprite spr = Resources.Load(spr_name, typeof(Sprite)) as Sprite;
                rend.sprite = spr;                
            }
        }
        public float cretedTime
        {
            get { return data.data[1]; }
            set
            {
                data.data[1] = value;
                if (OnValueChanged != null)
                { OnValueChanged(id,1, value); }
                Vector3 pos = transform.position;
                pos.y = value * LevelData.editorSpeed;
                transform.position = pos;
            }
        }
        public float xPosition
        {
            get { return data.data[2]; }
            set
            {
                Vector3 pos = transform.position;
                pos.x = value;
                transform.position = pos;
                data.data[2] = value;
                if (OnValueChanged != null)
                { OnValueChanged(id, 2, value); }
            }
        }
        public float duration
        {
            get { return data.data[3]; }
            set
            {
                value = Mathf.Clamp(value, 0.02F, 90F);
                data.data[3] = value;
                if (OnValueChanged != null)
                { OnValueChanged(id, 3, value); }
                if (l_rend)
                {
                    l_rend.SetPosition(1, new Vector3(0, value * LevelData.editorSpeed, 0));
                }

            }
        }

        private void Awake()
        {
            data = new NoteData();
            l_rend = GetComponent<LineRenderer>();
        }

        private void OnDestroy()
        {
            if(OnDestroyed != null)
            {
                
                OnDestroyed(id);
            }
        }
    }
}
