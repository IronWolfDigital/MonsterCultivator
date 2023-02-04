using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;


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
        
        SetupPartLocation(partObjectHolder);
        //Instantiate GO, get part location snap Data

    }

    private void SetupPartLocation(PartObjectHolder partObjectHolder)
    {
        var snapData = partObjectHolder.gameObject.GetComponent<PartLocationSnapData>();

        Vector3 requiredPosition = Vector3.zero;

        foreach (var partLocation in GetComponentsInChildren<PartLocationSnapData>())
        {
            switch (snapData.startingSnap.snapPoint)
            {
                case PartLocationSnapData.SnapPoints.Starter_Head:
                    break;
                case PartLocationSnapData.SnapPoints.Head_Starter:
                    if (partLocation.snapData.Exists(x => x.snapPoint == PartLocationSnapData.SnapPoints.Starter_Head))
                    {
                        requiredPosition = partLocation.snapData
                            .First(x => x.snapPoint == PartLocationSnapData.SnapPoints.Starter_Head).snapPointLocation
                            .position;
                    }
                    break;
                case PartLocationSnapData.SnapPoints.Head_Torso:
                    break;
                case PartLocationSnapData.SnapPoints.Torso_LARM:
                    break;
                case PartLocationSnapData.SnapPoints.Torso_RARM:
                    break;
                case PartLocationSnapData.SnapPoints.Torso_LLEG:
                    break;
                case PartLocationSnapData.SnapPoints.Torso_RLEG:
                    break;
                case PartLocationSnapData.SnapPoints.LARM_Torso:
                    if (partLocation.snapData.Exists(x => x.snapPoint == PartLocationSnapData.SnapPoints.Torso_LARM))
                    {
                        requiredPosition = partLocation.snapData
                            .First(x => x.snapPoint == PartLocationSnapData.SnapPoints.Torso_LARM).snapPointLocation
                            .position;
                    }
                    break;
                case PartLocationSnapData.SnapPoints.RARM_Torso:
                    if (partLocation.snapData.Exists(x => x.snapPoint == PartLocationSnapData.SnapPoints.Torso_RARM))
                    {
                        requiredPosition = partLocation.snapData
                            .First(x => x.snapPoint == PartLocationSnapData.SnapPoints.Torso_RARM).snapPointLocation
                            .position;
                    }
                    break;
                case PartLocationSnapData.SnapPoints.LLEG_Torso:
                    if (partLocation.snapData.Exists(x => x.snapPoint == PartLocationSnapData.SnapPoints.Torso_LLEG))
                    {
                        requiredPosition = partLocation.snapData
                            .First(x => x.snapPoint == PartLocationSnapData.SnapPoints.Torso_LLEG).snapPointLocation
                            .position;
                    }
                    break;
                case PartLocationSnapData.SnapPoints.RLEG_Torso:
                    if (partLocation.snapData.Exists(x => x.snapPoint == PartLocationSnapData.SnapPoints.Torso_RLEG))
                    {
                        requiredPosition = partLocation.snapData
                            .First(x => x.snapPoint == PartLocationSnapData.SnapPoints.Torso_RLEG).snapPointLocation
                            .position;
                    }
                    break;
                case PartLocationSnapData.SnapPoints.Torso_Head:
                    if (partLocation.snapData.Exists(x => x.snapPoint == PartLocationSnapData.SnapPoints.Head_Torso))
                    {
                        requiredPosition = partLocation.snapData
                            .First(x => x.snapPoint == PartLocationSnapData.SnapPoints.Head_Torso).snapPointLocation
                            .position;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        spawnedGameObject = Instantiate(partObjectHolder.gameObject, requiredPosition, Quaternion.identity, transform);
    }
    
    public void UnequipPart()
    {
        foreach (var stat in monsterPartData.statModifiers)
        {
            if(monsterController.currentStats.Exists(x => x.statType == stat.statType))
            {
                var requiredStat = monsterController.currentStats.First(x => x.statType == stat.statType);
                requiredStat.statValue -= stat.statModifier;
            }
        }
        
        Destroy(spawnedGameObject);
    }


    
}