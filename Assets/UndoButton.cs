using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButton : MonoBehaviour
{
    public MonsterController monsterController;
    private void OnMouseDown()
    {
        if (GameController.Instance.currentState == GameController.GameState.Fighting)
        {
            return;
        }
        monsterController.TryUndoPart(true);
    }
}
