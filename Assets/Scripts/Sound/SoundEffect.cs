using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class SoundEffect : MonoBehaviour
{
    public bool repeating;

    [SerializeField]
    protected float secondsToPlay;

    protected AudioSource audioToPlay;

    protected void Init()
    {
        audioToPlay = GetComponent<AudioSource>();
    }

    protected void StartSoundRoutine()
    {

    }

    protected abstract IEnumerator SoundCoroutine();

    protected abstract void ExecuteSound();
}
