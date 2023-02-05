using System;
using System.Collections.Generic;
using DG.Tweening;
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

    public bool isLocked;
    public int unlockPrice;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public SpriteRenderer spriteRenderer;

    public List<StatMultiplication> statMultiplications;

    public List<DOTweenAnimation> doTweenAnimations = new List<DOTweenAnimation>();
    public DOTweenAnimation bobbleAnim;
    private void OnEnable()
    {
        currentPrice = initialPrice;
    }
    
    private void OnMouseDown()
    {
        Debug.Log("cLICKED");

        if (isLocked)
        {
            if (PurchaserController.Instance.soulsCount >= unlockPrice)
            {
                PurchaserController.Instance.soulsCount -= unlockPrice;
                isLocked = false;
                spriteRenderer.sprite = unlockedSprite;
                return;
            }
            else
            {
                return;
            }
        }
        
        PurchaserController.Instance.TryPurchasingObjectPart(this);
    }

    private void OnMouseEnter()
    {
        foreach (var dtanim in doTweenAnimations)
        {
            dtanim.DORestart();
            dtanim.DOPlay();
        }

        bobbleAnim.DOPlay();
    }

    private void OnMouseExit()
    {
        foreach (var dtanim in doTweenAnimations)
        {
            dtanim.DOPlayBackwards();
        }
        bobbleAnim.DORestart();
        bobbleAnim.DOPause();
    }

    private void Update()
    {
        costDisplay.text = isLocked ? "Unlock \n" + unlockPrice.ToString() : currentPrice.ToString();
    }
}