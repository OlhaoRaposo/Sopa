using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
  public bool hit;

  void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.CompareTag("HitBox"))
    {
      hit = true;
    }else if (col.gameObject.CompareTag("EndHit"))
    {
      hit = false;
    }
    
  }
}

