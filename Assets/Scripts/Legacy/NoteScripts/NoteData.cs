using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Path = System.IO.Path;

[System.Serializable]
[XmlInclude(typeof(NoteData))]
public class NoteData : IComparable<NoteData>
{

    /* 0 = key;
     * 1 = time;
     * 2 = xPos;
     * 3 = length;
     */
    [XmlArrayItem("data")] public float[] data = new float[4];
    [XmlAttribute("id")] public int id = 1000;

    public int CompareTo(NoteData other)
    {
        if(this.data[1] > other.data[1])
        {
            return 1;
        }
        else if(this.data[1] < other.data[1])
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
