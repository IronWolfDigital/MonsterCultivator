using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaserController : MonoBehaviour
{
    public static PurchaserController Instance;
    public int soulsCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Destroy(gameObject);
    }

    public void TryPurchasingObjectPart(MonsterPartData monsterPartData)
    {
        
    }
}