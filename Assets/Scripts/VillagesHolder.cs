using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/VillagesHolder")]
public class VillagesHolder : ScriptableObject
{
    [SerializeField] private List<Village> villages;

    public List<Village> GetVillages()
    {
        return villages;
    }
}
