using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGirlCombatScript : MonoBehaviour
{
    public LogicScript logic;
    public Animator animator;
    public Transform attackBox;
    public LayerMask enemyLayers;
    public float attackRange;
    public float attackHeight;
    public float attackDistance;
    public int attackDamage = 1;
    public float attackHitStun = 0.1f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (logic.hp > 0)
        {
            // Verifica se há toques na tela
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    Vector3 touchPosition = touch.position;

                    // Apenas registra a ação quando o toque começa (fase de "Began")
                    if (touch.phase == TouchPhase.Began)
                    {
                        // Verifica se o toque está no lado direito da tela para atacar
                        if (touchPosition.x > Screen.width / 2 && Time.time >= nextAttackTime && !logic.waiting)
                        {
                            Attack();
                            nextAttackTime = Time.time + 1f / attackRate;
                        }
                    }
                }
            }

            // Verificação para o input do teclado (X para atacar)
            if (Input.GetKeyDown(KeyCode.X) && Time.time >= nextAttackTime && !logic.waiting)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }            
    }

    void Attack()
    {
        // Toca a animação de ataque
        animator.SetTrigger("BirdGirlAttack");

        // Define um offset para a posição do attackBox
        Vector2 offset = new Vector2(attackDistance, attackHeight);

        // Ajusta a posição do attackBox com o offset
        Vector2 adjustedAttackPosition = (Vector2)attackBox.position + offset;

        // Detecta inimigos no alcance da posição ajustada
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(adjustedAttackPosition, attackRange, enemyLayers);

        // Causa dano aos inimigos
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyT1Script>().TakeDamage(attackDamage);
            logic.TimeStop(attackHitStun);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackBox == null)
            return;

        Gizmos.color = new Color(1, 0, 0, 0.5f); // Cor vermelha com 50% de transparência

        Vector3 offset = new Vector3(attackDistance, attackHeight, 0f);
        Vector3 adjustedPosition = attackBox.position + offset;

        Gizmos.DrawWireSphere(adjustedPosition, attackRange);
    }
}
