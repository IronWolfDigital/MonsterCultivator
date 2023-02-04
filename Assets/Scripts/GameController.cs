using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public enum GameState {Growing, Fighting}

    public GameState currentState;
    public PurchaserController purchaserController;
    public MonsterController monsterController;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Destroying gameobject");
            Destroy(gameObject);   
        }
    }

    private void Start()
    {
        currentState = GameState.Growing;
        purchaserController.Init();
        monsterController.Init();
    }

    public void Attack()
    {
        currentState = GameState.Fighting;
    }
}
