using System.Collections;
using System.Collections.Generic;
using UIExample;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;

    private List<Bot> bots = new List<Bot>();

    [SerializeField] Level[] levels;
    public Level currentLevel;
    private GameObject currentLVIns;
    private int totalBot;
    private bool isRevive;

    private int levelIndex;

    public int TotalCharater => totalBot + bots.Count + 1;

    public void Start()
    {
        levelIndex = 0;
        OnLoadLevel(levelIndex);
        OnInit();
    }

    public void OnInit()
    {
        player.OnInit();

        for (int i = 0; i < currentLevel.botReal; i++)
        {
            NewBot(null);
        }

        totalBot = currentLevel.botTotal - currentLevel.botReal - 1;

        isRevive = false;

        SetTargetIndicatorAlpha(0);
    }

    public void OnReset()
    {
        player.OnDespawn();
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].OnDespawn();
        }

        bots.Clear();
        SimplePool.CollectAll();
    }

    public void OnLoadLevel(int level)
    {
        // Destroy instance cũ (nếu có)
        if (currentLVIns != null)
        {
            Destroy(currentLVIns); 
        }

        // Instantiate level mới và lưu cả 2 references
        Level levelInstance = Instantiate(levels[level]);
        currentLevel = levelInstance; // Component Level
        currentLVIns = levelInstance.gameObject; // GameObject
    }

    public Vector3 RandomPoint()
    {
        Vector3 randPoint = Vector3.zero;

        float size = Character.ATT_RANGE + Character.MAX_SIZE + 1f;

        for (int t = 0; t < 50; t++)
        {

            randPoint = currentLevel.RandomPoint();
            if (Vector3.Distance(randPoint, player.TF.position) < size)
            {
                continue;
            }

            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < bots.Count; i++)
                {
                    if (Vector3.Distance(randPoint, bots[i].TF.position) < size)
                    {
                        break;
                    }
                }

                if (j == 19)
                {
                    return randPoint;
                }
            }


        }

        return randPoint;
    }

    private void NewBot(IState<Bot> state)
    {
        Vector3 spawnPoint = RandomPoint();

        // ĐẢM BẢO VỊ TRÍ SPAWN TRÊN NAVMESH
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPoint, out hit, 10f, NavMesh.AllAreas))
        {
            spawnPoint = hit.position;
        }
        else
        {
            Debug.LogError("Không tìm thấy NavMesh hợp lệ cho spawn point!");
            return;
        }

        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, spawnPoint, Quaternion.identity);
        bot.OnInit();
        bot.ChangeState(state ?? new IdleState());
        bots.Add(bot);

        bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 7, player.Score + 7) : 1);
    }

    public void CharecterDeath(Character c)
    {
        if (c is Player)
        {
            UIManager.Ins.CloseAll();

            //revive
            if (!isRevive)
            {
                isRevive = true;
                UIManager.Ins.OpenUI<UIRevive>();
            }
            else
            {
                Fail();
            }
        }
        else
        if (c is Bot)
        {
            bots.Remove(c as Bot);

            if (GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Setting))
            {
                NewBot(Utilities.Chance(50, 100) ? new IdleState() : new PatrolState());
            }
            else
            {
                if (totalBot > 0)
                {
                    totalBot--;
                    NewBot(Utilities.Chance(50, 100) ? new IdleState() : new PatrolState());
                }

                if (bots.Count == 0)
                {
                    Victory();
                }
            }

        }

        UIManager.Ins.GetUI<UIGamePlay>().UpdateTotalCharacter();
    }

    private void Victory()
    {
        UserData.Ins.AddCoin(player.Coin);

        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIVictory>().SetCoin(player.Coin);
        player.ChangeAnim(Const.ANIM_WIN);
    }

    public void Fail()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIFail>().SetCoin(player.Coin);
    }

    public void Home()
    {
        if (player.Coin > 0)
        {
            UserData.Ins.AddCoin(player.Coin);
        }

        UIManager.Ins.CloseAll();
        OnReset();
        OnLoadLevel(levelIndex);
        OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void NextLevel()
    {
        // Tăng level và lưu
        levelIndex++;
        UserData.Ins.level = levelIndex;
        UserData.Ins.SaveAllData();

        // Reset và load màn mới
        UIManager.Ins.CloseAll();
        OnReset();
        OnLoadLevel(levelIndex);
        OnInit();
        UIManager.Ins.OpenUI<UIGamePlay>();
    }

    public void OnPlay()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new PatrolState());
        }
    }

    public void OnRevive()
    {
        player.TF.position = RandomPoint();
        player.OnRevive();
    }

    public void SetTargetIndicatorAlpha(float alpha)
    {
        List<GameUnit> list = SimplePool.GetAllUnitIsActive(PoolType.TargetIndicator);

        for (int i = 0; i < list.Count; i++)
        {
            (list[i] as TargetIndicator).SetAlpha(alpha);
        }
    }
}
