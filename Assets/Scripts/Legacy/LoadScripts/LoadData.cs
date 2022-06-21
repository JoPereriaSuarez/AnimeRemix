using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Esta clase se encarga de cargar los datos del nivel,
 * asi como los sprites para los botones
 * Usar singleton para referencia
 */
public class LoadData : MonoBehaviour
{
    public static LoadData instance;

    // No; carga los datos de las notas si esta en el editor de niveles
    // eso lo hace LevelData
    public bool isInEditor = false;
    public List<NoteData> notesData { get; private set; }
    public List<NormalNote> notePooling { get; private set; }
    public int poolCapacity = 20;
    [SerializeField] GameObject noteReference;
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
    [SerializeField] Sprite[] _buttonSprites;
    public Sprite[] buttonSprites
    {
        get { return _buttonSprites; }
    }

    [SerializeField] string _fileName;
    public string fileName
    {
        get { return _fileName + ".xml"; }
    }

    private void Awake()
    {
        instance = this;

        if (isInEditor)
        { return; }

        if(!LoadLevelData())
        {
            Debug.LogError("NO EXISTE EL ARCHIVO " + fileName + "EN LA CARPETA STREAM ASSETS");
        }

        StartNotePooling();
    }

    public NormalNote RequestNote()
    {
        NormalNote note = null;
        for(int i = 0; i < notePooling.Count; i ++)
        {
            if(!notePooling[i].gameObject.activeSelf)
            {
                notePooling[i].gameObject.SetActive(true);
                note = notePooling[i].GetComponent<NormalNote>();
                break;
            }
        }

        if(note == null)
        {
            GameObject obj;
            Vector3 pos = new Vector3(100, 100, noteReference.transform.position.z);
            Transform p = GameObject.Find("Notes").transform;
            obj = Instantiate(noteReference, pos, Quaternion.identity);
            obj.transform.parent = p;
            obj.SetActive(true);
            poolCapacity++;
            notePooling.Add(obj.GetComponent<NormalNote>());
        }
        return note;
    }

    void StartNotePooling()
    {
        notePooling = new List<NormalNote>();
        Vector3 pos = new Vector3(-100, -100, noteReference.transform.position.z);
        Transform p = GameObject.Find("Notes").transform;
        for (int i = 0; i < poolCapacity; i++)
        {
            GameObject obj = Instantiate(noteReference, pos, Quaternion.identity);
            obj.transform.parent = p;
            obj.SetActive(false);
            notePooling.Add(obj.GetComponent<NormalNote>());
        }
    }

    bool LoadLevelData()
    {
        string path = Application.streamingAssetsPath + "/LevelData/" + fileName;
        if (!System.IO.File.Exists(path))
        { return false; }

        NoteContainer container = NoteContainer.Load(path);
        notesData = container.notes;

        return true;
    }

}
