using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


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

                var statMultiplier = 1f;
                
                foreach (var statMultiplierData in this.monsterPartData.statMultiplications)
                {
                    if (statMultiplierData.statType == requiredStat.statType)
                    {
                        statMultiplier = statMultiplierData.multiplication;
                    }
                }
                
                requiredStat.statValue += (int) (stat.statModifier * statMultiplier) ;
            }
        }
        
        SetupPartLocation(partObjectHolder);
        //Instantiate GO, get part location snap Data

    }

    private void SetupPartLocation(PartObjectHolder partObjectHolder)
    {
        var snapData = partObjectHolder.gameObject.GetComponent<PartLocationSnapData>();
        Vector3 requiredPosition = Vector3.zero;
        partLocationSnapData = snapData;
        
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
        
        spawnedGameObject.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        var mask = spawnedGameObject.GetComponentInChildren<SpriteMask>();
        mask.enabled = true;
        mask.transform.DOMoveY(mask.transform.position.y - 150, 3f).SetEase(Ease.Linear);
        Invoke(nameof(DisableMaskInteraction), 0.4f);
    }

    private void DisableMaskInteraction()
    {
        var mask = spawnedGameObject.GetComponentInChildren<SpriteMask>();
        mask.enabled = false;
        spawnedGameObject.GetComponentInChildren<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
    }

    public void DestroyObject()
    {
        if (spawnedGameObject != null)
        {
            Destroy(spawnedGameObject);
        }
    }
    
    public void UnequipPart(bool useForce = true)
    {
        foreach (var stat in monsterPartData.statModifiers)
        {
            if(monsterController.currentStats.Exists(x => x.statType == stat.statType))
            {
                var requiredStat = monsterController.currentStats.First(x => x.statType == stat.statType);

                var statMultiplier = 1f;
                
                foreach (var statMultiplierData in this.monsterPartData.statMultiplications)
                {
                    if (statMultiplierData.statType == requiredStat.statType)
                    {
                        statMultiplier = statMultiplierData.multiplication;
                    }
                }
                
                requiredStat.statValue -= (int)(stat.statModifier * statMultiplier);
            }
        }

        if (useForce)
        {
            //do rigidbody
            if (spawnedGameObject != null)
            {
                var rb = spawnedGameObject.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
                rb.AddForce(new Vector3(Random.Range(-6f, 6f), Random.Range(0.5f, 1.2f), 0f), ForceMode2D.Impulse);
                rb.AddTorque(Random.Range(-3, 3), ForceMode2D.Impulse);
            }
        }
        else
        {
            DestroyObject();
        }
        
        
    }
    
    
}