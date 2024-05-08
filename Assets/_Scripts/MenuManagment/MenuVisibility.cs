using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVisibility : MonoBehaviour
{
   public string menuName;
   public bool IsOpen { get; private set; }

   public void SetMenuVisible()
   {
      IsOpen = true;
      gameObject.SetActive(true);
   }
   
   public void SetMenuNoVisible()
   {
      IsOpen = false;
      gameObject.SetActive(false);
   }
}
