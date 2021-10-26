using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{

    public GameObject gameOverScreen;

    public ObjectSpawner[] spawners;
    public Stats player;
    public MonsterMovement monster;
    private bool paused;

    public int score;

    public Text gameOverText;
    public Text scoreText;

    public ContainerPool containerPool;

    public VariableBasedSoundEffect[] variableSFX;

    public void IncreaseScore()
    {
        score += 1;
        UpdateScoreUI();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
        gameOverText.text = "Score: " + score;
    }

    private void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreUI();
    }

    private void Start()
    {
        Time.timeScale = 0;
        paused = true;

        spawners = FindObjectsOfType<ObjectSpawner>();
        variableSFX = FindObjectsOfType<VariableBasedSoundEffect>();
    }

    public void ResetGame()
    {
        SetScore(0);
        containerPool.ResetAllObjects();
        foreach(ObjectSpawner spawner in spawners)
        {
            spawner.ResetSpawnPoints();
            spawner.SpawnObjects();
        }

        foreach(VariableBasedSoundEffect sfx in variableSFX)
        {
            sfx.conditionVariable = false;
        }
        player.SetStatsToDefault();
        monster.SetMonsterToDefault();
        paused = false;
        Time.timeScale = 1f;
        player.GetComponent<Stats>().UpdateLivesUI();
        player.GetComponent<Stats>().ClearItemSprite();
    }

    public void QuitGame(){
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public bool IsGamePaused()
    {
        return paused;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        PauseGame();
    }

    public void PauseGame()
    {
        if(paused)
        {
            paused = false;
            Time.timeScale = 1f;
            
        } else
        { 
            paused = true;
            Time.timeScale = 0;
        }
    }
}
