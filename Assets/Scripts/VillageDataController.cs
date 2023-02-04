using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VillageDataController : MonoBehaviour
{
    public VillagesHolder villagesHolder;
    public int villageIndex;
    public int villageStatsSum = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            List<Stats> sortedStats = SortVillageStatsByType(villageIndex);
        }
    }

    public List<Stats> SortVillageStatsByType(int villageIndex)
    {
        List<Stats> sortedStats = villagesHolder.GetVillages()[villageIndex].stats.OrderByDescending(o => o.statValue).ToList();
        villageStatsSum = 0;

        foreach (var stat in sortedStats)
        {
            villageStatsSum += stat.statValue;
            Debug.Log(stat.statType + ": " + stat.statValue);
        }
        return sortedStats;
    }



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
