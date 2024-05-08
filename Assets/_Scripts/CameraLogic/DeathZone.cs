using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private bool isAlive = true;
    public GameObject Player;

    private float _TimeDeathCounter;
    public float _TimeThreshold = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAlive = true;
            Debug.Log("El jugador esta vivo");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            isAlive = false;
            Debug.Log("El jugador esta en peligro");
        }
    }

    private void Update()
    {
        if (!isAlive)
        {
            _TimeDeathCounter += Time.deltaTime;
        }

        if (_TimeDeathCounter > _TimeThreshold)
        {
            _TimeDeathCounter = 0;
            Debug.Log("YOU DIED");
        }
    }
}
