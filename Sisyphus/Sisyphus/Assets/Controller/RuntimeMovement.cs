using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class RuntimeMovement : MonoBehaviour
{
    private Movement _input;
    private CharacterController _controller;
    [SerializeField] private float fraction;
    private Animator _animator;
    private bool isRunning = false;
    private bool isRolling = false;
    private bool isBackwalking = false;
    private float yVelocity;
    public float jumpPower;
    private bool isGrounded;
    public float gravity;
    private float verticalVelocity;

    [Header("Stone Attack")]
    [SerializeField] GameObject stonePrefab;
    [SerializeField] Transform stoneSpawnPos;
    [SerializeField] private float stoneThrowPower;
    [SerializeField] private GameObject stoneInHand;

    [Header("Mouse Controller")]
    [SerializeField] private float mouseSensivity=1f;
    private float startedMousePos;

    private void Start()
    {
        _controller= GetComponent<CharacterController>();
        _input= GetComponent<Movement>();
        _animator= GetComponent<Animator>();
        _input.jumpAction.performed += OnJump;
        _input.runAction.performed += OnRun;
        _input.runAction.canceled += OnRunCanceled;
        _input.rollAction.performed += OnRoll; 
        _input.rollAction.canceled += OnRollCanceled;
        _input.backwalkAction.performed += OnBackwalk;
        _input.backwalkAction.canceled += OnBackwalkCanceled;
        _input.attackAction.performed += OnAttack;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        startedMousePos = Input.mousePosition.x;

        stoneInHand.SetActive(false);
    }
    private void OnDestroy()
    {
        
        _input.jumpAction.performed -= OnJump;
        _input.runAction.performed -= OnRun; 
        _input.runAction.canceled -= OnRunCanceled;
        _input.rollAction.performed -= OnRoll;
        _input.rollAction.canceled -= OnRollCanceled;
        _input.backwalkAction.performed -= OnBackwalk;
        _input.backwalkAction.canceled -= OnBackwalkCanceled;
        _input.attackAction.performed -= OnAttack;


    }
    private void Update()
    {
        Move();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _controller.height / 10 + 0.1f))
        {
            _animator.SetBool("grounded", true);
            isGrounded = true;
        }
        else
        {
            _animator.SetBool("grounded", false);
            isGrounded = false;
        }

        if (isGrounded && yVelocity < 0)
        {
            yVelocity = -9.8f;
        }

        if (!isGrounded)
        {
            yVelocity -= gravity * Time.deltaTime;
        }

        transform.eulerAngles = new Vector3(0, (Input.mousePosition.x - startedMousePos)/mouseSensivity,0);
    }
    private void Move()
    {
        
        _animator.SetFloat("speed", Mathf.Abs(_controller.velocity.x)+ Mathf.Abs(_controller.velocity.z));
        float speedMultiplier = isRunning ? 2.0f : 1.0f;


        Vector3 char_Direction = (gameObject.transform.right * _input.moveVal.x * _input.moveSpeed * speedMultiplier / fraction) +
            (gameObject.transform.forward * (_input.moveVal.y * _input.moveSpeed * speedMultiplier) / fraction);
        Vector3 char_Gravity = gameObject.transform.up * yVelocity;
        Vector3 movement = char_Direction + char_Gravity;
        _controller.Move(movement * Time.deltaTime);
        float speedblend = movement.magnitude;
        _animator.SetFloat("speedblend", speedblend);

        //Debug.Log(_controller.velocity.x + " " + _controller.velocity.z);
        _animator.SetBool("isRolling", isRolling);
        _animator.SetBool("isBackwalking", isBackwalking);

    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            yVelocity = jumpPower;
            _animator.SetTrigger("jump");
            _animator.SetFloat("speed", Mathf.Abs(_controller.velocity.x) + Mathf.Abs(_controller.velocity.z));
        }        

        
    }
    private void OnRun(InputAction.CallbackContext context)
    {
        isRunning = true;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
       
        isRunning = false;
    }
    private void OnRoll(InputAction.CallbackContext context)
    {
        isRolling = true;
    }

    private void OnRollCanceled(InputAction.CallbackContext context)
    {
        isRolling = false;
    }
    private void OnBackwalk(InputAction.CallbackContext context)
    {
        isBackwalking = true;
    }

    private void OnBackwalkCanceled(InputAction.CallbackContext context)
    {
        isBackwalking = false;
    }
    private void OnAttack(InputAction.CallbackContext context)
    {
        _animator.SetTrigger("attack");

    }

    public void Attack()
    {
        GameObject instStone = Instantiate(stonePrefab, stoneSpawnPos.position, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z));

        Rigidbody rb = instStone.GetComponent<Rigidbody>();

        rb.AddForce((instStone.transform.forward+instStone.transform.up) * stoneThrowPower, ForceMode.Impulse);
        stoneInHand.SetActive(false);
    }
    public void StoneIns()
    {
        stoneInHand.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            other.gameObject.GetComponent<MovingPlatform>().characterFollow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            other.gameObject.GetComponent<MovingPlatform>().characterFollow = false;
        }
    }

}
