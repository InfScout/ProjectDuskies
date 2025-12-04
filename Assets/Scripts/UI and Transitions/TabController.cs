using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
   [SerializeField] private Image[] tabImages;
   [SerializeField] private GameObject[] pages;

   [SerializeField] private Color selectedColor;

   void Awake()
   {
      ActivateTab(0);
   }
   
   public void ActivateTab(int tabNo)
   {
      for (int i = 0; i < pages.Length; i++)
      {
         bool isActive = i == tabNo;
         pages[i].SetActive(isActive);

         if (i == tabNo)
         {
            tabImages[tabNo].color = Color.white;
         }
         else
         {
            tabImages[i].color = selectedColor;
         }
      }
   }
}


