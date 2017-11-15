using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/* Se encarga de crear las notas en la posicion y momento adecuado
 * segun el archivo .xml que corresponda
 */
public class NoteSpawner : MonoBehaviour
{
    public List<float> notesTime = new List<float>();
    List<float> notePos = new List<float>();
    List<float> duration = new List<float>();
    bool isReady;
    float time;
    int index;

    [SerializeField] string _fileName;
    public string fileName { get { return _fileName + ".xml"; } }

    public float noteTimeToEnd  = 6.015978F;
    public float noteSpeed      = 2.0F;
    public float startDelay     = 0.8F;
    public NormalNote note;
    float delay = 0.0F;

    const float baseSpeed = 1.002667F;
    const float maxDelay = -0.021304F;
    const float baseDelay = 0.03924395F;

    private void Awake()
    {
        string path = Application.streamingAssetsPath + "/LevelData/" + fileName;
        NoteContainer container = NoteContainer.Load(path);
        foreach(NoteData note in container.notes)
        {
            notesTime.Add(note.data[1]);
            notePos.Add(note.data[2]);
            duration.Add(note.data[3]);
        }

        notesTime.TrimExcess();
    }

    public void Initialize(bool useDelay = false)
    {
        isReady = true;
        time = 0F;
        if(useDelay)
        { delay = noteTimeToEnd; }
        else
        { delay = startDelay * -1; }
    }

    void Update()
    {
        if(!isReady)
        { return; }

        if(time >= notesTime[index] - delay)
        {
            NormalNote _note = Instantiate(note.gameObject, new Vector2(notePos[index], 8F), Quaternion.identity).GetComponent<NormalNote>();
            _note.l_rend.SetPosition(1, new Vector3(0, 0, duration[index]));
            print("SUPPUSE TO COLLIDE AT " + notesTime[index]);
            index++;
        }

        time += Time.deltaTime;
        if (index >= notesTime.Count - 1)
        {
            index = notesTime.Count - 1;
            isReady = false;
        }
    }

}
