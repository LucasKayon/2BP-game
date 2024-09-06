using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT1Script : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed;
    public float deadZone = -45;
    public LogicScript logic;
    bool triggered = false;
    public int maxHealth = 1;
    int currentHealth;
    float scoreIncrease = 100;
    float scoreMultiplier = 1;
    float scorePenalty = -50;
    public float deathAnimationFrames;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            //Debug.Log("EnemyT1 deleted");
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Confere se o layer de colisão é o do jogador e se o obstaculo já foi ativado.
        if (collision.gameObject.layer == 3 && triggered == false)
        {
            EnemyAttack();
            triggered = true;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Hurt animation

        if(currentHealth <= 0)
        {
            logic.increaseEnergy(1);
            logic.increaseScore(scoreIncrease,scoreMultiplier);
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

