using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Stats : MonoBehaviour
{
    public int lives;
    public float stamina;
    public float candleTime;

    public Text livesText;
    public Text staminaText;
    public Text candleText;

    public UnityEvent OnPlayersDeath;
    public UnityEvent OnPlayerLowLife;

    public int defaultLives;
    public GameMaster gm;

    public VariableBasedSoundEffect whimper;

    public AudioSource hurtSFX;
    public AudioSource whimperSFX;

    public CameraShake cameraShake;

    private void Start()
    {
        OnPlayerLowLife.AddListener(delegate { whimper.conditionVariable = true; });
        SetStatsToDefault();
        UpdateLivesUI();
        Debug.LogWarning(cameraShake.gameObject.name);
    }


    public void OnMonsterAttackHandler()
    {
        DecreaseLives();
        UpdateLivesUI();
        cameraShake.StartShake();
        if(lives <= 0)
        {
            gm.GameOver();
        }
    }

    private void DecreaseLives()
    {
        lives -= 1;

        hurtSFX.Play();
        whimperSFX.Play();

        if(lives == 1)
        {
            OnPlayerLowLife.Invoke();
        }
        if (lives <= 0)
        {
            OnPlayersDeath.Invoke();
        }
    }

    public void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    public void UpdateStaminaUI()
    {
        staminaText.text = "Stamina: " + (int)(stamina * 100);
    }

    public void UpdateCandleUI()
    {
        candleText.text = "Remaining Light: " + (int)(candleTime*100);
    }

    public void SetStatsToDefault()
    {
        lives = defaultLives;
        stamina = 1;
        candleTime = 1;
    }
}
