using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButton : MonoBehaviour
{
    public MonsterController monsterController;
    public AudioClip undoButton;
    private void OnMouseDown()
    {
        if (GameController.Instance.currentState == GameController.GameState.Fighting)
        {
            return;
        }
        SoundManager.Instance.Play(undoButton);
        monsterController.TryUndoPart(true);
    }
}
