using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PartLocationSnapData : MonoBehaviour
{
    public enum SnapPoints {Starter_Head, Head_Starter, Head_Torso, Torso_LARM, Torso_RARM, Torso_LLEG, Torso_RLEG, LARM_Torso, RARM_Torso, LLEG_Torso, RLEG_Torso, Torso_Head}
    public List<SnapData> snapData = new List<SnapData>();
    public SnapData startingSnap;
    private void OnDrawGizmosSelected()
    {
        foreach (var snapData in snapData)
        {
            if (snapData.snapPointLocation == null) continue;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(snapData.snapPointLocation.position, 0.15f);
        }

        if (startingSnap.snapPointLocation != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(startingSnap.snapPointLocation.position, 0.15f);
        }
    }
}

[Serializable]
public class SnapData
{
    public PartLocationSnapData.SnapPoints snapPoint;
    public Transform snapPointLocation;
}

