using UnityEngine;
using System.Collections;

// Simplemente hace algo cuando a transcurrido 1 beat
public abstract class BeatReactor : MonoBehaviour
{
    protected virtual void Start()
    {
        BeatTracker.OnTimeChanged += OnTimeChecker;
    }

    protected abstract void OnTimeChecker(float delta);
}
