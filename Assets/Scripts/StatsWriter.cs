using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsWriter : MonoBehaviour
{
    private MonsterController monsterController;
    public TMP_Text textBox;
    public StatType statToDisplay;

    private void Start()
    {
        monsterController = GetComponent<MonsterController>();
    }

    private void Update()
    {
        foreach(var stat in monsterController.currentStats)
        {
            if(stat.statType == statToDisplay)
            {
                textBox.text = stat.statValue.ToString();
                break;
            }
        }
    }
}
