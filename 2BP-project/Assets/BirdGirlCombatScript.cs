using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGirlCombatScript : MonoBehaviour
{
    public Animator animator;
    public Transform attackBox;
    public LayerMask enemyLayers;
    public float attackRange;
    public int attackDamage = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }

    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("BirdGirlAttack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackBox.position, attackRange, enemyLayers);

        // Damage the enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("TESTE");
            enemy.GetComponent<EnemyT1Script>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackBox == null)
            return;

        Gizmos.DrawSphere(attackBox.position, attackRange);
    }
}
