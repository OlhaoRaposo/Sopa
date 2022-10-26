using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFlow : MonoBehaviour
{
    public GameObject[] clients;
    public int nextClient;
    void Start()
    {
        StartCoroutine("CheckClient");
    }

    void Update()
    {
    }
    void SpawnCliente()
    {
        clients[nextClient].SetActive(true);
    }

    IEnumerator CheckClient()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine("CheckClient");
    }
}
    
