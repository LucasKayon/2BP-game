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
    public float attackHeight;
    public float attackDistance;
    public int attackDamage = 1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("BirdGirlAttack");

        // Define an offset for the attackBox position
        Vector2 offset = new Vector2(attackDistance, attackHeight);

        // Adjust the attackBox position with the offset
        Vector2 adjustedAttackPosition = (Vector2)attackBox.position + offset;

        // Detect enemies in range of the adjusted attack position
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(adjustedAttackPosition, attackRange, enemyLayers);

        // Damage the enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("TESTE");
            enemy.GetComponent<EnemyT1Script>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackBox == null)
            return;

        Gizmos.color = new Color(1, 0, 0, 0.5f); // Red color with 50% transparency

        // Use the same offset as in the Attack method to ensure the Gizmo reflects the actual collider position
        Vector3 offset = new Vector3(attackDistance, attackHeight, 0f);

        // Calculate the adjusted position for the Gizmo
        Vector3 adjustedPosition = attackBox.position + offset;

        Gizmos.DrawWireSphere(adjustedPosition, attackRange);
    }
}
