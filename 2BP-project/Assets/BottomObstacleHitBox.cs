using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomObstacleHitBox : MonoBehaviour
{
    public LogicScript logic;

    bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
