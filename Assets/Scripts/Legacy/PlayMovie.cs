using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class PlayMovie : MonoBehaviour
{
    public VideoPlayer movie;
    [SerializeField]Material mat;

    void Start()
    {
        mat.mainTexture = movie.texture;
    }
}
