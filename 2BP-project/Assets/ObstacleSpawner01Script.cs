using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner01Script : MonoBehaviour
{
    public float timer = 0;
    public GameObject bottomObstacle;
    public GameObject topObstacle;
    public GameObject enemyT1;
    public float[] spawnTimes; // Pre-determined spawn times
    public float[] obstacleType; // Pre-determined spawn type
    public float[] obstacleHeight; // Pre-determined spawn height
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
        //Debug.Log("Timer: " + timer + ", SpawnIndex: " + spawnIndex);

        // Check if the current time has reached the next spawn time
        if (spawnIndex < spawnTimes.Length && timer >= spawnTimes[spawnIndex])
        {
            //Debug.Log("Spawning obstacle at time: " + spawnTimes[spawnIndex]);

            if (obstacleType[spawnIndex] == 0)
            {
                Spawn(bottomObstacle, obstacleHeight[spawnIndex]);
            }
            if (obstacleType[spawnIndex] == 1)
            {
                Spawn(topObstacle, obstacleHeight[spawnIndex]);
            }
            if (obstacleType[spawnIndex] == 2)
            {
                Spawn(enemyT1, obstacleHeight[spawnIndex]);
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