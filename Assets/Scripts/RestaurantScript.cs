using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantScript : PlateList
{
  [Header("Status")]
  public int runningOrders;
  public Text cash;

  [Header("Objects")] 
  public GameObject head;
  public GameObject[] plates;
  protected override void Start()
  {
    transform.position = new Vector3(100, Screen.height - 100, 0);
    base.Start();
    head.GetComponent<Plate>().nextPlate = head;
    head.GetComponent<Plate>().previousPlate = head;
  }
  public void AddPlate(int code)
  {
    Vector3 nextTransform = new Vector3(200,0,0);
    GameObject prato = null;
    if (code != 0)
    {
      prato = Instantiate(plates[code-1],transform.position + nextTransform * runningOrders,quaternion.identity,GameObject.Find("Canvas").transform);
      prato.GetComponent<Plate>().nextPlate = head.GetComponent<Plate>().nextPlate;
      prato.GetComponent<Plate>().previousPlate = head;
      prato.GetComponent<Plate>().nextPlate.gameObject.GetComponent<Plate>().previousPlate = prato;
      head.GetComponent<Plate>().nextPlate = prato;
      runningOrders+=1;
    }else 
      return;
  }
  

  public void RemovePlate()
  {
    for (GameObject prato = head.GetComponent<Plate>().nextPlate;prato != head;prato = prato.GetComponent<Plate>().nextPlate)
    {
      if (prato.GetComponent<Plate>().actualTimer <= 0)
      {
        AttPlatePosition(prato);
        prato.GetComponent<Plate>().previousPlate.gameObject.GetComponent<Plate>().nextPlate = prato.GetComponent<Plate>().nextPlate;
        prato.GetComponent<Plate>().nextPlate.gameObject.GetComponent<Plate>().previousPlate = prato.GetComponent<Plate>().previousPlate;
        RemoveClient(prato.GetComponent<Plate>().plateCode);
        runningOrders --;
      }
    }
  }
  public void CloseOrder(int code)
  {
    Debug.Log("Close");
    Debug.Log(code);
    float timer = 0;
    GameObject lastPlate = null;
    
    for (GameObject prato = head.GetComponent<Plate>().nextPlate;prato != head;prato = prato.GetComponent<Plate>().nextPlate)
    {
      if (prato.GetComponent<Plate>().plateCode == code)
      {
        Camera.main.GetComponent<Mouse>().Cash += prato.gameObject.GetComponent<Plate>().plateValue;
        cash.text = "Cash: " + Camera.main.GetComponent<Mouse>().Cash;
        AttPlatePosition(prato);
        prato.GetComponent<Plate>().previousPlate.gameObject.GetComponent<Plate>().nextPlate = prato.GetComponent<Plate>().nextPlate;
        prato.GetComponent<Plate>().nextPlate.gameObject.GetComponent<Plate>().previousPlate = prato.GetComponent<Plate>().previousPlate;
        runningOrders--;
        RemoveClient(prato.GetComponent<Plate>().plateCode);
      }
    }
  }

  public void RemoveClient(int code)
  {
    GameObject[] clients;
    clients = GameObject.FindGameObjectsWithTag("InteractiveObject");
    for (int i = 0; i < clients.Length; i++)
    {
      if (clients[i].GetComponent<InteractiveObject>().pedido == code)
      {
        Destroy(clients[i].gameObject);
        break;
        
      }
    }
  }
  

  public void AttPlatePosition(GameObject plate)
  {
    Vector3 nextTransform = new Vector3(-200,0,0);

    for (GameObject prato = plate.GetComponent<Plate>().previousPlate; prato != head; prato = prato.GetComponent<Plate>().previousPlate)
    {
      prato.transform.parent = GameObject.Find("TemporaryParent").transform;
      
      prato.transform.position += nextTransform;
      
      prato.transform.parent = GameObject.Find("Canvas").transform;
    }
    Destroy(plate);
  }
  
}
