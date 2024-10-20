using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner02Script : MonoBehaviour
{
    public float timer = 0;
    public GameObject bottomObstacle;
    public GameObject topObstacle;
    public GameObject spike;
    public GameObject enemyT1;
    public GameObject enemyT2;
    public GameObject clearScreen;

    [System.Serializable]
    public struct ObstacleData
    {
        public float spawnTime;
        public int obstacleType;
        public float obstacleHeight;
    }

    public ObstacleData[] obstacleData; // Array containing the spawn time, type, and height
    private int spawnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn(bottomObstacle);
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Check if the current time has reached the next spawn time
        if (spawnIndex < obstacleData.Length && timer >= obstacleData[spawnIndex].spawnTime)
        {
            // Get the current obstacle data
            ObstacleData currentObstacle = obstacleData[spawnIndex];

            // Check obstacle type and spawn accordingly
            if (currentObstacle.obstacleType == 0)
            {
                Spawn(spike, currentObstacle.obstacleHeight);
            }
            if (currentObstacle.obstacleType == 1)
            {
                Spawn(enemyT1, currentObstacle.obstacleHeight);
            }
            if (currentObstacle.obstacleType == 2)
            {
                Spawn(enemyT2, currentObstacle.obstacleHeight);
            }
            if (currentObstacle.obstacleType == 10)
            {
                Spawn(clearScreen, currentObstacle.obstacleHeight);
            }

            spawnIndex++; // Move to the next spawn time
        }

    }

    public void Spawn(GameObject obstacle, float height)
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, height, transform.position.z);
        Instantiate(obstacle, spawnPosition, transform.rotation);
    }
}

//height for bottom obstacles must be -7 or close
//height for enemyT1 must be 2 for middle, -7 for bottom and 9 for top