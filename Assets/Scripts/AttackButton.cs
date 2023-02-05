using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.CompilerServices;
using TMPro;

public class AttackButton : MonoBehaviour
{
    public Button button;
    public float iconFadeDuration = 1f;
    public float textFadeDuration = 1f;
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
            button.image.DOFade(1f, iconFadeDuration).OnComplete(() =>
            {
                button.interactable = true;
            });
            button.GetComponentInChildren<TMP_Text>().DOFade(1f, textFadeDuration);
        }
        else if(monsterController.partCount != monsterController.maxPartCount)
        {
            buttonFaded = false;
            button.GetComponentInChildren<TMP_Text>().DOFade(0f, textFadeDuration);
            button.image.DOFade(0f, iconFadeDuration).OnComplete(() =>
            {
                button.interactable = false;
            });
        }
    }
}
