using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlatform : MonoBehaviour
{
    public enum MoveAxis
    {
        X,
        Y,
        Z
    }

    public Rigidbody rb;
    public float speed;
    public float forceSpeed;

    public bool timer;
    public MoveAxis moveAxis = MoveAxis.X;

    private GameObject player;
    private CharacterController _controller;
    
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

        Vector3 moveDirection;
        switch (moveAxis)
        {
            case MoveAxis.X:
                moveDirection = Vector3.right;
                break;
            case MoveAxis.Y:
                moveDirection = Vector3.up;
                break;
            case MoveAxis.Z:
                moveDirection = Vector3.forward;
                break;
            default:
                moveDirection = Vector3.zero;
                break;
        }
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
