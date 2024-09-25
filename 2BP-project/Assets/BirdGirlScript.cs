using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterController2D;
using UnityEngine.Events;

public class BirdGirlScript : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilingCheck;

    public Rigidbody2D myRigidBody;
    public float flapIntensity;
    public Animator animator;

    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    public UnityEvent OnLandEvent;

    float timeUntilAttack;
    int currentEnergy;
    public LogicScript logic;
    public int maxEnergy = 5;
    public float scorePenalty = -50;
    public float hitStun = 1;

    private bool isInvincible = false;
    public float invincibilityDuration = 2f;

    private bool stopGroundCheck = false;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        if(logic.hp > 0)
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
                        // Verifica se o toque está no lado esquerdo da tela para pular
                        if (touchPosition.x < Screen.width / 2 && logic.energy >= 1 && !logic.waiting)
                        {
                            HandleJump();
                        }
                    }
                }
            }

            // Verificação para o input do teclado (Z para pular)
            if (Input.GetKeyDown(KeyCode.Z) && logic.energy >= 1 && !logic.waiting)
            {
                HandleJump();
            }
        }
        
    }

    private void HandleJump()
    {
        logic.decreaseEnergy(1);
        myRigidBody.velocity = Vector2.up * flapIntensity;
        animator.Play("Bird_Girl_Fly");

        // Para a checagem de solo por 0.5 segundos após pular
        StartCoroutine(StopGroundCheckForTime(0.5f));
    }

    public void TakeDamage()
    {
        if (!isInvincible)
        {
            logic.detractHp(1);
            logic.increaseScore(scorePenalty, 1);
            if (logic.score <= 0)
            {
                logic.setScore(0);
            }
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido tem a tag "Enemy"
        if (collision.CompareTag("Obstacle") && logic.hp > 0)
        {
            animator.SetTrigger("BirdGirlDamage");
            TakeDamage();
            logic.TimeStop(hitStun);
        }
    }


    private void FixedUpdate()
    {
        if (stopGroundCheck)
        {
            return;
        }

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;

                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }

                logic.setEnergyValue(1);
            }
        }

        animator.SetBool("IsGrounded", m_Grounded);
    }

    private IEnumerator StopGroundCheckForTime(float delay)
    {
        stopGroundCheck = true;
        yield return new WaitForSeconds(delay);
        stopGroundCheck = false;
    }
}
