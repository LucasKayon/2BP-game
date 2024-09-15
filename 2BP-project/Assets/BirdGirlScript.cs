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

    private bool isInvincible = false; // Track invincibility state
    public float invincibilityDuration = 2f; // Duration of invincibility in seconds

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        currentEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Keyboard input check
        if (Input.GetKeyDown(KeyCode.Z) && logic.energy >= 1 && !logic.waiting)
        {
            logic.decreaseEnergy(1);
            myRigidBody.velocity = Vector2.up * flapIntensity;
            animator.Play("Bird_Girl_Fly");
        }

        // Mouse input check
        if (Input.GetMouseButtonDown(0) && touchPos.x < 0 && logic.energy >= 1 && !logic.waiting)
        {
            logic.decreaseEnergy(1);
            myRigidBody.velocity = Vector2.up * flapIntensity;
            animator.Play("Bird_Girl_Fly");
        }
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
        yield return new WaitForSeconds(invincibilityDuration); // Wait for 2 seconds (or specified duration)
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("BirdGirlDamage");
        TakeDamage();
        logic.TimeStop(hitStun);
    }

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
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
                    if (logic.energy <= 0)
                    {
                        logic.setEnergyValue(1);
                    }
                }
            }
        }
    }
}
