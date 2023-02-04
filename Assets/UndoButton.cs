using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButton : MonoBehaviour
{
    public MonsterController monsterController;
    private void OnMouseDown()
    {
        monsterController.TryUndoPart(true);
    }
}
