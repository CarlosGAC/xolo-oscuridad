using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Stats : MonoBehaviour
{
    public int lives;
    public float stamina;

    public Text livesText;
    public Text staminaText;

    public UnityEvent OnPlayersDeath;

    public int defaultLives;

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
    }

    private void DecreaseLives()
    {
        lives -= 1;
        if(lives <= 0)
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

    public void SetStatsToDefault()
    {
        lives = defaultLives;
        stamina = 1;
    }
}
