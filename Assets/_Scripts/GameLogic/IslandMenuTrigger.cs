using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandMenuTrigger : MonoBehaviour
{
    [SerializeField] private string menuName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MenuManager.Instance.OpenMenuName(menuName);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MenuManager.Instance.CloseAllMenus();
        }
    }
}
