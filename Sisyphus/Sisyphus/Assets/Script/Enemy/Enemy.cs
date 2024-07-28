using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float mesafe;
    public Transform player;
    public float patrolRadius = 5f; // D��man�n hareket edebilece�i yar��ap
    public float speed = 1f; // D��man�n y�r�me h�z�
    public LayerMask groundLayer; // D��man�n hareket edebilece�i zemin katman�
    public float gravity = -9.8f; // Yer �ekimi de�eri

    private Vector3 startPoint;
    private Vector3 targetPoint;
    private Animator enemyAnimator;
    private Rigidbody rb;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false; // Varsay�lan yer �ekimini kapat�yoruz

        startPoint = transform.position;
        SetNewTargetPoint();
    }

    void Update()
    {
        mesafe = Vector3.Distance(transform.position, player.position);

        if (mesafe < 15f)
        {
            Vector3 pos = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(pos);
            enemyAnimator.SetBool("combatmode", true);
            enemyAnimator.SetBool("attackmode", false);
            if (mesafe < 2f)
            {
                enemyAnimator.SetBool("attackmode", true);
            }
        }
        else
        {
            enemyAnimator.SetBool("combatmode", false);
            enemyAnimator.SetBool("attackmode", false);
            Patrol();
        }

        ApplyCustomGravity();
        ConstrainWithinLayer();
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, targetPoint) < 15f)
        {
            SetNewTargetPoint();
        }
        else
        {
            MoveTowards(targetPoint);
        }
    }

    void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Yerden y�ksekli�i kontrol et ve d�zeltiyorsa d��man�n pozisyonunu ayarla
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    void SetNewTargetPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection.y = 0; // Y�ksekli�i s�f�rl�yoruz ki d��man sadece yatay d�zlemde hareket etsin
        targetPoint = startPoint + randomDirection;

        // Patrol alan�n�n d���na ��kmamas� i�in kontrol
        if (Vector3.Distance(targetPoint, startPoint) > patrolRadius)
        {
            targetPoint = startPoint + (randomDirection.normalized * patrolRadius);
        }

        // D��man�n yerinde kalmas� i�in targetPoint'i zeminde tutmak
        if (Physics.Raycast(targetPoint + Vector3.up * 10, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = startPoint;
        }
    }

    void ApplyCustomGravity()
    {
        // Yer �ekimi kuvvetini uyguluyoruz
        Vector3 gravityVector = new Vector3(0, gravity, 0);
        rb.AddForce(gravityVector, ForceMode.Acceleration);
    }

    void ConstrainWithinLayer()
    {
        // D��man�n groundLayer i�inde olup olmad���n� kontrol ediyoruz
        if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, Mathf.Infinity, groundLayer))
        {
            // D��man groundLayer d���na ��kt�ysa onu ba�lang�� noktas�na geri getiriyoruz
            rb.MovePosition(startPoint);
        }
    }
}
