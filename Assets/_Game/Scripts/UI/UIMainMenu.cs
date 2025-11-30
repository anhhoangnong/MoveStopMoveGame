using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIExample;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    private const string ANIM_OPEN = "Open";
    private const string ANIM_CLOSE = "Close";
    [SerializeField] Text txtCoin;
    [SerializeField] TextMeshProUGUI playerCoinTxt;
    [SerializeField] RectTransform coinPoint;
    [SerializeField] Animation anim;
    public RectTransform CoinPoint => coinPoint;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollow.Ins.ChangeState(CameraFollow.State.MainMenu);

        playerCoinTxt.SetText(UserData.Ins.coin.ToString());

        anim.Play(ANIM_OPEN);

        UpdateCoinDisplay();

    }


    public void SettingButton()
    {

    }

    public void ShopButton()
    {
        UIManager.Ins.OpenUI<UIShop>();
        Close(0);
    }


    public void WeaponButton()
    {
        UIManager.Ins.OpenUI<UIWeapon>();
        Close(0);
    }

    public void PlayButton()
    {
        LevelManager.Ins.OnPlay();
        UIManager.Ins.OpenUI<UIGamePlay>();
        CameraFollow.Ins.ChangeState(CameraFollow.State.Gameplay);

        Close(0.5f);
        anim.Play(ANIM_CLOSE);
    }



   

    private void UpdateCoinDisplay()
    {
        if (txtCoin != null)
        {
            txtCoin.text = UserData.Ins.coin.ToString();
        }
    }
}
