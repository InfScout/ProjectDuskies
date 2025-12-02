using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
   [SerializeField] private Image[] tabImages;
   [SerializeField] private GameObject[] pages;

   [SerializeField] private Color selectedColor;

   void Start()
   {
      ActivateTab(0);
   }
   public void ActivateTab(int tabNo)
   {
      for (int i = 0; i < pages.Length; i++)
      {
         pages[i].SetActive(false);
         tabImages[i].color = selectedColor;
      }
      pages[tabNo].SetActive(true);
      tabImages[tabNo].color = Color.white;
   }

}


