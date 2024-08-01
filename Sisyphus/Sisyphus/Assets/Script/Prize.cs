using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prize : MonoBehaviour
{
    private Movement movement;
    private GameManager manager;
    [SerializeField] GameObject prize1, prize2, mark1, mark2, prizeheart, _gamemanager, gameCanvas, endCanvas ;
    [SerializeField] Countdown countdown;
    private AudioSource prizesound;
    private void Start()
    {
        movement= GetComponent<Movement>();
        prizesound = GetComponents<AudioSource>()[1];
        manager= _gamemanager.GetComponent<GameManager>();
        prize1.gameObject.SetActive(false);
        prize2.gameObject.SetActive(false);
      
       
    }
    private void Update()
    {
        if (prize1.activeSelf &&Input.GetKeyDown(KeyCode.E))
        {
            
            StartCoroutine(sixseconds());
            prize1.gameObject.SetActive(false);


        }
        if (prize2.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(fiveseconds());
            prize2.gameObject.SetActive(false);
            
        }
    }
    IEnumerator fiveseconds()
    {
        countdown.countdownText.color = Color.blue;
        countdown.div = 2;
        yield return new WaitForSeconds(5);
        countdown.countdownText.color = Color.black;
        countdown.div = 1;
        
    }
    IEnumerator sixseconds()
    {
        movement.moveSpeed = 15;
        yield return new WaitForSeconds(6);
        movement.moveSpeed = 4;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("prize"))
        {
            prizesound.Play();
            Destroy(mark1);
            prize1.gameObject.SetActive(true);

        }
        else if (other.gameObject.CompareTag("prize2"))
        {
            prizesound.Play();
            Destroy(mark2);
            prize2.gameObject.SetActive(true);
        }

        else if (other.gameObject.CompareTag("heartprize"))
        {
            prizesound.Play();
            Destroy(prizeheart);
            manager.health = manager.health >= 3 ? manager.health : manager.health + 1;

        }
        else if (other.gameObject.CompareTag("End"))
        { 
            gameCanvas.gameObject.SetActive(false);
            endCanvas.gameObject.SetActive(true);
        }

    }
}
