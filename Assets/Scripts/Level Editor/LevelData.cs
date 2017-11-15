using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LevelEditor;
using System.Xml;
using System.Xml.Serialization;
using Path = System.IO.Path;

public class LevelData : MonoBehaviour
{
    public static float editorSpeed
    {
        get {
            if (!instance)
                return 0.0F;
            else
            {
                return instance.speed;
            }
        }
    }

    static LevelData instance;
    public GameObject noteToInstantiate;
    public float speed = 3F;
    
    NoteContainer container;
    public NoteCreator[] creators;
    [SerializeField] string _fileName;
    public string fileName
    { get { return _fileName + ".xml"; } }

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < creators.Length; i++)
        {
            creators[i].OnCreated += OnNoteCreated;
        }
    }

    private void Start()
    {
        if(!LoadLevelData())
        {
            print("CANT LOAD LEVEL");
            container = new NoteContainer();
        }
    }

    void OnNoteCreated(NoteOnLevelEditor note)
    {
        container.notes.Add(note.data);
        note.data.id += container.notes.Count;
        note.OnDestroyed += OnNoteDestroyed;
    }
    void OnNoteDestroyed(int noteId)
    {
        for (int i = 0; i < container.notes.Count; i ++)
        {
            if (container.notes[i].id == noteId)
            {
                container.notes.Remove(container.notes[i]);
                container.notes.TrimExcess();
                break;
            }
        }
    }
    void OnNoteValueChanged(int id, int index, float value)
    {
        for (int i = 0; i < container.notes.Count; i++)
        {
            if (container.notes[i].id == id)
            {
                container.notes[i].data[index] = value;
                break;
            }
        }
    }

    public void SaveLevelData()
    {
        string path = Application.streamingAssetsPath + "/LevelData/" + fileName;
        container.notes.Sort();
        container.Save(path);
        print("FILE IS SAVED");
    }

    public bool LoadLevelData()
    {
        string path = Application.streamingAssetsPath + "/LevelData/" + fileName;
        if (!System.IO.File.Exists(path))
        { return false; }

        container = NoteContainer.Load(path);

        InstantiateNotes(container.notes);

        return true;
    }

    void InstantiateNotes(List<NoteData> notes)
    {
        GameObject obj;
        Vector3 pos = new Vector3();
        NoteOnLevelEditor noteOnEditor;
        pos.z = noteToInstantiate.transform.position.z;
        Transform parent = GameObject.Find("Notes").transform;

        foreach(NoteData data in notes)
        {
            pos.x = data.data[2];
            pos.y = data.data[1] * speed;
            obj = Instantiate(noteToInstantiate, pos, Quaternion.identity);
            obj.transform.parent = parent;
            noteOnEditor = obj.GetComponent<NoteOnLevelEditor>();
            noteOnEditor.type = (NoteType)data.data[0];
            noteOnEditor.cretedTime = data.data[1];
            noteOnEditor.xPosition = data.data[2];
            noteOnEditor.duration = data.data[3];
            noteOnEditor.id = data.id;
            noteOnEditor.OnValueChanged += OnNoteValueChanged;
            noteOnEditor.OnDestroyed += OnNoteDestroyed;
        }
    }
}
