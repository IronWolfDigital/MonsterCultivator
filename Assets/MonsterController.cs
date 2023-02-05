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
    public int maxPartCount;
    public string monsterName;
    
    public Transform statsHolder;

    public Transform headStarter;
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
        if (partCount >= maxPartCount) return false;
        Debug.Log("Called 2");
        var newPartController = gameObject.AddComponent<PartController>();
        partControllers.Add(newPartController);
        newPartController.EquipPart(this, monsterPartData, monsterPartData.partObjectHolders[partCount]);
        if (partCount < maxPartCount)
        {
            partCount++;
        }

        
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
        Debug.Log($"Resetting: {partCount}");
        
        for (int i = partCount - 1; i >= 0; i--)
        {
            Debug.Log($"Trying to get part with index {i - 1}");
            partControllers[i].monsterPartData.currentPrice -=
                partControllers[i].monsterPartData.priceIncrease;
            PurchaserController.Instance.soulsCount += partControllers[i].monsterPartData.currentPrice;
            partControllers[i].UnequipPart(false);
            Destroy(partControllers[i]);
            partControllers.RemoveAt(i);
        }

        partCount = 0;
    }
    
    public void TryUndoPart(bool useforce = true)
    {
        if (partCount <= 0) return;
        partControllers[partCount-1].monsterPartData.currentPrice -=
            partControllers[partCount-1].monsterPartData.priceIncrease;
        PurchaserController.Instance.soulsCount += partControllers[partCount-1].monsterPartData.currentPrice;
        partControllers[partCount-1].UnequipPart(useforce);
        Destroy(partControllers[partCount-1]);
        partControllers.RemoveAt(partCount-1);
        partCount--;
    }
}