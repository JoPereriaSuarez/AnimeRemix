using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSong : MonoBehaviour
{
    public float time = 15F;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(StartAlarm(time));
        }
    }

    IEnumerator StartAlarm(float t)
    {
        Alarm alarm = gameObject.AddComponent<Alarm>();
        alarm.Initialize(t);

        yield return new WaitUntil(() => alarm.isReady == true);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPaused = true;
#endif 
    }
}
