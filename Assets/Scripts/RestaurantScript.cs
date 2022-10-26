using System;
using System.Collections;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RestaurantScript : PlateList
{
  [Header("Status")]
  public int runningOrders;

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

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.P))
    {
      AddPlate(Random.Range(1,4));
    }
  }
  public IEnumerator StartCastOrders()
  {
    yield return new WaitForSeconds(1);
    InitiateOrders();
    StartCoroutine(StartCastOrders());
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
    Orders order = new Orders();
    
    
    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    string filePath = Path.Combine(desktop, "PlateList.txt");
    foreach (string line  in File.ReadAllLines(filePath))
    {
      lines++;
    }
    plateChoose = Random.Range(1, lines/4);
    Debug.Log($"<color=blue>PlatePlateChoose {plateChoose}</color>");
    foreach (string line  in File.ReadAllLines(filePath))
    {
      if ((numbersOfPlates / 4)== plateChoose -1 )
      {
        if (counter == 0)
        {
          name = line;
          Debug.Log($"<color=green>PlateName {line}</color>");
        }else if (counter == 1) {
          code = int.Parse(line);
          Debug.Log($"<color=green>PlateCode {line}</color>");
        }else if (counter == 2)
        {
          timer = float.Parse(line);
          Debug.Log($"<color=green>PlateTimer {line}</color>");
        }else if (counter == 3)
        {
          value = float.Parse(line);
          
          Debug.Log($"<color=green>PlateValue {line}</color>");
          counter = -1;
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
  void AddPlate(int code)
  {
    Vector3 nextTransform = new Vector3(200,0,0);
    GameObject prato;
     prato = Instantiate(plates[code - 1],transform.position + nextTransform * runningOrders,quaternion.identity,GameObject.Find("Canvas").transform);
     prato.GetComponent<Plate>().nextPlate = head.GetComponent<Plate>().nextPlate;
     prato.GetComponent<Plate>().previousPlate = head;
     prato.GetComponent<Plate>().nextPlate.gameObject.GetComponent<Plate>().previousPlate = prato;
     head.GetComponent<Plate>().nextPlate = prato;
     
     runningOrders+=1;
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
  
  public class Orders
{
    public Pedidos head;
    
    public Orders()
    {
        head = new Pedidos(0, "", 0, 0,null);
    }
    
    public void AdicionaPedido(int code,string name,float timer,float value)
    {
        Pedidos pedidos = new Pedidos(code, name, timer, value,null);
        pedidos.nextOrder = head.nextOrder;
        head.nextOrder = pedidos;
        
    }

   

    public void RemovePedidos(int code)
    {
        Vector3 jump = new Vector3(20,0,0);
       Pedidos pedidos = new Pedidos(0, "", 0, 0, null);
        
        for (pedidos = head.nextOrder;pedidos.nextOrder != null;pedidos = pedidos.nextOrder)
        {
            if (pedidos.orderCode == code) {
                pedidos.nextOrder = pedidos.nextOrder.nextOrder;
                break;
            }
        }
    }
}
public class Pedidos
{
    public int orderCode;
    public string orderName;
    public float orderTimer;
    public float orderValue;
    public Pedidos nextOrder;
    public Pedidos(int code, string name, float timer, float value, Pedidos seg)
    {
        orderCode = code;
        orderName = name;
        orderTimer = timer;
        orderValue = value;
        nextOrder = seg;
    }
}
}
