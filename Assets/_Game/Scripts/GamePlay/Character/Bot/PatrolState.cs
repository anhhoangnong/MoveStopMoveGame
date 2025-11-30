using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    private float stuckTimer = 0f;
    private Vector3 lastPosition;
    
    public void OnEnter(Bot t)
    {
        t.SetDestination(LevelManager.Ins.RandomPoint());
        lastPosition = t.TF.position;
        stuckTimer = 0f;
    }

    public void OnExecute(Bot t)
    {
        // Kiểm tra đã đến đích
        if (t.IsDestination)
        {
            t.ChangeState(new IdleState());
            return;
        }
        
        // KIỂM TRA BOT BỊ KẸT 
        stuckTimer += Time.deltaTime;
        if (stuckTimer > 0.5f)
        {
            if (Vector3.Distance(t.TF.position, lastPosition) < 0.1f)
            {
                // Bot bị kẹt -> tìm điểm mới
                t.SetDestination(LevelManager.Ins.RandomPoint());
            }
            lastPosition = t.TF.position;
            stuckTimer = 0f;
        }
    }

    public void OnExit(Bot t)
    {
    }
}
