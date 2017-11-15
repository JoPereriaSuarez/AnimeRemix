using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Contiene los datos "in-game"
public class GameModel : MonoBehaviour
{
    // Puntaje de la cancion actual
    // Se resetea cuando el valor el 0
    public int actualSongScore
    {
        get { return PlayerPrefs.GetInt("Score", 0); }
        set
        {
            if(value == 0)
            {
                PlayerPrefs.DeleteKey("Score");
                return;
            }
            PlayerPrefs.SetInt("Score", value);
        }
    }
    
    [SerializeField] string _fileName = "songLevel";
    public char[] temp_initials = new char[3];
    [SerializeField] int[] defaultScores = new int[10];

    public string fileName
    {
        get { return _fileName; }
    }
    string path
    {
        get { return Application.streamingAssetsPath + "/RankingsData/" + _fileName + ".xml"; }
    }
    public RankingSongContainer ranking;

    public void Start()
    {
        actualSongScore = 0;
        PlayerPrefs.SetString("RankingFile", fileName);
        PlayerPrefs.SetString("GameLevel", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        print("SCENE NAME " + PlayerPrefs.GetString("GameLevel"));
        if (!LoadRanking())
        {
            ranking = new RankingSongContainer();
            for (int i = 0; i < defaultScores.Length; i++)
            {
                ranking.data.Add(new RankingData(temp_initials, defaultScores[i]));
            }
            SaveRanking();
        }
    }

    public bool LoadRanking()
    {
        if (!System.IO.File.Exists(path))
        { return false; }

        ranking = RankingSongContainer.LoadRaking(path);
        return true;
    }
    public void SaveRanking()
    {
        ranking.data.Sort();
        if(ranking.data.Count>10)
        {
            ranking.data.RemoveAt(10);
        }
        ranking.Save(path);
    }

    public bool CanAddScoreToRanking()
    {
        int index = -1;
        for (int i = 0; i < ranking.data.Count; i++)
        {
            if (actualSongScore > ranking.data[i].value)
            {
                index = i;
                break;
            }
        }
        if (index == -1)
        { return false; }

        return true;
    }

    public bool AddScoreToRanking()
    {
        int index = -1;
        for (int i = 0; i < ranking.data.Count; i++)
        {
            if(actualSongScore > ranking.data[i].value)
            {
                index = i;
                break;
            }
        }
        if(index == -1)
        { return false; }

        ranking.data.Add(new RankingData(temp_initials, actualSongScore));
        SaveRanking();
        return true;
    }
}

public enum ScoreValues
{
    MaxPerfect = 1000,
    Perfect   = 800,
    Great       = 500,
    Bad     = 300,
    Terrible = 100,
    None     = 0,
}
