using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Spawner : BeatReactor
{
    [SerializeField] GameObject noteReference;
    List<NoteData> levelData;
    float songTime;
    public int cretorNumber = -8;
    [SerializeField] LayerMask layer;
    
    LoadData data;
    int index;
    [HideInInspector]public bool _start;
    public Transform buttonTransform;
    Vector3 notePos;

    private void Awake()
    {
        songTime = 0.0F;
        index = 0;
    }

    protected override void Start()
    {
        base.Start();
        levelData = new List<NoteData>();
        data = LoadData.instance;
        notePos = new Vector3();
        notePos.x = this.transform.position.x;
        notePos.z = 0F;
        for (int i = 0; i < data.notesData.Count; i++)
        {
            if (data.notesData[i].data[2] == cretorNumber)
            {
                levelData.Add(data.notesData[i]);
            }
        }
    }

    public void InstantiateNotes()
    {
        if (songTime >= levelData[index].data[1])
        {
            NormalNote note = data.RequestNote();
            note.gameObject.layer = (int)Mathf.Log(layer.value, 2);
            notePos.y = (levelData[index].data[1] + 0.2F) * 5F;
            note.transform.position = notePos;
            note.noteInput = (NoteType)levelData[index].data[0];
            note.parent = this.buttonTransform;
            note.Initialize();
            /* LISTA DE VALORES SPRITES
             * 0 = izquierda,
             * 1 = derecha,
             * 2 = arriba,
             * 3 = abajo,
             * 4 = azul,
             * 5 = rojo,
             * 6 = amarillo
             * 7 = verde
             */
            int spr_index = 7;
            switch (note.noteInput)
            {
                case NoteType.green:
                spr_index = 7;
                break;

                case NoteType.blue:
                spr_index = 4;
                break;

                case NoteType.yellow:
                spr_index = 6;
                break;

                case NoteType.red:
                spr_index = 5;
                break;

                case NoteType.right:
                spr_index = 1;
                break;

                case NoteType.left:
                spr_index = 0;
                break;

                case NoteType.up:
                spr_index = 2;
                break;

                case NoteType.down:
                spr_index = 3;
                break;
            }
            SpriteRenderer rend = note.gameObject.GetComponent<SpriteRenderer>();
            rend.sprite = data.buttonSprites[spr_index];
            rend.color = Color.white;
            note.l_rend.SetPosition(1, new Vector3(0, levelData[index].data[3] * 5F, 0));
            note.duration = ( levelData[index].data[3] > 0.1F ) ? NoteDurationType.longNote : NoteDurationType.singleNote;

                BoxCollider2D col = note.GetComponent<BoxCollider2D>();
                Vector2 size = new Vector2(1, (levelData[index].data[3] * 5F) + 0.5F);
                col.size = size;
                size.x = 0.0F;
                size.y = ( size.y / 2 ) - 0.5F;
                col.offset = size;
            
            index++;

        }
    }

    private void Update()
    {
        if (!_start)
        { return; }

        if (index < levelData.Count)
        {
            InstantiateNotes();
        }
    }

    protected override void OnTimeChecker(float delta)
    {
        if(!_start)
        { return; }
        songTime += delta;
    }
}
