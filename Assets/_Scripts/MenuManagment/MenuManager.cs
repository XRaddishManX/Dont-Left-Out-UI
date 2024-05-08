using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] MenuVisibility[] menus;
    
    public static MenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenuName(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].SetMenuVisible();
            }
            else if (menus[i].IsOpen)
            {
                CloseMenu(menus[i]);
            }
        }
    }
    
    public void CloseMenuName(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].SetMenuNoVisible();
            }
        }
    }
    
    public void CloseAllMenus()
    {
        foreach (var menu in menus)
        {
            menu.SetMenuNoVisible();
        }
    }
    
    public void OpenMenu(MenuVisibility menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].IsOpen)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.SetMenuVisible();
    }
    
    void CloseMenu(MenuVisibility menu)
    {
        menu.SetMenuNoVisible();
    }
}
