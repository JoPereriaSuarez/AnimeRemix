using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour
{
    public MovieTexture movie;
    [SerializeField]Material mat;

    void Start()
    {
        mat.mainTexture = movie;
    }
}
