using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VillageDataController : MonoBehaviour
{
    public VillagesHolder villagesHolder;
    public void AttackVillage(MonsterController monsterController)
    {
        int totalStatDifference = 0;
        
        foreach (var stat in villagesHolder.GetVillages()[0].stats)
        {
            if(monsterController.currentStats.Exists(x => x.statType == stat.statType))
            {
                var requiredStat = monsterController.currentStats.First(x => x.statType == stat.statType);
                totalStatDifference += (Math.Abs(requiredStat.statValue - stat.statValue));
            }
        }
        
        //nuspresti, kaip totalStatDifference itakoja, kiek zmoniu suvalgei.... apskaiciuoti rewardus
    }
}
