using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Finish, Revive, Setting }

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState) => this.gameState == gameState;

    private void Awake()
    {

        //lock fps xuống 60fps
        Application.targetFrameRate = 60;

        //Init data
        UserData.Ins.OnInitData();
        //Debug.Log($"Loaded coin: {UserData.Ins.coin}");
    }

    private void Start()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
    }
}
