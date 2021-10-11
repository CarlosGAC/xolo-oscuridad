using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityBasedSoundEffect : SoundEffect
{
    public List<AudioClip> clips;

    [SerializeField]
    private int nextClipPosition;

    private void Start()
    {
        Init();
        StartCoroutine(SoundCoroutine());
    }
    protected override void ExecuteSound()
    {
        nextClipPosition = Random.Range(0, clips.Count - 1);
        audioToPlay.clip = clips[nextClipPosition];
        audioToPlay.Play();
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
