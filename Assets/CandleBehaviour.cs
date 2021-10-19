using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CandleBehaviour : MonoBehaviour
{
    public Stats playerStats;
    public Light2D candle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats.candleTime < 0.5f)
        {
            candle.intensity = playerStats.candleTime + 0.5f;
        } else
        {
            candle.intensity = 1f;
        }
    }
}
