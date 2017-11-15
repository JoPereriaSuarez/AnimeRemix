using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using Path = System.IO.Path;

[System.Serializable]
[XmlInclude(typeof(RankingSongContainer))]
public class RankingSongContainer
{
    public List<RankingData> data = new List<RankingData>();

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(RankingSongContainer));
        var encoding = Encoding.GetEncoding("UTF-8");
        XmlSerializerNamespaces nameSpaces = new XmlSerializerNamespaces();
        using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
        {
            serializer.Serialize(file, this as RankingSongContainer); //, false, encoding);
        }
    }

    public static RankingSongContainer LoadRaking(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(RankingSongContainer));
        using (FileStream file = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(file) as RankingSongContainer;
        }
    }
}
