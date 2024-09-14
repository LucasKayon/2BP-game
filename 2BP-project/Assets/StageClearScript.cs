using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearScript : MonoBehaviour
{
    public float moveSpeed;
    public float deadZone = -45;
    public LogicScript logic;
    bool triggered = false;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
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
            triggered = true;
            logic.stageClear();
        }

    }
}
