using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int hp;
    public int energy;
    public float score;
    public Text hpIndicator;
    public Text energyIndicator;
    public Text scoreIndicator;
    public GameOverScreen GameOverScreen;
    public ClearStageScript clearStage;
    public bool waiting;

    // References to the obstacle spawners
    public GameObject obstacleSpawner1;
    public GameObject obstacleSpawner2;
    public GameObject obstacleSpawner3;

    void Start()
    {
        hp = 3;
        energy = 1;
        score = 0;

        // Deactivate all spawners first
        obstacleSpawner1.SetActive(false);
        obstacleSpawner2.SetActive(false);
        obstacleSpawner3.SetActive(false);

        // Activate the correct spawner based on the selected stage (spawnerIndex)
        switch (StageSelectScript.spawnerIndex)
        {
            case 1:
                obstacleSpawner1.SetActive(true);
                break;
            case 2:
                obstacleSpawner2.SetActive(true);
                break;
            case 3:
                obstacleSpawner3.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid spawner index");
                break;
        }
    }

    [ContextMenu("Decrease HP")]
    public void detractHp(int scoreToDetract)
    {
        hp = hp - scoreToDetract;
        hpIndicator.text = $"HP: {hp}";
    }

    [ContextMenu("Increase Energy")]
    public void increaseEnergy(int energyToIncrease)
    {
        energy = energy + energyToIncrease;
        energyIndicator.text = $"ENERGY: {energy}";
    }

    [ContextMenu("Decrease Energy")]
    public void decreaseEnergy(int energyToDecrease)
    {
        energy = energy - energyToDecrease;
        energyIndicator.text = $"ENERGY: {energy}";
    }

    [ContextMenu("Set Energy Value")]
    public void setEnergyValue(int energyValue)
    {
        energy = energyValue;
        energyIndicator.text = $"ENERGY: {energy}";
    }

    [ContextMenu("Increase Score")]
    public void increaseScore(float scoreIncrease, float multiplier)
    {
        score = score + (scoreIncrease * multiplier);
        scoreIndicator.text = $"SCORE: {score}";
    }

    public void setScore(float scoreValue)
    {
        score = scoreValue;
        scoreIndicator.text = $"SCORE: {score}";
    }

    public void stageClear()
    {
        if (hp >= 1)
        {
            clearStage.Setup(score);
        }
    }

    public void TimeStop(float duration)
    {
        if (waiting)
        {
            return;
        }

        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }

    void Update()
    {
        if (hp <= 0)
        {
            setScore(0);
            GameOverScreen.Setup();
        }
    }
}
