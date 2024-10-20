using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT2Script : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed;
    public float deadZone = -45;
    public LogicScript logic;
    public int maxHealth = 1;
    int currentHealth;
    float scoreIncrease = 100;
    float scoreMultiplier = 1;
    float scorePenalty = -50;
    public float deathAnimationFrames;

    // Variables for sinusoidal movement
    public float waveAmplitude = 3f; // Height of the wave (adjustable)
    public float waveFrequency = 1f; // Speed of the wave (adjustable)
    private float startYPosition;     // Starting Y position of the enemy

    // Min and Max height for movement
    public float minHeight = -7f;
    public float maxHeight = 9f;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        currentHealth = maxHealth;

        // Store the initial Y position to use in the sinusoidal calculation
        startYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Leftward movement
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Sinusoidal movement on the Y-axis
        float newYPosition = startYPosition + Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

        // Clamp the Y position to stay between the minHeight and maxHeight
        newYPosition = Mathf.Clamp(newYPosition, minHeight, maxHeight);

        // Apply the new Y position
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

        // Check if the enemy has reached the dead zone and destroy it
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Hurt animation (if applicable)

        if (currentHealth <= 0)
        {
            logic.increaseEnergy(1);
            logic.increaseScore(scoreIncrease, scoreMultiplier);
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died");

        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Destroy(gameObject, deathAnimationFrames);
    }

    public void EnemyAttack()
    {
        logic.detractHp(1);
        logic.increaseScore(scorePenalty, 1);
        if (logic.score <= 0)
        {
            logic.setScore(0);
        }
    }
}
