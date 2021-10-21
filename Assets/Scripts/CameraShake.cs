using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;
    public float shakeFrequency;
    private bool shaking;

    public float shakeDuration;

    private float shakeTimeLeft;

    private bool hold;
    private void Start()
    {
        mainCamera = Camera.main;
        Debug.Log(mainCamera.transform.position);
        shakeTimeLeft = shakeDuration;
    }

    private void Update()
    {
        if(shaking)
        {
            Shake();
        }else if(!hold)
        {
            StopShake();
        }
    }

    public void StartShake()
    {
        hold = false;
        shaking = true;
    }

    private void Shake()
    {
        mainCamera.transform.position += Random.insideUnitSphere * shakeFrequency;
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -1.5f);

        shakeTimeLeft -= Time.deltaTime;
        shakeTimeLeft = Mathf.Clamp01(shakeTimeLeft);
        if (shakeTimeLeft == 0)
        {
            shakeTimeLeft = shakeDuration;
            shaking = false;
        }
    }

    private void StopShake()
    {
        Vector3 pos = new Vector3(0, 0, -1);
        mainCamera.transform.localPosition = pos;
        hold = true;
    }
}
