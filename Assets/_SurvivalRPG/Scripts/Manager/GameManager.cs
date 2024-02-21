using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public enum EEventNames {UpdateXP};
    enum EGameState{ Start = 0, Play = 1, Pause = 2, Die = 3, Revive = 4, GameOver =5 };

    [HideInInspector] public Action GameStart, GamePause, GameResume, GamePlay, GameRevive, GameOver, UpLevel;

    public System.Action<EEventNames, GameObject, System.Action> OnEventName;

    // Button
    public Dictionary<ButtonType, List<Button>> btnDict = new Dictionary<ButtonType, List<Button>>();
    [HideInInspector] public System.Action<ButtonType, GameObject, System.Action> ActionButtonClicked;


    [Header("----------------[ GameObject ]---------------")]
    [SerializeField] public FillBar hungerFillBar;
    [SerializeField] public FillBar thirstyFillBar;
    [SerializeField] public FillBar healthFillBar;
    [Header("----------------[ GameObject ]---------------")]
    [SerializeField] public Character_Player Player;
    [SerializeField] public GameObject Notice_Board;
    [SerializeField] Transform ReviveTransform;
    [SerializeField] public GameObject Canvas;
    
    [SerializeField] public FillBar XPHUD;

    [Tooltip("0:GamePlay - 1:Inventory")]
    [Header("----------------[ HUD ]---------------")]
    [SerializeField] public GameObject ReviveHud;
    [SerializeField] public GameObject SwitchPanels;
    [SerializeField] public GameObject InventoryHud;
    [SerializeField] public GameObject InventorySlotUI;
    [Header("----------------[ Button ]---------------")]
    [SerializeField] public Button PickupBtn;
    [SerializeField] public Button AttackBtn;

    int currentExp = 0;
    int currentLevel = 1;
    int requireExp = 10;

    // Coin
    int totalCoin = 0;
    [SerializeField] int ReviveCoin = 10;
    [SerializeField] TMP_Text[] totalCoinTMP;
    [SerializeField] TMP_Text[] LevelTMP;

    private void Awake()
    {
        Instance = this;
        GameStart += () =>
        {
            Character_Player cp = FindObjectOfType<Character_Player>();

            if (cp != null)
            {
                GameRevive();
            }
            else
            {
                if (Player != null)
                    Instantiate(Player);
            }

        };

        GameOver += () =>
        {
            //Time.timeScale = 0;
            SwitchPanel(EPanel.Revive);
        };

        GameRevive += () =>
        {
            SwitchPanel(EPanel.GamePlay);
        };

        UpLevel += () =>
        {
            
        };

        OnEventName += (EventName, GameObject, action) =>
        {
            switch (EventName)
            {
                case EEventNames.UpdateXP:

                    action.Invoke();
                    break;
            }

        };

        ActionButtonClicked += (btnType, go, action) =>
        {
            switch (btnType)
            {
                case ButtonType.ATTACK1:
                    Player.Attack();
                    break;

            }
        };
    }

    private void Start()
    {
        Init();
    }

    public async void Init()
    {
        while (!hungerFillBar || !thirstyFillBar || !healthFillBar)
        {
            await System.Threading.Tasks.Task.Delay(100);
        }
        GameStart();
        TotalCoin = PlayerPrefs.GetInt("Coin");
        CurrentExp = PlayerPrefs.GetInt("XP");
        Level = PlayerPrefs.GetInt("Level");
    }

    public int Level
    {
        get => currentLevel;
        set
        {
            foreach (var tmp in LevelTMP) tmp.SetText("Lv." + currentLevel);

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
            GameRevive();
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

            if (XPHUD) XPHUD.SetPercent(currentLevel / requireExp);
        }
    }

    public void RegisterButton(ButtonManager b)
    {
        if (btnDict == null)
            btnDict = new Dictionary<ButtonType, List<Button>>();

        if (!btnDict.ContainsKey(b.btnType))
            btnDict.Add(b.btnType, new List<Button>());

        btnDict[b.btnType].Add(b.GetComponent<Button>());
    }

    public void ActivateButton(ButtonType btn)
    {
        if (btnDict.ContainsKey(btn))
        {
            foreach (var b in btnDict[btn])
                b.interactable = true;
        }
    }

    public void DisableButton(ButtonType btn)
    {
        if (btnDict.ContainsKey(btn))
        {
            foreach (var b in btnDict[btn])
                b.interactable = false;
        }
    }

    public void CallPanel(int i)
    {
       
        EPanel panel = (EPanel)i;
        print(panel);
        SwitchPanel(panel);
    }

    public async void SwitchPanel(EPanel panel)
    {
        switch(panel)
        {
            case EPanel.Revive:
                await System.Threading.Tasks.Task.Delay(2000);
                ReviveHud.SetActive(true);
                break;
            case EPanel.GamePlay:
                ReviveHud.SetActive(false);
                SwitchPanels.SetActive(false);
                break;
            case EPanel.Inventory:
                SwitchPanels.SetActive(true);
                break;
        }
        
    }

    private void OnDisable()
    {
        SaveGame();
    }

    private void OnApplicationQuit()
    {
        //Debug.Log("Save");
        SaveGame();
    }

    private void SaveGame()
    { 
        PlayerPrefs.SetInt("Coin", TotalCoin);
        PlayerPrefs.SetInt("XP", CurrentExp);
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.Save();
    }
}

public enum EPanel { GamePlay, Inventory, Revive, Setting, Resume }