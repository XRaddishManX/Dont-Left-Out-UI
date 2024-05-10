using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class MyPlayerManager : MonoBehaviour
{
    private PhotonView _photonView;
    [SerializeField] private GameObject playerBoatPrefab;
    private GameObject _playerBoatInstantiated;
    
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (_photonView.IsMine)
        {
            CreatePlayerBoat();
        }
    }

    private void CreatePlayerBoat()
    {
        _playerBoatInstantiated = PhotonNetwork.Instantiate(this.playerBoatPrefab.name, 
            CreateRandomPositionSpawn(), Quaternion.identity);
        EnableControls();
    }

    private void EnableControls()
    {
        _playerBoatInstantiated.GetComponentInChildren<CannonControl>().EnableCanonControls();
        _playerBoatInstantiated.GetComponent<ShipController>().EnableMovementControls();
    }

    private Vector3 CreateRandomPositionSpawn()
    {
        var randomPositionX = Random.Range(-225, -175);
        var randomPositionZ = Random.Range(-20, 20);
        Vector3 randomPosition = new Vector3(randomPositionX, 0.5f, randomPositionZ);
        return randomPosition;
    }
}
