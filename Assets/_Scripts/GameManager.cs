using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    private Camera _camera;
    private CameraController _cameraController;
    private bool _isAlreadyInMainMenu;
    
    [SerializeField] private GameObject playerBoatMenu;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _cameraController = _camera.GetComponent<CameraController>();
        
    }

    private void Start()
    {
        _isAlreadyInMainMenu = false;
    }

    private void Update()
    {
        if (Launcher.Instance.IsConnectedToLobby && !_isAlreadyInMainMenu)
        {
            StartGameMenu();
            _isAlreadyInMainMenu = true;
        }
    }

    private void StartGameMenu()
    {
        {
            _cameraController.SetCameraTopDownView();
            playerBoatMenu.GetComponentInChildren<CannonControl>().EnableCanonControls();
            playerBoatMenu.GetComponent<ShipController>().EnableMovementControls();
        }
    }

    public override void OnJoinedRoom()
    {
       _cameraController.MoveCameraToRoomScenario();
       playerBoatMenu.GetComponentInChildren<CannonControl>().DisableCanonControls();
       playerBoatMenu.GetComponent<ShipController>().DisableMovementControls();
    }

    public override void OnLeftRoom()
    {
        _cameraController.MoveCameraToMenuScenario();
        playerBoatMenu.GetComponentInChildren<CannonControl>().EnableCanonControls();
        playerBoatMenu.GetComponent<ShipController>().EnableMovementControls();
    }
    
}
