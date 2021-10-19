using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableBasedSoundEffect : SoundEffect
{
    public bool conditionVariable;

    public int minSeconds;
    public int maxSeconds;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(SoundCoroutine());
    }

    protected override IEnumerator SoundCoroutine()
    {
        while(true)
        {
            if (conditionVariable)
            {
                ExecuteSound();
            }
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
        }
    }

    protected override void ExecuteSound()
    {
        audioToPlay.Play();
    }
}
