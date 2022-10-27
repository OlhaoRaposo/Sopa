using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    //Public VAR
    [Header("Aim Config")]
    public float MouseSens = 100f;
    public float minAngle, MaxAngle;
    public Transform player;
    //public Text intText;
    [Header("Text")]
    public Text intText;
    public int pedidoAtual;
    
    //Inventory
    [Header("Inventory")] 
    public int plate01;
    public int plate02;
    public int plate03;
    public float Cash;

    //Private VAR
    private float xRotation = 0f;
    private float distanceOfInteract = 4;
    private RaycastHit ray;
    private Vector3 rayVec;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minAngle, MaxAngle);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

        //Desenha um Ray do tamano do raycast
        Debug.DrawRay(Camera.main.transform.position, transform.forward * distanceOfInteract, Color.red);

        rayVec = Camera.main.transform.position;
        if (Physics.Raycast(rayVec, transform.forward, out ray))
        {
            if (ray.distance < distanceOfInteract)
            {
                if (ray.collider.gameObject.CompareTag("InteractiveObject"))
                {
                    //Esta Colidindo com o Objeto
                    Interact(ray.collider.gameObject);
                }
                else
                {
                    //Esse objeto nao e interativo.
                    NotInteract();
                    return;
                }
            }
            else
            {
                //Esta Colidindo mas esta muito longe.
                NotInteract();
                return;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("a");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("2a");
        }
    }
    void Interact(GameObject gmbj)
    {
        intText.enabled = true;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gmbj.GetComponent<InteractiveObject>().isClient)
            {
                if (gmbj.GetComponent<InteractiveObject>().hasOrderTaken == false)
                {
                    gmbj.GetComponent<InteractiveObject>().InteractClient();
                    gmbj.GetComponent<InteractiveObject>().hasOrderTaken = true;
                }else
                {
                    gmbj.GetComponent<InteractiveObject>().CloseOrder();
                }
                
            }else if (gmbj.GetComponent<InteractiveObject>().isorderTaker)
            {
                gmbj.GetComponent<InteractiveObject>().AddOrder();
            }else if (gmbj.GetComponent<InteractiveObject>().isOven)
            {
                gmbj.GetComponent<InteractiveObject>().InteractOven();
            }
        }
        
    }
    void NotInteract()
    {
        intText.enabled = false;
    }
}
