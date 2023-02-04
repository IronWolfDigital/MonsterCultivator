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
        monsterPartData.currentPrice += monsterPartData.priceIncrease;
        partCount++;
        
        return true;
    }

    public void TryUndoPart()
    {
        if (partCount <= 0) return;
        PurchaserController.Instance.soulsCount += partControllers[partCount].monsterPartData.currentPrice;
        partControllers[partCount].UnequipPart();
        Destroy(partControllers[partCount]);
        partControllers.RemoveAt(partCount);
        partCount--;
    }
}