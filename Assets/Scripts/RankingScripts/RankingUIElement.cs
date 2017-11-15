using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUIElement : MonoBehaviour
{
    [SerializeField] Text initialsText;
    [SerializeField] Text valueText;

    public void SetInitial(char[] initials)
    {
        initialsText.text = initials[0].ToString() + "."
            + initials[1].ToString() + "." + initials[2].ToString() + ":";
    }

    public void SetRecordValue(int value)
    {
        valueText.text = value.ToString("000000");
    }
}
