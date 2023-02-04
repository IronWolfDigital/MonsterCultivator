using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPartData : MonoBehaviour
{
    public List<StatModifier> statModifiers = new List<StatModifier>();
    public List<PartObjectHolder> partObjectHolders = new List<PartObjectHolder>();
    public int initialPrice;
    public int priceIncrease;
    public int currentPrice;
    public PartObjectName partObjectName;

    private void OnEnable()
    {
        currentPrice = initialPrice;
    }
}