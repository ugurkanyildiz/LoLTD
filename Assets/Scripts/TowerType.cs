using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerType : ScriptableObject
{
   public string towerName;
   public string description;
   public float buildingPrice;
   public float level2Price;
   public float level3Price;
   public GameObject level1Graphic;
   public GameObject level2Graphic;
   public GameObject level3Graphic;
}

