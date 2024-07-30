using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem fireParticles; // Ateþin parçacýk sistemi
    public float fireDuration = 1f; // Ateþin çýkma süresi
    public float restDuration = 1f; // Ateþin çýkmama süresi
    //public int damageAmount = 1; // Oyuncuya verilecek hasar miktarý
    private Collider fireCollider; // Ateþin collider'ý

    private bool isFiring = false;

    void Start()
    {
        fireParticles.Stop(); // Baþlangýçta ateþi kapalý tut
        fireCollider = GetComponent<Collider>(); // Bu obje üzerindeki Collider'ý al
        if (fireCollider != null)
        {
            fireCollider.enabled = false; // Baþlangýçta collider'ý devre dýþý býrak
        }
        StartCoroutine(FireControlRoutine()); // Ateþ püskürtme coroutine'ini baþlat
    }

    IEnumerator FireControlRoutine()
    {
        while (true)
        {
            // Ateþ partiküllerini baþlat
            fireParticles.Play();
            isFiring = true;

            // Collider'ý etkinleþtir
            if (fireCollider != null)
            {
                fireCollider.enabled = true;
            }

            // Ateþ çýkma süresi kadar bekle
            yield return new WaitForSeconds(fireDuration);

            // Ateþ partiküllerini durdur
            fireParticles.Stop();
            isFiring = false;

            // Collider'ý devre dýþý býrak
            if (fireCollider != null)
            {
                fireCollider.enabled = false;
            }

            // Ateþ çýkmama süresi kadar bekle
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
