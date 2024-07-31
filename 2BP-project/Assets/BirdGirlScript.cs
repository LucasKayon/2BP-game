using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGirlScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapIntensity;
    public Animator animator;

    //[SerializeField] private Animator anim;
    //[SerializeField] private float attackSpeed;
    //[SerializeField] private float damage;

    float timeUntilAttack;

    // Start is called before the first frame update
    void Start()
    {
        //timeUntilAttack = 0f; // Initialize timeUntilAttack to 0 to allow immediate attack
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            myRigidBody.velocity = Vector2.up * flapIntensity;            
            animator.Play("Bird_Girl_Fly");
            //animator.SetBool("IsFlying", true);
        }

        
    }
}
