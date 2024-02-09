using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    enum EGameState{ Start = 0, Play = 1, Pause = 2, Die = 3, Revive = 4, GameOver =5 };

    [HideInInspector] public Action GameStart, GamePause, GameResume, GamePlay, GameRevive, GameOver;

    private void Awake()
    {
        
    }

    
}
