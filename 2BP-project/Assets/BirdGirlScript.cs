using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterController2D;
using UnityEngine.Events;

public class BirdGirlScript : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings

    public Rigidbody2D myRigidBody;
    public float flapIntensity;
    public Animator animator;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.    
    public UnityEvent OnLandEvent;

    //[SerializeField] private Animator anim;
    //[SerializeField] private float attackSpeed;
    //[SerializeField] private float damage;

    float timeUntilAttack;
    int currentEnergy;
    public LogicScript logic;
    public int maxEnergy = 5;
    public float scorePenalty = -50;
    public float hitStun = 1;

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

        if (Input.GetKeyDown(KeyCode.Z) == true && logic.energy >= 1 && logic.waiting == false || Input.GetMouseButton(0) && touchPos.x<0 && logic.energy >=1 && logic.waiting == false)
        {
            logic.decreaseEnergy(1);
            myRigidBody.velocity = Vector2.up * flapIntensity;            
            animator.Play("Bird_Girl_Fly");
            //animator.SetBool("IsFlying", true);
        }

        
    }

    public void TakeDamage()
    {
        logic.detractHp(1);
        logic.increaseScore(scorePenalty, 1);
        if (logic.score <= 0)
        {
            logic.setScore(0);
        }
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

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {                
                m_Grounded = true;
                
                if (!wasGrounded)
                { 
                    
                    OnLandEvent.Invoke();
                    if(logic.energy <= 0)
                    {
                        logic.setEnergyValue(1);
                    }
                    
                    

                    
                }
                    
            }
        }
    }
}
