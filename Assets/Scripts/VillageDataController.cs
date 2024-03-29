using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VillageDataController : MonoBehaviour
{
    public VillagesHolder villagesHolder;
    public int villageIndex;
    public int villageStatsSum = 0;
    public VillageAttackInfoDisplayController villageAttackInfoDisplayController;
    //public TMP_InputField monsterName;
    private void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.K))
        {
            List<Stats> sortedStats = SortVillageStatsByType(villageIndex);
        }
        #endif
    }

    public List<Stats> SortVillageStatsByType(int villageIndex)
    {
        List<Stats> sortedStats = villagesHolder.GetVillages()[villageIndex].stats.OrderByDescending(o => o.statValue).ToList();
        return sortedStats;
    }

    public List<Stats> SortVillageStatsByType()
    {
        List<Stats> sortedStats = villagesHolder.GetVillages()[villageIndex].stats.OrderByDescending(o => o.statValue).ToList();
        return sortedStats;
    }

    public void AttackVillage(MonsterController monsterController)
    {
        int totalVillagersKilled = 0;
        var villageStatsSorted = SortVillageStatsByType(villageIndex);

        for (int i = 0; i < villageStatsSorted.Count; i++) //going from the biggest stats to lowest
        {
            switch (i)
            {
                case 0:
                    var requiredMonsterStat =
                        monsterController.currentStats.First(x => x.statType == villageStatsSorted[i].statType);

                    totalVillagersKilled += (int)(villagesHolder.GetVillages()[villageIndex].villagersCount  * 0.4 +
                                            (requiredMonsterStat.statValue - villageStatsSorted[i].statValue) * 0.5);
                    break;
                case 1:
                    var requiredMonsterStatSecond =
                        monsterController.currentStats.First(x => x.statType == villageStatsSorted[i].statType);

                    totalVillagersKilled += (int)(villagesHolder.GetVillages()[villageIndex].villagersCount  * 0.3 +
                                                  (requiredMonsterStatSecond.statValue - villageStatsSorted[i].statValue) * 0.25);
                    break;
                case 2:
                    var requiredMonsterStatThird =
                        monsterController.currentStats.First(x => x.statType == villageStatsSorted[i].statType);

                    totalVillagersKilled += (int)(villagesHolder.GetVillages()[villageIndex].villagersCount  * 0.2 +
                                                  (requiredMonsterStatThird.statValue - villageStatsSorted[i].statValue) * 0.2);
                    break;
                case 3:
                    var requiredMonsterStatFourth =
                        monsterController.currentStats.First(x => x.statType == villageStatsSorted[i].statType);

                    totalVillagersKilled += (int)(villagesHolder.GetVillages()[villageIndex].villagersCount  * 0.1 + 
                                                  (requiredMonsterStatFourth.statValue - villageStatsSorted[i].statValue) * 0.05);
                    break;
            }
        }

        totalVillagersKilled =
            Mathf.Min(totalVillagersKilled, villagesHolder.GetVillages()[villageIndex].villagersCount);
        
        Debug.Log($"Villagers killed {totalVillagersKilled} / {villagesHolder.GetVillages()[villageIndex].villagersCount}");
        //monsterController.monsterName = monsterName.text;

        if (totalVillagersKilled >= villagesHolder.GetVillages()[villageIndex].villagersCount)
        {
            villageIndex++;
        }

        int soulsGained = (int)totalVillagersKilled * 3;
        
        villageAttackInfoDisplayController.StartAnimation(monsterController, totalVillagersKilled, 
            villagesHolder.GetVillages()[villageIndex].villagersCount, 
            soulsGained,villagesHolder.GetVillages()[villageIndex].villageName, villageIndex);
    }
}
