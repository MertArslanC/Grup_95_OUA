using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float mesafe;
    public Transform player;
    Vector3 pos;
    private Animator enemyanimator;

    void Start()
    {
        enemyanimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        mesafe= Vector3.Distance(transform.position, player.position);
        pos=new Vector3(player.position.x,transform.position.y,player.position.z);
        if(mesafe<10f)
        {
            transform.LookAt(pos);
            enemyanimator.SetBool("combatmode", true);
            enemyanimator.SetBool("attackmode", false);
            if (mesafe < 2f)
            {
                enemyanimator.SetBool("attackmode", true);
            }
        }
        else
        {
            enemyanimator.SetBool("combatmode", false); 
            enemyanimator.SetBool("attackmode", false);

        }
    }
}
