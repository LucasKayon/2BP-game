using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT1Script : MonoBehaviour
{
    public float moveSpeed;
    public float deadZone = -45;
    public LogicScript logic;
    bool triggered = false;
    public int maxHealth = 1;
    int currentHealth;

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

        if (transform.position.x < deadZone)
        {
            //Debug.Log("Bottom Obstable Deleted");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Confere se o layer de colisão é o do jogador e se o obstaculo já foi ativado.
        if (collision.gameObject.layer == 3 && triggered == false)
        {

            logic.detractHp(1);
            triggered = true;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Debug.Log("Enemy died");

        Destroy(gameObject);
    }
}

