using System.Collections;
using System.Collections.Generic;
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
    public int maxPartCount = 5;

    public bool TryAddPart(MonsterPartData monsterPartData)
    {
        if (partCount >= maxPartCount) return false;

        //Check price and remove money;
        
        var newPartController = gameObject.AddComponent<PartController>();
        partControllers.Add(newPartController);
        newPartController.EquipPart(this, monsterPartData, monsterPartData.partObjectHolders[partCount]);
        monsterPartData.currentPrice += monsterPartData.priceIncrease;
        
        partCount++;
        
        return true;
    }

    public void UndoPart()
    {
        partControllers[partCount].UnequipPart();
        Destroy(partControllers[partCount]);
        //Add money from the part;
        partControllers.RemoveAt(partCount);
        partCount--;
    }
}