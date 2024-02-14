using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    enum EGameState{ Start = 0, Play = 1, Pause = 2, Die = 3, Revive = 4, GameOver =5 };

    [HideInInspector] public Action GameStart, GamePause, GameResume, GamePlay, GameRevive, GameOver, UpLevel;

    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] Transform ReviveTransform;

    [Header("--------[ GameObject ]-------")]
    [SerializeField] public GameObject Canvas;

    int currentExp = 0;
    int currentLevel = 1;
    int requireExp = 10;

    // Coin
    int totalCoin = 0;
    [SerializeField] int ReviveCoin = 10;
    [SerializeField] TMP_Text[] totalCoinTMP;

    private void Awake()
    {
        GameOver += () =>
        {
            Time.timeScale = 0;

        };


        GameStart += () =>
        {
            Character_Player cp = FindObjectOfType<Character_Player>();
            if (cp != null)
            {
                GameRevive();
            }
            else 
            {
                Instantiate(PlayerPrefab);
            }
            
        };

        GameRevive += () =>
        {

        };

        UpLevel += () =>
        {

        };

    }

    private void Start()
    {

        Init();
    }

    public void Init()
    {
        GameStart();
        TotalCoin = PlayerPrefs.GetInt("Coin");
    }

    public int Level
    {
        get => currentLevel;
        set
        {
            if (currentLevel == value) return;

            currentLevel++;
        }
    }
    public int TotalCoin
    {
        get => totalCoin;
        set { 

            totalCoin = value;

            SaveGame();

            if (totalCoinTMP == null) return;

            foreach (var tmp in totalCoinTMP) tmp.SetText(totalCoin + "");
        }
    }

    public void Revive()
    {
        if (TotalCoin >= ReviveCoin)
        {
            TotalCoin -= ReviveCoin;
            
        }
        else
        {
    
        }
    }

    public int CurrentExp
    {
        get => currentExp;
        set
        {
            currentExp = value;
            if (currentExp >= requireExp)
            {
                currentLevel++;
                currentExp = currentExp - requireExp;
                requireExp = (int)(requireExp * 1.5f);
              
                UpLevel();
            }


            
        }
    }

    private void OnDisable()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("Coin", TotalCoin);
        PlayerPrefs.Save();
    }
}
