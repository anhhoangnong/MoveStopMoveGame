using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIVictory : UICanvas
{
    private int coin;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] RectTransform x3Point;
    [SerializeField] RectTransform mainMenuPoint;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
    }

    public void x3Button()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();

        UIVfx.Ins.AddCoin(9, coin * 3, x3Point.position, UIVfx.Ins.CoinPoint);
    }

    public void NextAreaButton()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.Home();

        UIVfx.Ins.AddCoin(3, coin, mainMenuPoint.position, UIVfx.Ins.CoinPoint);
    }

    //internal void SetCoin(int coin)
    //{
    //    this.coin = coin;
    //    coinTxt.SetText(coin.ToString());
    //}

    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] TextMeshProUGUI txtTotalCoin; // Tổng coin hiện có

    public void SetCoin(int earnedCoin)
    {
        // Hiển thị coin vừa nhặt
        txtCoin.text = earnedCoin.ToString();

        // Hiển thị tổng coin
        if (txtTotalCoin != null)
        {
            txtTotalCoin.text = UserData.Ins.coin.ToString();
        }
    }

    public void OnClickHome()
    {
        Close(0);
        LevelManager.Ins.Home();
    }

    public void OnClickNextLevel()
    {
        Close(0);
        LevelManager.Ins.NextLevel();
    }
}
