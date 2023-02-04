using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PartType
{
    Head,
    Body,
    Arm_Left,
    Arm_Right,
    Leg_Left,
    Leg_Right,
    Special
}

public class MonsterController : MonoBehaviour
{
    public List<PartController> partControllers = new List<PartController>();
    public List<Stats> currentStats = new List<Stats>();
    public int partCount = 0;
    public int maxPartCount = 4;
    public string monsterName;
    
    public Transform statsHolder;
    
    public void Init()
    {
        partCount = 0;
    }

    private void Update()
    {
        int index = 0;
        foreach (var statText in statsHolder.GetComponentsInChildren<TMP_Text>())
        {
            statText.text = currentStats[index].statType.ToString() + ": " +currentStats[index].statValue.ToString();
            index++;
        }
    }

    public bool TryAddPart(MonsterPartData monsterPartData)
    {
        Debug.Log("Called 1.5");
        if (partCount > maxPartCount) return false;
        Debug.Log("Called 2");
        var newPartController = gameObject.AddComponent<PartController>();
        partControllers.Add(newPartController);
        newPartController.EquipPart(this, monsterPartData, monsterPartData.partObjectHolders[partCount]);
        partCount++;
        
        return true;
    }

    public void DeleteVisuals()
    {
        foreach (var part in partControllers)
        {
            part.DestroyObject();
        }
    }

    public void Reset()
    {
        for (int i = 0; i < partCount; i++)
        {
            TryUndoPart();
        }
    }
    
    public void TryUndoPart()
    {
        if (partCount <= 0) return;
        partControllers[partCount-1].monsterPartData.currentPrice -=
            partControllers[partCount-1].monsterPartData.priceIncrease;
        PurchaserController.Instance.soulsCount += partControllers[partCount-1].monsterPartData.currentPrice;
        partControllers[partCount-1].UnequipPart();
        Destroy(partControllers[partCount-1]);
        partControllers.RemoveAt(partCount-1);
        partCount--;
    }
}