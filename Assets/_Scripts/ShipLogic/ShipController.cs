using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    //Components
    private Rigidbody _rigidbody;
    
    //Movement and rotation
    [SerializeField, Range(0,20)] private float forwardRelativeForce;
    [SerializeField, Range(0,20)] private float torque;
    [SerializeField] private ForceMode movementForceMode;
    [SerializeField] private ForceMode torqueForceMode;
    private Vector3 _movementDirection;
    private Vector3 _rotationDirection;

    //Input System
    private PlayerControls _playerMovementControls;
    
    //Instance
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerMovementControls = new PlayerControls();
    }

    private void Update()
    {
        var movementInput = _playerMovementControls.Player.Movement.ReadValue<float>();
        var rotationInput = _playerMovementControls.Player.Rotation.ReadValue<float>();
        
        _movementDirection = new Vector3(0, 0, movementInput);
        _rotationDirection = new Vector3(0, rotationInput, 0);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(_movementDirection*forwardRelativeForce,movementForceMode);
        _rigidbody.AddRelativeTorque(_rotationDirection*torque,torqueForceMode);
    }
    
    public void EnableMovementControls()
    {
        _playerMovementControls.Player.Enable();
    }
    
    public void DisableMovementControls()
    {
        _playerMovementControls.Player.Disable();
    }
    
    private void OnDisable()
    {
        _playerMovementControls.Player.Disable();
    }
    
}
