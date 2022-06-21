using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    string scene;
    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator LoadCustomScene(string scene_name)
    {
        SceneManager.LoadScene("SceneLoader", LoadSceneMode.Single);

        yield return new WaitForSecondsRealtime(1);
        AsyncOperation async_op = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Single);

        while (!async_op.isDone)
        {
            yield return null;
        }
    }

    public void StartLoading(string scene)
    {
        this.scene = scene;
        StartCoroutine(LoadCustomScene(scene));
    }
}
