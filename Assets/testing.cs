using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public VillagesHolder villageData;
    public int indexToPrint;
    public void PrintVillageData(int i)
    {
        if (i >= 0 && i < villageData.GetVillages().Count)
        {
            Village village = villageData.GetVillages()[i];
            Debug.Log("Village Name: " + village.villageName);
            foreach (Stats stat in village.stats)
            {
                Debug.Log("Stat Type: " + stat.statType + " Stat Value: " + stat.statValue);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PrintVillageData(indexToPrint-1);
        }
    }
}
