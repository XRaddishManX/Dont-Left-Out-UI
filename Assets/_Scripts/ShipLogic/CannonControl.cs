using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonControl : MonoBehaviour
{
    //References
    private Camera _camera;

    //Input System
    private PlayerControls _playerCanonControls;
    
    //Instance
    
    void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _playerCanonControls = new PlayerControls();
    }

    void Update()
    {
        transform.LookAt(ObtainMouseWorldPosition());
    }

    private Vector3 ObtainMouseWorldPosition()
    {
        Vector3 mousePosition = _playerCanonControls.Player.Aiming.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.y = transform.position.y;
        return mouseWorldPosition;
    }

    public void EnableCanonControls()
    {
        _playerCanonControls.Player.Enable();
    }
    
    public void DisableCanonControls()
    {
        _playerCanonControls.Player.Disable();
    }

    private void OnDisable()
    {
        _playerCanonControls.Player.Disable();
    }
}
