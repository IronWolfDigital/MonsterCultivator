using System.Linq;
using UnityEngine;


// Represents current Live Part on a monster
public class PartController : MonoBehaviour
{
    public PartType partType;
    public MonsterPartData monsterPartData;
    public MonsterController monsterController;
    public GameObject spawnedGameObject;
    public PartLocationSnapData partLocationSnapData;
    public int pricePaid;
    public void EquipPart(MonsterController monsterController, MonsterPartData monsterPartData, PartObjectHolder partObjectHolder)
    {
        this.monsterPartData = monsterPartData;
        this.monsterController = monsterController;
        this.partType = partObjectHolder.partType;
        
        
        
        foreach (var stat in monsterPartData.statModifiers)
        {
            if(monsterController.currentStats.Exists(x => x.statType == stat.statType))
            {
                var requiredStat = monsterController.currentStats.First(x => x.statType == stat.statType);
                requiredStat.statValue += stat.statModifier;
            }
        }
        
        //Instantiate GO, get part location snap Data

    }

    public void UnequipPart()
    {
        foreach (var stat in monsterPartData.statModifiers)
        {
            if(monsterController.currentStats.Exists(x => x.statType == stat.statType))
            {
                var requiredStat = monsterController.currentStats.First(x => x.statType == stat.statType);
                requiredStat.statValue += stat.statModifier;
            }
        }

        //Destroy spawned GO
    }
}