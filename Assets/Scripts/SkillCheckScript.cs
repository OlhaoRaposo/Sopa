using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillCheckScript : MonoBehaviour
{
    public RectTransform  hitboxRect;
    public RectTransform  pointerRect;
    public PointerScript pointer;
    public int plateToBeAdded;
    

    private void Start()
    {
        hitboxRect.transform.Rotate(0,0,Random.Range(0,361)); 
    }

   

    void Update()
     {
         pointerRect.transform.Rotate(0,0,-150 * Time.deltaTime *2);

         if (Input.GetKeyDown(KeyCode.Space))
         {
             if (pointer.hit == true)
             {
                 switch (plateToBeAdded)
                 {
                     case 1:
                         Camera.main.GetComponent<Mouse>().plate01++;
                         Camera.main.GetComponent<Mouse>().AttInventory();
                         break;
                     case 2:
                         Camera.main.GetComponent<Mouse>().plate02++;
                         Camera.main.GetComponent<Mouse>().AttInventory();
                         break;
                     case 3:
                         Camera.main.GetComponent<Mouse>().plate03++;
                         Camera.main.GetComponent<Mouse>().AttInventory();
                         break;
                     
                 }
                 Destroy(gameObject);
             }else{ 
                 Destroy(gameObject);
             }
         }
         
     }
}
