using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Image = UnityEngine.UI.Image;

public class VillageAttackInfoDisplayController : MonoBehaviour
{
    public DOTweenAnimation flyAnimation;
    public DOTweenAnimation fadeAnimation;

    public TMP_Text attackDescription;
    public TMP_Text howManySoulsGained;

    public Animator fadeAnimator;
    public MonsterController monsterController;

    public TMP_Text dayCount;

    public DOTweenAnimation textFadeAnim;

    public Image bgImage;
    public void StartAnimation(MonsterController monsterController, int totalVillagersKilled, int totalVillagers, int soulsGained, string villageName, int villageIndex)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("A village named ");
        sb.Append(villageName);
        sb.Append(" was attacked by an abomination");

        var biggestStat = monsterController.currentStats.OrderByDescending(o => o.statValue).First();

        switch (biggestStat.statType)
        {
            case StatType.STRENGTH:
                sb.Append(". In a horrific display of brute strength the creature managed to take the lives of ");
                break;
            case StatType.VITALITY:
                sb.Append(". In a unending surge of vitality the creature managed to take the lives of ");
                break;
            case StatType.INTELLIGENCE:
                sb.Append(". In a most cunningly malicious way the creature managed to take the lives of ");
                break;
            case StatType.AGILITY:
                sb.Append(". In a gust of deadly blades the creature managed to take the lives of ");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        sb.Append(totalVillagersKilled);
        sb.Append(" out of ");
        sb.Append(totalVillagers);
        sb.Append(" villagers. After the onslaught, the beast dissolved with its souls returning to its maker");

        attackDescription.text = sb.ToString();
        howManySoulsGained.text = "Souls gained: " + soulsGained.ToString();

        PurchaserController.Instance.soulsCount += soulsGained;

        dayCount.text = $"Day {villageIndex + 1}";
        fadeAnimator.SetTrigger("AttackStarted");
    }

    public void FullyFadedOut()
    {
        Debug.Log("fULLY FADED OUT");
        flyAnimation.DORestart();
        fadeAnimation.DORestart();
        flyAnimation.DOPlay();
        fadeAnimation.DOPlay();
    }
    
    private void DeleteMonsterVisuals()
    {
        monsterController.DeleteVisuals();
    }

    public void RemovePanel()
    {
        flyAnimation.DOPlayBackwards();
        fadeAnimation.DOPlayBackwards();

        textFadeAnim.DORestart();
        textFadeAnim.DOPlay();

        Invoke(nameof(StartFadingOut), 1f);
    }

    private void StartFadingOut()
    {
        fadeAnimator.SetTrigger("AttackEnded");
    }

    public void AnimationFullyEnded()
    {
        GameController.Instance.NextCycleStarted();
    }

    private void Start()
    {
        dayCount.text = "Day 1";

        textFadeAnim.DORestart();
        textFadeAnim.DOPlay();
    }
}
