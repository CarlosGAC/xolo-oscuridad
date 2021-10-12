using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip itemGrabbed;
    [SerializeField]
    private AudioClip itemDropped;
    [SerializeField]
    private AudioClip containerFinished;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void PlayItemGrabbed()
    {
        audioSource.PlayOneShot(itemGrabbed);
    }

    public void PlayItemDropped()
    {
        audioSource.PlayOneShot(itemDropped);
    }

    public void PlayContainerFinished()
    {
        audioSource.PlayOneShot(containerFinished);
    }
}
