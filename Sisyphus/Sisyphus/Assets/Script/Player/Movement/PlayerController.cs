using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float speed,power;
    private float gravity;

    [SerializeField] GameObject stonePrefab;
    [SerializeField] Transform stoneSpawnPos;
    [SerializeField] private float stoneThrowPower;
    private void Start()
    {
        rigidBody= GetComponent<Rigidbody>();
        gravity = (float)Physics.gravity.y;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigidBody.AddForce(Vector3.up *power , ForceMode.Impulse);
            
            //rigidBody.velocity = Vector3.up * (jumpDistance / jumpTime + (1 / 2 * gravity * jumpTime));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            FireStone();
        }

    }
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction=new Vector3(horizontalInput, 0, verticalInput).normalized;
        
        rigidBody.velocity= direction*speed;

    }

    private void FireStone()
    {
        GameObject instStone = Instantiate(stonePrefab,stoneSpawnPos.position,Quaternion.identity);

        Rigidbody rb = instStone.GetComponent<Rigidbody>();

        rb.AddForce(instStone.transform.forward*stoneThrowPower,ForceMode.Impulse);
    }

}
