using UnityEngine;
using System.Collections;
using System;

public class NormalNote : MonoBehaviour
{
    GameController g_controller;

    const float timeToPress = 0.6F;
    public bool canPressed { get; private set; }

    public MyInputButton button;
    public NoteDurationType duration = NoteDurationType.singleNote;
    public NoteType noteInput;
    public InputState inputState = InputState.idle;
    public Transform parent;
    BoxCollider2D col;
    SpriteRenderer rend;
    public LineRenderer l_rend { get; private set; }
    bool isPressed = false;

    Transform p;
    LightController l_coontroller;

    float timeAlarmCheck = 0.0F;
    bool callAlarm = false;
    ScoreValues noteScore = ScoreValues.None;
    LightIntensity l_intensity = LightIntensity.disable;

    private void Awake()
    {
        l_rend = GetComponent<LineRenderer>();
        col = GetComponent<BoxCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        p = GameObject.Find("Notes").transform;
    }

    private void Start()
    {
        g_controller = GameController.instance;
    }

    public void Initialize()
    {
        canPressed = true;
        col.enabled = true;
        rend.enabled = true;
        l_rend.enabled = true;

        Color cl = rend.color;
        cl.a = 1F;
        rend.color = cl;

        cl = l_rend.startColor;
        cl.a = 1F;
        l_rend.startColor = cl;

        cl = l_rend.endColor;
        cl.a = 1F;
        l_rend.endColor = cl;

        callAlarm = false;
        timeAlarmCheck = 0.0F;
    }
    private void Update()
    {
        if(button != null)
        {
            button.CheckInputState(ref inputState);
        }

        if(callAlarm)
        {
            timeAlarmCheck += Time.deltaTime;
            if(timeAlarmCheck >= timeToPress)
            {
                CancelPress();
            }
        }
    }

    ScoreValues CheckForScore(float valueToCheck)
    {
        ScoreValues value = ScoreValues.None;

        if (valueToCheck >= 0.00F && valueToCheck <= 0.200F)
        {
            value = ScoreValues.MaxPerfect;
        }
        else if(valueToCheck > 0.200F && valueToCheck <= 0.350F)
        {
            value = ScoreValues.Perfect;
        }
        else if(valueToCheck > 0.350F && valueToCheck <= 0.70F)
        {
            value = ScoreValues.Great;
        }
        else if(valueToCheck > 0.700F && valueToCheck <= 0.800F)
        {
            value = ScoreValues.Bad;
        }
        else if(valueToCheck > 0.800F)
        {
            value = ScoreValues.Terrible;
        }

        return value;
    }
    LightIntensity CheckLightFromScore(ScoreValues score)
    {
        LightIntensity value = LightIntensity.disable;

        switch (score)
        {
            case ScoreValues.MaxPerfect:
            value = LightIntensity.maxPerfect;
            break;

            case ScoreValues.Perfect:
            value = LightIntensity.perfect;
            break;

            case ScoreValues.Great:
            value = LightIntensity.great;
            break;

            case ScoreValues.Bad:
            value = LightIntensity.bad;
            break;

            case ScoreValues.Terrible:
            value = LightIntensity.terrible;
            break;
        }

        return value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        button = new MyInputButton(noteInput);
        l_coontroller = collision.GetComponent<LightController>();

        callAlarm = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!canPressed)
        { return; }

        if (inputState == InputState.down)
        {
            float distanceToParent = Mathf.Abs(parent.transform.position.y - transform.position.y);

            noteScore = CheckForScore(distanceToParent);
            l_intensity = CheckLightFromScore(noteScore);
 //           print(noteScore.ToString() + " on " +  distanceToParent);

            callAlarm = false;
            timeAlarmCheck = 0.0F;
            isPressed = true;
            if(duration == NoteDurationType.singleNote)
            {
                if (l_coontroller != null)
                {
                    l_coontroller.isInCollision = true;
                    l_coontroller.ChangeLightIntensity(l_intensity);
                }
                OnPressed();
            }
        }
        else if(inputState == InputState.up)
        {
            transform.parent = null;
            button = null;
            inputState = InputState.idle;
            l_intensity = LightIntensity.disable;
            l_coontroller.ChangeLightIntensity(l_intensity);
        }
        else if(duration == NoteDurationType.longNote && isPressed && inputState == InputState.hold)
        {
            if(l_coontroller != null)
            {
                l_coontroller.isInCollision = true;
            }
            OnPressed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LightController l_coontroller = collision.GetComponent<LightController>();
        Invoke("BackToPool", 1.2F);
    }

    void BackToPool()
    {
        CancelInvoke();
        transform.parent = p;
        button = null;
        inputState = InputState.idle;
        col.enabled = false;
        l_rend.enabled = false;
        rend.enabled = false;
        transform.position = new Vector2(-100, -100);
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

    void CancelPress()
    {
        Color cl = rend.color;
        Color l_color = l_rend.startColor;
        cl.a = 0.4F;
        l_color.a = 0.4F;

        rend.color = cl;
        l_rend.startColor = l_color;
        l_rend.endColor = l_color;       
    }

    Vector3 pos;
    Vector2 col_offset = new Vector2(0, 0);
    Vector2 col_size = new Vector2(1, 0);
    void OnPressed()
    {
        if(transform.parent != parent)
        {
            transform.parent = parent;
        }
        transform.localPosition = Vector2.zero;

        if(duration == NoteDurationType.longNote)
        {
            pos = l_rend.GetPosition(1);
            pos.y -= Time.deltaTime * CameraOnGame.speed;
            l_rend.SetPosition(1, pos);

            if (pos.y > 0.23F)
            {
                int score = Mathf.RoundToInt((float)noteScore * ( Time.deltaTime * 3.5F ));
                g_controller.AddSongScore(score);

                float offset = ( pos.y / 2 ) - 0.25F;
                col_size.y = pos.y;
                col_offset.y = offset;
                col.size = col_size;
                col.offset = col_offset;
            }
            else
            {
                col.enabled = false;
                StartCoroutine(WaitToTurnOffLight());
            }
        }
        else
        {
            g_controller.AddSongScore((int)noteScore);
            col.enabled = false;
            StartCoroutine(WaitToTurnOffLight());
        }
    }

    IEnumerator WaitToTurnOffLight()
    {
        yield return new WaitForSeconds(0.5F);
        if(l_coontroller != null)
        {
            l_coontroller.ChangeLightIntensity(LightIntensity.disable);
            l_coontroller.isInCollision = false;
        }
    }
}

[System.Serializable]
public class MyInputButton
{
    public  NoteType type;
     public string axis;
    public int sign;

    public MyInputButton(NoteType type)
    {
        this.type = type;
        switch (type)
        {
            case NoteType.blue:
            axis = "BlueButton";
            sign = 1;
            break;

            case NoteType.red:
            axis = "RedButton";
            sign = 1;
            break;

            case NoteType.yellow:
            axis = "YellowButton";
            sign = 1;
            break;

            case NoteType.green:
            axis = "GreenButton";
            sign = 1;
            break;

            case NoteType.right:
            axis = "Vertical";
            sign = 1;
            break;

            case NoteType.left:
            axis = "Vertical";
            sign = -1;
            break;

            case NoteType.up:
            axis = "Horizontal";
            sign = 1;
            break;
            case NoteType.down:
            axis = "Horizontal";
            sign = -1;
            break;
        }
    }

    public int CheckInput()
    {
        float value = Input.GetAxis(axis);
        if (value != 0.0F && Mathf.Sign(value) == sign)
        {
            return 1;
        }

        return 0;
    }

    public InputState CheckInputState(ref InputState state)
    {
        if(axis == "" || axis == null)
        { return InputState.idle;  }
        int noteValue = CheckInput();
        switch (state)
        {
            case ( InputState.idle ):
            if (noteValue != 0.0F)
            {
                state = InputState.down;
            }
            break;

            case ( InputState.down ):
            state = InputState.hold;
            break;

            case ( InputState.hold ):
            if (noteValue == 0)
            {
                state = InputState.up;
            }
            break;

            case InputState.up:
            state = InputState.idle;
            break;
        }

        return state;
    }
}

public enum NoteDurationType
{
    singleNote, 
    longNote,
}
public enum InputState
{
    idle,
    down,
    hold,
    up,
}
