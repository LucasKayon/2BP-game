using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int hp;
    public Text hpIndicator;

    void Start()
    {
        hp = 3;
    }
    

    [ContextMenu("Decrease HP")]
   public void detractHp(int scoreToDetract)
    {
        hp = hp - scoreToDetract;
        //Debug.Log($"HP decreased, current HP: {hp}");
        hpIndicator.text = $"HP: {hp}";
    }
}
