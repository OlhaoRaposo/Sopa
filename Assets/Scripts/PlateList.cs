using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.UI;


public class PlateList : MonoBehaviour
{
    public Text Cardapio;
     void Awake() {
        AdicionaPratos(0,"",0,0);
    }
     
      void Start()
     {
         //AdicionaPratos(12,"Bolo",3,12);
         //AdicionaPratos(14,"Coxinha",2,4);
         //AdicionaPratos(13,"Biscoito",1,2);
         
         //MostraPratos();
         Debuga();
     }

       void Update()
      {
      }

      public Plates head;
    public PlateList()
    {
        head = new Plates(0, "", 0, 0,null);
    }
    public void AdicionaPratos(int code,string name,float timer,float value)
    {
        Plates plate = new Plates(code, name, timer, value,null);
        plate.nextPlate = head.nextPlate;
        head.nextPlate = plate;
        Debug.Log("Adicionou");
    }

    public void MostraPratos()
    {
        Plates plate = new Plates(0, "", 0, 0, null);

        for (plate = head.nextPlate;plate.nextPlate != null;plate = plate.nextPlate)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "PlateList.txt");
            StreamWriter sw = new StreamWriter(filePath, true,Encoding.ASCII);
            sw.Write($"{plate.plateName}\n{plate.plateCode.ToString()}\n{plate.plateTimer}\n{plate.plateValue}\n");
            sw.Close();
        }
    }

    public void Debuga()
    {
        int counter = 0;
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktop, "PlateList.txt");
        foreach (string line  in File.ReadAllLines(filePath))
        {
            Debug.Log($"<color=red>Contador {counter}</color>");
            switch (counter)
                {
                    case 0 :
                        Debug.Log($"<color=green>PlateName {line}</color>");
                        Cardapio.text += $"Prato: {line}\n";
                        break;
                    case 1:
                        Debug.Log($"<color=green>PlateCode {line}</color>");
                        Cardapio.text += $"Codigo: {line}\n";
                        break;
                    case 2:
                        Debug.Log($"<color=green>PlateTimer {line}</color>");
                        Cardapio.text += $"Tempo de Preparo: {line}\n";
                        break;
                    case 3:
                        Debug.Log($"<color=green>PlateValue {line}</color>");
                        Cardapio.text += $"Preço do prato: {line}\n \n";
                        counter = -1;
                        break;
                }
            counter++;
        }
    }

    public void RemovePratos(int code)
    {
        Plates plate = new Plates(0, "", 0, 0, null);
        
        for (plate = head.nextPlate;plate.nextPlate != null;plate = plate.nextPlate)
        {
            if (plate.plateCode == code)
            {
                plate.nextPlate = plate.nextPlate.nextPlate;
                break;
            }
        }
    }










}
public class Plates
{
    public int plateCode;
    public string plateName;
    public float plateTimer;
    public float plateValue;
    public Plates nextPlate;

    public Plates(int code, string name, float timer,float value,Plates seg)
    {
        plateCode = code;
        plateName = name;
        plateTimer = timer;
        plateValue = value;
        nextPlate = seg;
    }
}