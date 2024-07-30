using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem fireParticles; // Ate�in par�ac�k sistemi
    public float fireDuration = 1f; // Ate�in ��kma s�resi
    public float restDuration = 1f; // Ate�in ��kmama s�resi
    //public int damageAmount = 1; // Oyuncuya verilecek hasar miktar�
    private Collider fireCollider; // Ate�in collider'�

    private bool isFiring = false;

    void Start()
    {
        fireParticles.Stop(); // Ba�lang��ta ate�i kapal� tut
        fireCollider = GetComponent<Collider>(); // Bu obje �zerindeki Collider'� al
        if (fireCollider != null)
        {
            fireCollider.enabled = false; // Ba�lang��ta collider'� devre d��� b�rak
        }
        StartCoroutine(FireControlRoutine()); // Ate� p�sk�rtme coroutine'ini ba�lat
    }

    IEnumerator FireControlRoutine()
    {
        while (true)
        {
            // Ate� partik�llerini ba�lat
            fireParticles.Play();
            isFiring = true;

            // Collider'� etkinle�tir
            if (fireCollider != null)
            {
                fireCollider.enabled = true;
            }

            // Ate� ��kma s�resi kadar bekle
            yield return new WaitForSeconds(fireDuration);

            // Ate� partik�llerini durdur
            fireParticles.Stop();
            isFiring = false;

            // Collider'� devre d��� b�rak
            if (fireCollider != null)
            {
                fireCollider.enabled = false;
            }

            // Ate� ��kmama s�resi kadar bekle
            yield return new WaitForSeconds(restDuration);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (isFiring && other.CompareTag("Player"))
        {
            // Oyuncuya hasar ver
            GameManager gameManager = other.GetComponent<GameManager>();
            if (gameManager != null)
            {
                gameManager.health -= damageAmount;
                Debug.Log("Player health decreased by fire. Current health: " + gameManager.health);
            }
        }
    }
    */
}
