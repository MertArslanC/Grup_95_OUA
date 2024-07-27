using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody capsule;
    public Vector2 moveVal;
    [SerializeField] public float moveSpeed = 10;
    public InputAction jumpAction;
    public InputAction runAction;
    public InputAction rollAction;
    public InputAction backwalkAction;
    public InputAction attackAction;

    private void Awake()
    {
        capsule = GetComponent<Rigidbody>();
        var inputActionAsset = GetComponent<PlayerInput>().actions;
        jumpAction = inputActionAsset["Jump"];
        runAction = inputActionAsset["Run"];
        rollAction = inputActionAsset["Roll"];
        backwalkAction = inputActionAsset["Backwalk"];
        attackAction = inputActionAsset["Attack"];
    }
    public void Moving(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
           
            moveVal= value.ReadValue<Vector2>();
            

        }
        if (value.canceled)
        {
            moveVal = value.ReadValue<Vector2>();
        }
    }
}
