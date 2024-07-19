using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float mesafe;
    public Transform player;
    public float patrolDistance = 5f; // D��man�n y�r�yece�i mesafe
    public float speed = 1f; // D��man�n y�r�me h�z�

    Vector3 pos;
    private Animator enemyanimator;
    private Vector3 startPoint;
    private Vector3 targetPoint;
    private bool movingToTarget;

    void Start()
    {
        enemyanimator = GetComponent<Animator>();
        startPoint = transform.position;
        SetNewTargetPoint();
    }

    // Update is called once per frame
    void Update()
    {
        mesafe = Vector3.Distance(transform.position, player.position);
        pos = new Vector3(player.position.x, transform.position.y, player.position.z);

        if (mesafe < 10f)
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
            Patrol();
        }
    }

    void Patrol()
    {
        MoveTowards(targetPoint);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            SetNewTargetPoint();
        }
    }

    void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void SetNewTargetPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolDistance;
        randomDirection.y = 0; // Y�ksekli�i s�f�rl�yoruz ki d��man sadece yatay d�zlemde hareket etsin
        targetPoint = transform.position + randomDirection;

        // Patrol alan�n�n d���na ��kmamas� i�in kontrol ekleyebiliriz (iste�e ba�l�)
        if (Vector3.Distance(targetPoint, startPoint) > patrolDistance)
        {
            targetPoint = startPoint + (randomDirection.normalized * patrolDistance);
        }
    }
}
