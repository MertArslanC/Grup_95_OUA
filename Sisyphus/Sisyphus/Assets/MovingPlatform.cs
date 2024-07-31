using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float forceSpeed;

    public bool timer;

    private void Start()
    {
        forceSpeed = speed;
    }
    private void Update()
    {
        rb.velocity = Vector3.forward * forceSpeed;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            timer = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            Rigidbody rb=collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(this.rb.velocity.x,rb.velocity.y,this.rb.velocity.z);
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
