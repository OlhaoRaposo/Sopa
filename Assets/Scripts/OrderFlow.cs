using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFlow : MonoBehaviour
{
    public GameObject[] clients;
    public GameObject[] slots;
    public int nextClient;
    void Start()
    {
        StartCoroutine("CheckClient");
    }
    

    IEnumerator CheckClient()
    {
        yield return new WaitForSeconds(5);
        nextClient = Random.Range(0, clients.Length);
        {
            if (slots[nextClient].transform.childCount == 0)
            {
                Instantiate(clients[nextClient].gameObject,slots[nextClient].transform.position,Quaternion.identity,slots[nextClient].transform);
            }
        }
        StartCoroutine("CheckClient");
    }
}
    
