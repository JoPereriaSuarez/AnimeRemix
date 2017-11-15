using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Path = System.IO.Path;

[System.Serializable]
[XmlInclude(typeof(RankingData))]
public class RankingData : IComparable<RankingData>
{
    [XmlArrayItem("initial")] public char[] initials = new char[3];
    [XmlAttribute("value")] public int value;

    public RankingData()
    {

    }

    public RankingData(char[] initial, int rankingValue)
    {
        initials = initial;
        value = rankingValue;
    }

    public int CompareTo(RankingData other)
    {
        if(this.value > other.value)
        {
            return -1;
        }
        else if (this.value < other.value)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
