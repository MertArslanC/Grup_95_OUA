using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private Animator _bossanimator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject endingCanvas, startCanvas;
    [Header("Walk")]
    public float detectionRadius = 20f;
    public Transform player;
    [Header("Attack")]
    public float attackRadius = 2f;

    float distanceToPlayer;
    [Header("Boss Health")]
    public float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float damageToCharacter;
    [Header("Character Health")]
    public float chr_currentHealth;
    [SerializeField] private float chr_maxHealth;
    [SerializeField] private Slider chr_healthSlider;
    [SerializeField] private float damageToBoss;
    void Start()
    {
        _bossanimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        chr_currentHealth = chr_maxHealth;
        StartCoroutine(effect());
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        healthSlider.value = currentHealth / maxHealth;
        chr_healthSlider.value = chr_currentHealth / chr_maxHealth;
        if (currentHealth > 0) {
            if (attackRadius < distanceToPlayer && distanceToPlayer <= detectionRadius)
            {

                navMeshAgent.SetDestination(player.position);
                _bossanimator.SetBool("BossisWalking", true);
                _bossanimator.SetBool("BossisAttacking", false);
            }
            if (distanceToPlayer <= attackRadius)
            {


                navMeshAgent.ResetPath();
                _bossanimator.SetBool("BossisAttacking", true);
                _bossanimator.SetBool("BossisWalking", false);
            }

            else if (distanceToPlayer > detectionRadius)
            {

                navMeshAgent.SetDestination(transform.position);
                _bossanimator.SetBool("BossisWalking", false);
                _bossanimator.SetBool("BossisAttacking", false);
            }
        }
        else
        {
            startCanvas.SetActive(true);
            navMeshAgent.ResetPath();
            _particleSystem.Play();
            endingCanvas.gameObject.SetActive(false);
            _bossanimator.SetTrigger("BossDead");
            _bossanimator.SetBool("BossisWalking", false);
            _bossanimator.SetBool("BossisAttacking", false);
        }
    }

    public void BossAttack()
    {
        if(distanceToPlayer < attackRadius)
        {
            chr_currentHealth -= damageToCharacter;
        }
        else
        {
            Debug.Log("Playere Vurulmadý!");
        }

    }
    IEnumerator effect()
    {

        yield return new WaitForSeconds(1);
        _particleSystem.Pause();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stone"))
        {
            currentHealth -= damageToBoss;
            Destroy(other.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}