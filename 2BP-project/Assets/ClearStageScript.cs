using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ClearStageScript : MonoBehaviour
{
    public LogicScript logic;
    public Text scoreIndicator;


    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(float score)
    {
        scoreIndicator.text = $"SCORE: {score}";
        gameObject.SetActive(true);
    }

    
}
