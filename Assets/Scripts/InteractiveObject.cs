using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractiveObject : MonoBehaviour
{
    public GameObject player;

    [Header("Client")]
    public bool isClient;
    public bool hasOrderTaken = false;
    public int pedido;
    [SerializeField]
    private float hunger = 300;
    private float hunterMp;

    [Header("Order")] 
    public bool isorderTaker;

    [Header("Oven")] 
    public bool isOven;
    public GameObject ovenUi;
    public TMP_InputField input;
    public GameObject skillCheck;
    public string plateToBake;
    
    
    private void Start() {
        //SetVars
        player = GameObject.Find("Player");
        hunterMp = Random.Range(1, 3);
    }

     void Update()
     {
         if (hunger <= 0)
         {
             InteractClient();
         }else
            hunger -= Time.deltaTime * hunterMp;
     }
     public void CloseOrder()
     {
       switch (pedido)
       {
         case 1:
           if (Camera.main.GetComponent<Mouse>().plate01 > 0)
           {
             Camera.main.GetComponent<Mouse>().plate01--;
             GameObject.Find("Restaurant").GetComponent<RestaurantScript>().CloseOrder(pedido);
           }
           break;
         case 2 :
           if (Camera.main.GetComponent<Mouse>().plate02 > 0)
           {
             Camera.main.GetComponent<Mouse>().plate02--;
             GameObject.Find("Restaurant").GetComponent<RestaurantScript>().CloseOrder(pedido);
           }
           break;
         case 3:
           if (Camera.main.GetComponent<Mouse>().plate03 > 0)
           {
             Camera.main.GetComponent<Mouse>().plate03--;
             GameObject.Find("Restaurant").GetComponent<RestaurantScript>().CloseOrder(pedido);
           }
           break;
       }
     }
    public void InteractClient()
    {
        InitiateOrders();
        hasOrderTaken = true;
    }
    public void InitiateOrders()
      {
        int lines = 0;
        int counter = 0;
        int plateChoose;
        int numbersOfPlates = 0;
        string name = "";
        int code = 0;
        float timer = 0;
        float value = 0;
        
        
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktop, "PlateList.txt");
        foreach (string line  in File.ReadAllLines(filePath))
        {
          lines++;
        }
        plateChoose = Random.Range(1, lines/4+1);
        foreach (string line  in File.ReadAllLines(filePath))
        {
          if ((numbersOfPlates / 4)== plateChoose -1 )
          {
            if (counter == 0)
            {
              name = line;
            }else if (counter == 1) {
              code = int.Parse(line);
              pedido = code;
              Camera.main.GetComponent<Mouse>().pedidoAtual = pedido;
              Debug.Log(pedido);
            }else if (counter == 2)
            {
              timer = float.Parse(line);
            }else if (counter == 3)
            {
              value = float.Parse(line);
              
              break;
            }
          }
          else
          {
            numbersOfPlates++;
            if (counter == 3)
            {
              counter = -1;
            }
          }
          counter++;
        }
      }

    public void AddOrder()
    {
      GameObject.Find("Restaurant").GetComponent<RestaurantScript>().AddPlate(Camera.main.GetComponent<Mouse>().pedidoAtual);
      Camera.main.GetComponent<Mouse>().pedidoAtual = 0;
    }

    public void InteractOven()
    {
      if (ovenUi.activeSelf == false) {
        ovenUi.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
      }
      else
      {
        ovenUi.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
      }
    }

    public void OvenBake()
    {
      plateToBake = input.text;
      GameObject skillCheckObj;
      skillCheckObj = Instantiate(skillCheck,new Vector3(Screen.width/2,Screen.height/2,0),quaternion.identity);
      skillCheckObj.GetComponent<SkillCheckScript>().plateToBeAdded = int.Parse(plateToBake);
      skillCheckObj.transform.SetParent(GameObject.Find("Canvas").transform);
      InteractOven();
    }
    
    
}
