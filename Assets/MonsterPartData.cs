using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterPartData : MonoBehaviour
{
    public List<StatModifier> statModifiers = new List<StatModifier>();
    public List<PartObjectHolder> partObjectHolders = new List<PartObjectHolder>();
    public int initialPrice;
    public int priceIncrease;
    public int currentPrice;
    public PartObjectName partObjectName;
    public TMP_Text costDisplay;
    private void OnEnable()
    {
        currentPrice = initialPrice;
    }
    
    private void OnMouseDown()
    {
        Debug.Log("cLICKED");
        PurchaserController.Instance.TryPurchasingObjectPart(this);
    }

    private void Update()
    {
        costDisplay.text = currentPrice.ToString();
    }
}