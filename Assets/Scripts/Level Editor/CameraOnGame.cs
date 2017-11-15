using UnityEngine;
using System.Collections;

public class CameraOnGame : BeatReactor
{
    public static float y_pos
    { get { return instance.transform.position.y; } }
    public static float speed
    { get { return instance.camSpeed; } }
    static CameraOnGame instance;

    Camera cam;
    
    public float camOffset = 0.0F;
    public float camSpeed;
    public bool canMove = false;
    Vector3 pos;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
        pos = transform.position;
    }

    protected override void Start()
    {
        base.Start();
        pos.y = camOffset;
        transform.position = pos;
    }

    protected override void OnTimeChecker(float delta)
    {
        if(!canMove)
        {
            return;
        }

        try
        {
            pos.y += delta * camSpeed;
            instance.transform.position = pos;
        }
        catch(MissingReferenceException)
        {
            print("I CAUGHT YOU BIIIIITCH!");
            instance = FindObjectOfType<CameraOnGame>();
        }

    }
}
