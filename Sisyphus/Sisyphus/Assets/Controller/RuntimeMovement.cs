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

    private void Start()
    {
        _controller= GetComponent<CharacterController>();
        _input= GetComponent<Movement>();
        _animator= GetComponent<Animator>();
        _input.jumpAction.performed += OnJump;
        _input.runAction.performed += OnRun; // Run action'�n� dinlemeye ba�l�yoruz
        _input.runAction.canceled += OnRunCanceled;
        _input.rollAction.performed += OnRoll; // Rolling action dinleyicisi
        _input.rollAction.canceled += OnRollCanceled;
        _input.backwalkAction.performed += OnBackwalk; // Backwalk action dinleyicisi
        _input.backwalkAction.canceled += OnBackwalkCanceled;

    }
    private void OnDestroy()
    {
        
        _input.jumpAction.performed -= OnJump;
        _input.runAction.performed -= OnRun; // Run action'�n� dinlemeyi b�rak�yoruz
        _input.runAction.canceled -= OnRunCanceled;
        _input.rollAction.performed -= OnRoll;
        _input.rollAction.canceled -= OnRollCanceled;
        _input.backwalkAction.performed -= OnBackwalk;
        _input.backwalkAction.canceled -= OnBackwalkCanceled;


    }
    private void Update()
    {
        Move();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _controller.height / 2 + 0.1f))
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
            yVelocity = -1; // Yer�ekimi etkisini s�f�rlamak i�in k���k negatif bir de�er
        }

        if (!isGrounded)
        {
            yVelocity -= gravity * Time.deltaTime; // Yer�ekimi ekle
        }

    }
    private void Move()
    {
        //_controller.Move(Vector3.down * 9.8f * Time.deltaTime);
        _controller.Move(new Vector3((_input.moveVal.x*_input.moveSpeed)/fraction, yVelocity, (_input.moveVal.y*_input.moveSpeed)/fraction));
        _animator.SetFloat("speed", Mathf.Abs(_controller.velocity.x)+ Mathf.Abs(_controller.velocity.z));
        float directionMultiplier = isBackwalking ? -1.0f : 1.0f; // Backwalk y�n� i�in �arpan
        float speedMultiplier = isRunning ? 2.0f : 1.0f;

        Vector3 movement = new Vector3(
            _input.moveVal.x * _input.moveSpeed * speedMultiplier * directionMultiplier / fraction,
            0f,
            (_input.moveVal.y * _input.moveSpeed * speedMultiplier) / fraction
        );

         // E�er ko�uyorsa h�z� iki kat�na ��kar
        //Vector3 speedMultiplier = new Vector3((_input.moveVal.x * _input.moveSpeed * speedMultiplier) / fraction, 0f, (_input.moveVal.y * _input.moveSpeed * speedMultiplier) / fraction);
        _controller.Move(movement * Time.deltaTime);

        float speedblend = movement.magnitude; // Hareket h�z�n� hesapl�yoruz
        _animator.SetFloat("speedblend", speedblend); // Animator'a hareket h�z�n� bildiriyoruz

        Debug.Log(_controller.velocity.x + " " + _controller.velocity.z);
        _animator.SetBool("isRolling", isRolling);
        _animator.SetBool("isBackwalking", isBackwalking);

    }
    private void OnJump(InputAction.CallbackContext context)
    {
        _animator.SetTrigger("jump");
        //_animator.SetBool("grounded", true);
        _animator.SetFloat("speed", Mathf.Abs(_controller.velocity.x) + Mathf.Abs(_controller.velocity.z));

        yVelocity = jumpPower;
    }
    private void OnRun(InputAction.CallbackContext context)
    {
        // Ko�ma modunu aktif hale getiriyoruz
        isRunning = true;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        // Ko�ma modunu iptal ediyoruz
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


  /*  private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            _animator.SetBool("grounded", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _animator.SetBool("grounded", false);
        }
    }*/
}
