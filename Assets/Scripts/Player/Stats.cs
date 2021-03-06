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

    public GameObject livesContainer;
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

    public SpriteRenderer itemSprite;
    private void Start()
    {
        OnPlayerLowLife.AddListener(delegate { whimper.conditionVariable = true; });
        SetStatsToDefault();
        UpdateLivesUI();
    }

    public void ClearItemSprite()
    {
        itemSprite.sprite = null;
    }

    public void OnMonsterAttackHandler()
    {
        DecreaseLives();
        UpdateLivesUI();
        if(lives > 0)
        {
            cameraShake.StartShake();
        }
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
        for(int i = 0; i < lives; i++)
        {
            livesContainer.transform.GetChild(i).gameObject.SetActive(true);
        }
            for (int i = lives; i < livesContainer.transform.childCount; i++)
            {
                livesContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
    }

    public void UpdateStaminaUI()
    {
        staminaText.text = "Stamina: " + (int)(stamina * 100);
    }

    public void UpdateCandleUI()
    {
        candleText.text = "Luz restante: " + (int)(candleTime*100);
    }

    public void SetStatsToDefault()
    {
        lives = defaultLives;
        stamina = 1;
        candleTime = 1;
    }
}
