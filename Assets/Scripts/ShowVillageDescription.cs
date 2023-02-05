using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class ShowVillageDescription : MonoBehaviour
{
    public TMP_Text text;
    public Button button;

    public VillageDataController villageDataController;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }
    public string GetMessageForBiggestStatType()
    {
        Stats biggestStat = villageDataController.SortVillageStatsByType().First();
        string message = "";

        switch (biggestStat.statType)
        {
            case StatType.STRENGTH:
                message = "To defeat this village, your abomination must have an inhuman power. Focus on building it's strength.";
                break;
            case StatType.INTELLIGENCE:
                message = "A sharp mind is crucial to succeed in this village.";
                break;
            case StatType.AGILITY:
                message = "Agility is key to conquering this village. Make sure your hero is quick on their feet.";
                break;
            case StatType.VITALITY:
                message = "This village will require great endurance.";
                break;
        }
        return message;
    }
    public void OnButtonClick()
    {
        string message = GetMessageForBiggestStatType();
        text.text = message;
    }

}
