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

    void Start()
    {
        hp = 3;
        energy = 1;
        score = 0;
    }
    

   [ContextMenu("Decrease HP")]
   public void detractHp(int scoreToDetract)
    {
        hp = hp - scoreToDetract;
        //Debug.Log($"HP decreased, current HP: {hp}");
        hpIndicator.text = $"HP: {hp}";
    }

    [ContextMenu("Increase Energy")]
    public void increaseEnergy(int energyToIncrease)
    {
        energy = energy + energyToIncrease;
        //Debug.Log($"ENERGY increased, current ENERGY: {energy}");
        energyIndicator.text = $"ENERGY: {energy}";
    }

    [ContextMenu("Decrease Energy")]
    public void decreaseEnergy(int energyToDecrease)
    {
        energy = energy - energyToDecrease;
        //Debug.Log($"ENERGY increased, current ENERGY: {energy}");
        energyIndicator.text = $"ENERGY: {energy}";
    }

    [ContextMenu("Set Energy Value")]
    public void setEnergyValue(int energyValue)
    {
        energy = energyValue;
        Debug.Log($"ENERGY RESET, current ENERGY: {energy}");
        energyIndicator.text = $"ENERGY: {energy}";
    }

    [ContextMenu("Increase Score")]
    public void increaseScore(float scoreIncrease, float multiplier)
    {
        score = score + (scoreIncrease*multiplier);
        //Debug.Log($"ENERGY increased, current ENERGY: {energy}");
        scoreIndicator.text = $"SCORE: {score}";
    }

    public void setScore(float scoreValue)
    {
        score = scoreValue;
        scoreIndicator.text = $"SCORE: {score}";
    }

    void Update()
    {
        if (hp <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
