using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/* Determina si el NoteSpawner o el AudioTracker deberia de reproducirse primero.
 * Inicializa el Spawner y el AudioTracker en el momento determinado
 * Funciones globales del juego
 * Determina el puntaje y lo coteja con el ranking
 */
 [RequireComponent(typeof(GameModel))]
public class GameController : MonoBehaviour
{
    public static GameController instance;

    GameModel model;
    [SerializeField] AudioTracker audio_tracker;
    [SerializeField] Spawner[] spawners;
    [SerializeField] CameraOnGame cam;
    [SerializeField] Text scoreText;

    [SerializeField] GameObject background;
    Image backgroundImage;
    RectTransform backgroundScale;
    public bool isSongOver = false;
    public float timeToSkip = 98F;

    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(GameController.instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        model = GetComponent<GameModel>();
    }

    public void LoadLevel(string levelName)
    {
        //CameraOnGame.instance = null;
        SceneLoader.instance.StartLoading(levelName);
    }

    public float offset = 0.13F;

    private void Start()
    {
        if (cam == null)
        {
            print("CAN HAS BEEN DESTROYED");
            cam = FindObjectOfType<CameraOnGame>();
        }
        
        Image im = background.GetComponent<Image>();
        model.actualSongScore = 0;
        RectTransform rect = background.GetComponent<RectTransform>();
        if(im != null && rect != null)
        {
            backgroundImage = im;
            backgroundScale = rect;
        }
        if (scoreText != null)
        { scoreText.text = model.actualSongScore.ToString("0000000"); }

        Invoke("StartGame", 2F);
    }

    void StartGame()
    {
        cam.canMove = true;
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i]._start = true;
            isSongOver = false;
        }
        StartCoroutine(StartAlarm(2.453331F - 0.76F + offset));
    }

    IEnumerator StartAlarm(float t)
    {
        Alarm alarm = gameObject.AddComponent<Alarm>();
        alarm.Initialize(t);

        yield return new WaitUntil(() => alarm.isReady == true);

        audio_tracker.PlayResumeAudio();

        Invoke("StopAtEndSong", timeToSkip);
    }

    void StopAtEndSong()
    {
        isSongOver = true;
        cam.camSpeed = 0.0F;

        string levelName = "";

        if (model.CanAddScoreToRanking())
        {
            levelName = "LoginPlayer";
        }
        else
        {
            levelName = "RankingScream";
            PlayerPrefs.SetString("RankingFile", model.fileName);
        }

        StartCoroutine(WaitToEndLevel(1F, levelName));
    }

    void PlayAudio()
    {
        audio_tracker.PlayResumeAudio();
    }

    private void Update()
    {/*
        if(AudioTracker.instance != null)
        {
            
            if (AudioTracker.songTime >= audio_tracker.GetClipLength() && !isSongOver)
            {
                print("TERMINO");
                isSongOver = true;
                cam.camSpeed = 0.0F;

                string levelName = "";

                if (model.CanAddScoreToRanking())
                {
                    levelName = "LoginPlayer";
                }
                else
                {
                    levelName = "RankingScream";
                    PlayerPrefs.SetString("RankingFile", model.fileName);
                }

                StartCoroutine(WaitToEndLevel(3F, levelName));
            }
            
        }
        */

    }
    
    IEnumerator WaitToEndLevel(float t, string level)
    {
        yield return new WaitForSeconds(t);

        LoadLevel(level);
    }

    // SCORES METHODS
    public int GetCurrentScore()
    {
        return model.actualSongScore;
    }
    public void AddSongScore (int value)
    {
        model.actualSongScore += value;
        if (scoreText != null)
        { scoreText.text = model.actualSongScore.ToString("000000"); }
    }

    public void LogPlayer(char[] initials)
    {
        model.temp_initials = initials;
        model.AddScoreToRanking();

        LoadLevel("RankingScream");
    }

}