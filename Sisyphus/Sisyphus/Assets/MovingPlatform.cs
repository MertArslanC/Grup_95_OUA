using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float forceSpeed;

    public bool timer;

    private GameObject player;
    private CharacterController _controller;
    public Vector3 moveDirection;
    public bool characterFollow;
    private void Start()
    {
        _controller = GameObject.FindAnyObjectByType<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        forceSpeed = speed;
        characterFollow = false;
    }
    private void Update()
    {
        moveDirection = Vector3.forward * forceSpeed;
        rb.velocity = moveDirection;
        if (timer == true)
        {
            if(forceSpeed > 0)
            {
                forceSpeed = -speed;
                timer=false;
            }
            else if(forceSpeed < 0)
            {
                forceSpeed = speed;
                timer = false;
            }
        }

        if(characterFollow)
        {
            _controller.Move(moveDirection*Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            timer = true;
        }

    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            characterFollow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            characterFollow = false;
        }
    }*/

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag =="Player")
        {

            //player.transform.position = gameObject.transform.position + transform.up * ((player.transform.localScale.y / 2) + gameObject.transform.localScale.y / 2);
            //Rigidbody rb=collision.gameObject.GetComponent<Rigidbody>();
            //rb.velocity = new Vector3(this.rb.velocity.x,rb.velocity.y,this.rb.velocity.z);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }*/
}
