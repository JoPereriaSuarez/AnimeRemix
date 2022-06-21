using UnityEngine;
using System.Collections;
using System;

public class AscendingSongTimeComparer : IComparer
{
    public int Compare(object x, object y)
    {
        NoteData data1 = (NoteData)x;
        NoteData data2 = (NoteData)y;

        if(data1.data[1] > data2.data[1])
        {
            return 1;
        }
        else if (data1.data[1] < data2.data[1])
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public static IComparer SortBySongTime()
    {
        return (IComparer)new AscendingSongTimeComparer();
    }
}
