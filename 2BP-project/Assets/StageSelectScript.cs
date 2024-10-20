using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    // This will store the selected spawner index
    public static int spawnerIndex = 0;

    public void SelectStage1()
    {
        // Set spawnerIndex for Stage 1
        spawnerIndex = 1;
        // Load the DemoGame scene
        SceneManager.LoadScene("DemoStage");
    }

    public void SelectStage2()
    {
        // Set spawnerIndex for Stage 2
        spawnerIndex = 2;
        // Load the DemoGame scene
        SceneManager.LoadScene("DemoStage");
    }

    public void SelectStage3()
    {
        // Set spawnerIndex for Stage 3
        spawnerIndex = 3;
        // Load the DemoGame scene
        SceneManager.LoadScene("DemoStage");
    }
}
