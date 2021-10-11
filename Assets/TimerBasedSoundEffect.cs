using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBasedSoundEffect : SoundEffect
{
    public float minTime;
    public float maxTime;

    private void Start()
    {
        Init();

        if (repeating)
        {
            StartCoroutine(SoundCoroutine());
        }
        else
        {
            secondsToPlay = CalculateSecondsToPlay();
            audioToPlay.PlayDelayed(secondsToPlay);
        }
    }
    protected float CalculateSecondsToPlay()
    {
        return Random.Range(minTime, maxTime);
    }

    protected override void ExecuteSound()
    {
        audioToPlay.Play();
        secondsToPlay = CalculateSecondsToPlay();
    }

    protected override IEnumerator SoundCoroutine()
    {
        while(true)
        {
            ExecuteSound();
            yield return new WaitForSeconds(secondsToPlay);
        }
    }
}
