using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class AttackButton : MonoBehaviour
{
    public Button button;
    public float fadeDuration = 1f;
    public MonsterController monsterController;
    public GameController gameController;
    private bool buttonFaded = false;

    private void Start()
    {
        button.onClick.AddListener(ButtonClicked);
    }
    void ButtonClicked()
    {     
        gameController.Attack();
        
    }
    private void Update()
    {
        if (monsterController.partCount == monsterController.maxPartCount && !buttonFaded)
        {
            buttonFaded = true;
            button.GetComponent<CanvasRenderer>().SetAlpha(0f);
            button.interactable = false;
            button.image.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                button.interactable = true;
            });
        }
        else if(monsterController.partCount != monsterController.maxPartCount)
        {
            buttonFaded = false;
            button.image.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                button.interactable = false;
            });
        }
    }
}
