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

    public int defaultLives;
    public GameMaster gm;

    private void Start()
    {
        SetStatsToDefault();
        UpdateLivesUI();
    }


    public void OnMonsterAttackHandler()
    {
        Debug.LogWarning("OnMonsterAttackHandler");
        DecreaseLives();
        UpdateLivesUI();
        if(lives <= 0)
        {
            gm.GameOver();
        }
    }

    private void DecreaseLives()
    {
        lives -= 1;
        GetComponent<AudioSource>().Play();
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
