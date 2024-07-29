using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 current_CheckPoint;
    CharacterController characterController;

    public float voidYPos;

    private GameObject previousCheckPoint;

    public Color checkTakenColor, checkNotTakenColor;
    
    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        current_CheckPoint = transform.position;
    }

    private void Update()
    {
        if(gameObject.transform.position.y <= voidYPos)
        {
            characterController.enabled = false;
            gameObject.transform.position = current_CheckPoint;
            characterController.enabled = true;
            gameManager.health -= 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkpoint"))
        {
            current_CheckPoint = other.gameObject.transform.position;

            if(previousCheckPoint != null)
            {
                previousCheckPoint.transform.GetChild(0).transform.localPosition = new Vector3(0f, -1f, 0f);
                previousCheckPoint.transform.GetComponentInChildren<SpriteRenderer>().color = checkNotTakenColor;
            }

            other.transform.GetChild(0).transform.localPosition = new Vector3(0f, 0f, 0f);
            other.transform.GetComponentInChildren<SpriteRenderer>().color = checkTakenColor;

            previousCheckPoint = other.gameObject;
        }
        else if (other.CompareTag("enemy"))
        {
            gameManager.health -= 1;
        }
    }
}
