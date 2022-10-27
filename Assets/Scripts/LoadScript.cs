using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
   public GameObject credits;
   public GameObject tutorial;
   
   public void LoadGame(string scene)
   {
      SceneManager.LoadScene(scene);
   }
   public void CloseGame()
   {
      Application.Quit();
   }

   public void OpenCredits()
   {
      if (credits.activeSelf == false) {
         credits.SetActive(true);
      }else {
         credits.SetActive(false);
      }
   }

   public void OpenTutorial()
   {
      if (tutorial.activeSelf == false) {
         tutorial.SetActive(true);
      }else {
         tutorial.SetActive(false);
      }
   }
}
