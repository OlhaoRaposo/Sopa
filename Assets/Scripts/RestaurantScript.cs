using System;
using System.IO;
using System.Text;
using UnityEngine;

public class RestaurantScript : PlateList
{
  public PlateList menu;

  public void MontaMenu(string name)
  {
    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    string filePath = Path.Combine(desktop, "PlateList.txt");
    StreamReader sw = new StreamReader("PlateList.txt");





  }




}
