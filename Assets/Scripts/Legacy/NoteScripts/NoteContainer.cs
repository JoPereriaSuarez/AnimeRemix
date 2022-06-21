using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using Path = System.IO.Path;

[XmlInclude(typeof(NoteContainer))]
[System.Serializable]
public class NoteContainer
{
    public List<NoteData> notes = new List<NoteData>();

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(NoteContainer));
        FileStream file = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
        using (file )
        {
            serializer.Serialize(file, this as NoteContainer);
        }
    }

    public static NoteContainer Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(NoteContainer));
        using(FileStream file = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(file) as NoteContainer;
        }
    }
}
