using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    public bool isHead;
    public int plateCode;
    public string plateName;
    public float plateTimer;
    public float plateValue;
    public Image TimerBar;
    public float actualTimer;
    public GameObject nextPlate;
    public GameObject previousPlate;

    private void Start()
    {
        actualTimer = plateTimer;
    }

    private void Update()
     {
         if (isHead) {
             return;
         }else {
             TimerBar.fillAmount = actualTimer / plateTimer;
             if (actualTimer > 0) {
                 actualTimer -= Time.deltaTime;

             }else {
                 GameObject.Find("Restaurant").GetComponent<RestaurantScript>().runningOrders--;
                 GameObject.Find("Restaurant").GetComponent<RestaurantScript>().RemovePlate();
             }
         }
     }
}
