using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchaserController : MonoBehaviour
{
    public static PurchaserController Instance;
    public int soulsCount;
    public int initialSouls = 100;
    public MonsterController monsterController;
    public TMP_Text soulsDisplay;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void Update()
    {
        if (soulsDisplay != null)
        {
            soulsDisplay.text = soulsCount.ToString();
        }
    }

    public void Init()
    {
        soulsCount = initialSouls;
    }
    
    public void TryPurchasingObjectPart(MonsterPartData monsterPartData)
    {
        Debug.Log("cALLED 1");
        if (soulsCount >= monsterPartData.currentPrice && monsterController.TryAddPart(monsterPartData))
        {
            Debug.Log("cALLED 3");
            soulsCount -= monsterPartData.currentPrice;
            monsterPartData.currentPrice += monsterPartData.priceIncrease;
        }

    }
}