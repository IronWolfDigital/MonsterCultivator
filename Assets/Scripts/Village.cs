using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Village")]
public class Village : ScriptableObject
{
    public string villageName;
    public List<Stats> stats;

    public void AddStat(StatType type, int value)
    {
        Stats stat = new Stats
        {
            statType = type,
            statValue = value
        };
        stats.Add(stat);
    }
}

